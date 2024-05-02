
using Microsoft.AspNetCore.Mvc;
using infoManager.Models;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.Exceptions;
using infoManager.Models.Enums;
using infoManagerAPI.DTO.Person.Response;

namespace infoManagerAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController(IPeopleService service) : ControllerBase
    {
        private readonly IPeopleService _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetPeople()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<PersonResponse>> GetPerson(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);            
            }            
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutPerson(PersonUpdateRequest person, int id)
        {
            try 
            {
                await _service.UpdateAsync(person, id);
                return Ok("Updated successfully!"); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PatchStatusPerson(StatusEnum status, int id)
        {
            try
            {
                await _service.UpdateStatusAsync(status, id);
                return Ok("Status:" + status.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonResponse>> PostPerson(PersonRequest person)
        {
            try
            {
                var createdPerson = await _service.CreateAsync(person);
                return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
