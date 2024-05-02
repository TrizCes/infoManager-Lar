using infoManager.Models.Enums;
using infoManager.Models;

namespace infoManagerAPI.Interfaces.Repositories
{
    public interface IPhoneNumbersRepository
    {
        Task<bool> CreateAsync(PhoneNumber phone);
        Task<PhoneNumber> UpdateAsync(PhoneNumber phone);
        Task<PhoneNumber?> GetByIdAsync(int id);
        Task<List<PhoneNumber>> GetAllAsync();
        Task<List<PhoneNumber>> GetByPersonIdAsync(int personId);
        Task<bool> DeleteAsync(int id);
        void Detach(PhoneNumber phone);
    }
}
