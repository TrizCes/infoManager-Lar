
using Microsoft.AspNetCore.Mvc;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.DTO.PhoneNumber.Response;
using infoManagerAPI.DTO.PhoneNumber.Request;
using infoManagerAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace infoManagerAPI.Controllers
{
    [Route("api/phone")]
    [ApiController]
  
    public class PhoneNumbersController(IPhoneNumbersService service) : ControllerBase
    {

        [HttpGet("all")]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PhoneNumberResponse>>> GetPhoneNumbers()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (HttpException ex)
            {
                if (ex.StatusCode == 401) return Unauthorized(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("person/{id}")]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PhoneNumberResponse>>> GetPhoneByPerson(int id)
        {
            try 
            { 
                return Ok(await service.GetByPersonAsync(id));
            }
            catch (HttpException ex)
            {
                if (ex.StatusCode == 401) return Unauthorized(ex.Message);
                return NotFound(ex.Message);
    
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        public async Task<ActionResult<PhoneNumberResponse>> GetPhoneNumber(int id)
        {
            try 
            { 
                return Ok(await service.GetByIdAsync(id));
            }
            catch (HttpException ex)
            {
                if (ex.StatusCode == 401) return Unauthorized(ex.Message);
                return NotFound(ex.Message);

            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Regular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<PhoneNumberResponse?>> PutPhoneNumber(int id, PhoneNumberRequest phoneNumber)
        {
            try
            {
                var data = await service.UpdateAsync(phoneNumber, id);               
                return Ok(data);
            }
            catch(HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                if (e.StatusCode == 404) return NotFound(e.Message);
                if (e.StatusCode == 409) return Conflict(e.Message);

                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Regular")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PhoneNumberFullResponse>> PostPhoneNumber(PhoneNumberRequest phoneNumber)
        {
            try
            {
                var data = await service.CreateAsync(phoneNumber);
                return CreatedAtAction(nameof(GetPhoneNumber),new { Id = data.Id }, data);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeletePhoneNumber(int id)
        {
            try
            {
                await service.DeleteAsync(id);
                return NoContent();
            }
            catch (HttpException e)
            {
                if (e.StatusCode == 401) return Unauthorized(e.Message);
                if (e.StatusCode == 404) return NotFound(e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}
