using infoManagerAPI.Models.Enums;
using infoManagerAPI.Models;
using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.DTO.Person.Response;

namespace infoManagerAPI.Interfaces.Services
{
    public interface IPeopleService
    {
        Task<PersonResponse> CreateAsync(PersonRequest person);
        Task<bool> UpdateAsync(PersonUpdateRequest person, int id);
        Task<bool> UpdateStatusAsync(StatusEnum status, int id);
        Task<PersonResponse?> GetByIdAsync(int id);
        Task<List<PersonResponse>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
