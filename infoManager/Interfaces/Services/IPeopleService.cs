using infoManager.Models.Enums;
using infoManager.Models;
using infoManagerAPI.DTO.Person.Request;

namespace infoManagerAPI.Interfaces.Services
{
    public interface IPeopleService
    {
        Task<Person> CreateAsync(PersonRequest person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> UpdateStatus(StatusEnum status, int id);
        Task<Person?> GetByIdAsync(int id);
        Task<List<Person>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
