using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.DTO.Person.Response;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Models;
using infoManagerAPI.Models.Enums;

namespace infoManagerAPI.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> CreateAsync(User user);
        Task<bool> UpdatePasswordAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserAsync(string email);
        Task<List<User>> GetAllAsync();
        Task<bool> DeleteAsync(User user);
        void Detach (User user);
    }
}
