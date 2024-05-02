using infoManager.Models;
using infoManager.Models.Enums;

namespace infoManagerAPI.Interfaces.Repositories
{
    public interface IPeopleRepository
    {
        Task<bool> CreateAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> UpdateStatus(StatusEnum status, int id);
        Task<Person?> GetByIdAsync(int id);
        Task<List<Person>> GetAllAsync();
        Task<Person?> GetByCpfAsync(string cpf);
        Task<bool> DeleteAsync(int id);
        void Detach(Person person);
    }
}
