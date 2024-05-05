using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace infoManagerAPI.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController(IUsersService service) : ControllerBase
    {
    
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateJwtToken(AuthenticationRequest request)
        {

            return Ok(new { token = await service.GeneratorJwtToken(request) });
        }
    }
}
