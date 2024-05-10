using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;

namespace infoManagerAPI.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UserResponse> CreateAsync(UserRequest user);
        Task<bool> UpdatePasswordAsync(int id, PasswordRequest request);
        Task<UserResponse?> GetByIdAsync(int id);
        Task<List<UserResponse>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
