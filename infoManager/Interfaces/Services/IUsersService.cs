using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Models;
using infoManagerAPI.Models.Enums;

namespace infoManagerAPI.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UserResponse> CreateAsync(UserRequest user);
        Task<bool> UpdatePasswordAsync(string password, UserRequest user);
        Task<UserResponse?> GetByIdAsync(int id);
        Task<List<UserResponse>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<string> GeneratorJwtToken(AuthenticationRequest request);
    }
}
