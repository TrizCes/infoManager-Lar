using infoManagerAPI.DTO.Authenticate.Request;

namespace infoManagerAPI.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> GeneratorJwtToken(AuthenticationRequest request);
    }
}
