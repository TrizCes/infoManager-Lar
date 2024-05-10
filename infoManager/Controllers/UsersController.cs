using Microsoft.AspNetCore.Mvc;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.DTO.User.Response;
using AutoMapper;
using infoManagerAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace infoManagerAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController (IUsersService service, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            try
            {
                return await service.GetAllAsync();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            try
            {
                var user = await service.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception e) 
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin, Regular, Visitor")]
        public async Task<IActionResult> PatchUserPassword(int id, PasswordRequest request)
        {
            var idVerify = GetIdOfUser();
            if (idVerify != id) return BadRequest("Error in id verification");
            try
            {
                var updated = await service.UpdatePasswordAsync(id, request);
                return NoContent();
            }
            catch (HttpException e) 
            {
                if (e.StatusCode == 404) return NotFound(e.Message);
                if (e.StatusCode == 409) return Conflict(e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> PostUser(UserRequest user)
        {
            try
            {
                var Data = await service.CreateAsync(user);

                return CreatedAtAction("GetUser", new { id = Data.Id }, Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await service.DeleteAsync(id);
                return NoContent();

            }
            catch (HttpException e) 
            {
                if (e.StatusCode == 404) return NotFound(e.Message);
                if (e.StatusCode == 409) return Conflict(e.Message);

                return BadRequest(e.Message);
            }
        }

        protected int GetIdOfUser()
        {
            var user = HttpContext.User.Identities.FirstOrDefault();
            int id = int.Parse(user.Claims.First().Value);
            return id;
        }
    }
}
