using AutoMapper;
using infoManager.Models;
using infoManager.Models.Enums;
using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.DTO.Person.Response;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace infoManagerAPI.Services
{
    public class PeopleService(IPeopleRepository repository, IMapper mapper) : IPeopleService
    {
        public async Task<PersonResponse> CreateAsync(PersonRequest person)
        {
            if (string.IsNullOrWhiteSpace(person.Name))
                throw new BadRequestException("Name field cannot be empty");

            if (string.IsNullOrWhiteSpace(person.Cpf))
                throw new BadRequestException("CPF field cannot be empty");

            var CpfExist = await repository.GetByCpfAsync(person.Cpf);
            if(CpfExist != null) throw new BadRequestException("CPF is already registered");

            var sixteenYearsAgo = DateOnly.FromDateTime(DateTime.Today).AddYears(-16);
            if (person.Birthday.CompareTo(sixteenYearsAgo) >= 0)
                throw new BadRequestException("The person must be at least 16 years old");

            person.Status = StatusEnum.Pending;

            var PersonData = mapper.Map<Person>(person);
            await repository.CreateAsync(PersonData);

            var Response = mapper.Map<PersonResponse>(PersonData);
            return Response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _ = await GetByIdAsync(id) ?? throw new NotFoundException("ID not found");
            var result = await repository.DeleteAsync(id);
            return result;

        }

        public async Task<List<PersonResponse>> GetAllAsync()
        {
            var PeopleData = await repository.GetAllAsync();
            var Response = mapper.Map<List<PersonResponse>>(PeopleData);
            return Response;
        }

        public async Task<PersonResponse?> GetByIdAsync(int id)
        {
            var PersonData = await repository.GetByIdAsync(id) ?? throw new NotFoundException("ID not found");
            var Response = mapper.Map<PersonResponse>(PersonData);
            return Response;
        }

        public async Task<bool> UpdateAsync(PersonUpdateRequest person, int id)
        {
            var IdExist = await GetByIdAsync(id) ?? throw new NotFoundException("ID not found");
            if (string.IsNullOrWhiteSpace(person.Name))
                throw new BadRequestException("Name field cannot be empty");

            if (string.IsNullOrWhiteSpace(person.Cpf))
                throw new BadRequestException("CPF field cannot be empty");

            var CpfExist = await repository.GetByCpfAsync(person.Cpf);
            if (CpfExist != null && id != CpfExist.Id) throw new BadRequestException("CPF is already registered for another person");

            var sixteenYearsAgo = DateOnly.FromDateTime(DateTime.Today).AddYears(-16);
            if (person.Birthday.CompareTo(sixteenYearsAgo) >= 0)
                throw new BadRequestException("The person must be at least 16 years old");

            var updatedPerson = mapper.Map<Person>(person) ?? throw new Exception("Error mapping data");
            updatedPerson.Id = id;
            repository.Detach(mapper.Map<Person>(IdExist));
            var result = await repository.UpdateAsync(updatedPerson);
            return result;

        }

        public async Task<bool> UpdateStatusAsync(StatusEnum status, int id)
        {
            var result = await repository.UpdateStatus(status, id);
            if(!result) throw new Exception("Update fail");
            return result;
        }
    }
}
