using infoManager.Models;
using infoManagerAPI.DTO.PhoneNumber.Request;
using infoManagerAPI.DTO.PhoneNumber.Response;

namespace infoManagerAPI.Interfaces.Services
{
    public interface IPhoneNumbersService
    {
        Task<PhoneNumber> CreateAsync(PhoneNumberRequest phone);
        Task<PhoneNumberResponse> UpdateAsync(PhoneNumberRequest phone, int id);
        Task<PhoneNumberResponse?> GetByIdAsync(int id);
        Task<List<PhoneNumberResponse>> GetAllAsync();
        Task<List<PhoneNumberResponse>> GetByPersonAsync(int personId);
        Task<bool> DeleteAsync(int id);
    }
}
