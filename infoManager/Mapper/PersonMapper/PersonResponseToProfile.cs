using AutoMapper;
using infoManagerAPI.Models;
using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.DTO.Person.Response;

namespace infoManagerAPI.Mapper.PersonMapper
{
    public class PersonResponseToProfile : Profile
    {
        public PersonResponseToProfile()
        {
            CreateMap<PersonResponse, Person>()
                .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
                .ForMember(x => x.Name, y => y.MapFrom(src => src.Name))
                .ForMember(x => x.Cpf, y => y.MapFrom(src => src.Cpf))
                .ForMember(x => x.Birthday, y => y.MapFrom(src => src.Birthday))
                .ForMember(x => x.Status, y => y.MapFrom(src => src.Status))
                .ForMember(x => x.PhoneNumbers, y => y.MapFrom(src => src.Phones))
                .ReverseMap();
        }
    }
}

