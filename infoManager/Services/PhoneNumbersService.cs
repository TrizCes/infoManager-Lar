using AutoMapper;
using infoManagerAPI.DTO.PhoneNumber.Request;
using infoManagerAPI.DTO.PhoneNumber.Response;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.Models;


namespace infoManagerAPI.Services
{
    public class PhoneNumbersService(IPhoneNumbersRepository repository, IMapper mapper, IPeopleRepository peopleRepository) : IPhoneNumbersService
    {

        public async Task<PhoneNumberFullResponse> CreateAsync(PhoneNumberRequest phone)
        {
            if (string.IsNullOrWhiteSpace(phone.Number))
                throw new BadRequestException("Number field cannot be empty");
            if (string.IsNullOrWhiteSpace(phone.PersonId.ToString()))
                throw new BadRequestException("Person Id field cannot be empty");
            if (string.IsNullOrWhiteSpace(phone.Type.ToString()))
                throw new BadRequestException("Type field cannot be empty");

            var PersonExist = await peopleRepository.GetByIdAsync(phone.PersonId);
            if (PersonExist == null) throw new BadRequestException("Person ID doesn't matches our database");

            var data = mapper.Map<PhoneNumber>(phone);
            await repository.CreateAsync(data);
            var response = mapper.Map<PhoneNumberFullResponse>(data);
            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _ = await GetByIdAsync(id) ?? throw new NotFoundException("ID not found");
            var result = await repository.DeleteAsync(id);
            return result;
        }

        public async Task<List<PhoneNumberResponse>> GetAllAsync()
        {
            var Data = await repository.GetAllAsync();
            var Response = mapper.Map<List<PhoneNumberResponse>>(Data);
            return Response;
        }

        public async Task<PhoneNumberFullResponse?> GetByIdAsync(int id)
        {
            var Data = await repository.GetByIdAsync(id);
            if (Data == null) throw new NotFoundException("ID doesn't exist");
            var response = mapper.Map<PhoneNumberFullResponse>(Data);
            return response;

        }

        public async Task<List<PhoneNumberResponse>> GetByPersonAsync(int personId)
        {
            var Person = await peopleRepository.GetByIdAsync(personId);
            if (Person == null) throw new NotFoundException("Person ID doesn't exist");
            var Data = await repository.GetByPersonIdAsync(personId);
            var Response = mapper.Map<List<PhoneNumberResponse>>(Data);
            return Response;
        }

        public async Task<PhoneNumberResponse> UpdateAsync(PhoneNumberRequest phone, int id)
        {
            var ancient = await repository.GetByIdAsync(id);

            if (ancient == null) throw new BadRequestException($"The ID {id} doesn't exist");
            if (ancient.PersonId != phone.PersonId) throw new BadRequestException($"Person ID {phone.PersonId} doesn't exist for this ID {id}");
            if (string.IsNullOrWhiteSpace(phone.Number)) throw new BadRequestException("Number field cannot be empty");
            if (string.IsNullOrWhiteSpace(phone.Type.ToString())) throw new BadRequestException("Type field cannot be empty");

            

            var data = mapper.Map<PhoneNumber>(phone);
            data.Id = id;
            await repository.UpdateAsync(data);
            var Response = mapper.Map<PhoneNumberResponse>(data);
            return Response;
        }
    }
}
