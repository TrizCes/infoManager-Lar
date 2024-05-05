using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using infoManagerAPI.Data;
using infoManagerAPI.Models;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.DTO.User.Response;

namespace infoManagerAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController (IUsersService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            return await service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            var user = await service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPatch("password/{id}")]
        public async Task<IActionResult> PatchUserPassword(int id, string password)
        {
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostUser(UserRequest user)
        {
            var Data = await service.CreateAsync(user);

            return CreatedAtAction("GetUser", new { id = Data.Id }, Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = service.DeleteAsync(id);
                return NoContent();

            }catch (Exception e) 
            {
                if (e.HResult == 404) return NotFound(e.Message);
                if (e.HResult == 409) return Conflict(e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}
