
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using infoManagerAPI.Models;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.DTO.PhoneNumber.Response;
using infoManagerAPI.DTO.PhoneNumber.Request;
using infoManagerAPI.Exceptions;
using NuGet.Protocol;
using Microsoft.AspNetCore.Http.HttpResults;

namespace infoManagerAPI.Controllers
{
    [Route("api/phone")]
    [ApiController]
    public class PhoneNumbersController(IPhoneNumbersService service) : ControllerBase
    {

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PhoneNumberResponse>>> GetPhoneNumbers()
        {
            try
            {
                return Ok(await service.GetAllAsync());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("person/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PhoneNumberResponse>>> GetPhoneByPerson(int id)
        {
            try 
            { 
                return Ok(await service.GetByPersonAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
    
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneNumberResponse>> GetPhoneNumber(int id)
        {
            try 
            { 
                return Ok(await service.GetByIdAsync(id));
            }
            catch (Exception ex)
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
        public async Task<ActionResult<PhoneNumberResponse?>> PutPhoneNumber(int id, PhoneNumberRequest phoneNumber)
        {
            try
            {
                var data = await service.UpdateAsync(phoneNumber, id);               
                return Ok(data);
            }
            catch(Exception e)
            {
                if (e.HResult == 404) return NotFound(e.Message);
                if (e.HResult == 409) return Conflict(e.Message);

                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
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
            catch (Exception e)
            {
                if (e.HResult == 404) return NotFound(e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}
