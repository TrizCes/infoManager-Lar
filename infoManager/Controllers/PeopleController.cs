
using Microsoft.AspNetCore.Mvc;
using infoManagerAPI.Models;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Models.Enums;
using infoManagerAPI.DTO.Person.Response;
using Microsoft.AspNetCore.Authorization;

namespace infoManagerAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    [Authorize(Roles = "Admin, Regular, Visitor")]
    public class PeopleController(IPeopleService service) : ControllerBase
    {
        private readonly IPeopleService _service = service;

        [HttpGet]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetPeople()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<PersonResponse>> GetPerson(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                return NotFound(e.Message);            
            }            
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Regular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutPerson(PersonUpdateRequest person, int id)
        {
            try 
            {
                await _service.UpdateAsync(person, id);
                return Ok("Updated successfully!"); 
            }
            catch (HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("status/{id}")]
        [Authorize(Roles = "Admin, Regular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PatchStatusPerson(StatusEnum status, int id)
        {
            try
            {
                await _service.UpdateStatusAsync(status, id);
                return Ok("Status:" + status.ToString());
            }
            catch (HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Regular")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PersonResponse>> PostPerson(PersonRequest person)
        {
            try
            {
                var createdPerson = await _service.CreateAsync(person);
                return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
            }
            catch (HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (HttpException e)
            {
                if(e.StatusCode == 401) return Unauthorized(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
