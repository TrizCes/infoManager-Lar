using AutoMapper;
using infoManagerAPI.Models;
using infoManagerAPI.DTO.Person.Request;

namespace infoManagerAPI.Mapper.Request
{
    public class PersonUpdateRequestToPersonProfile : Profile
    {
        public PersonUpdateRequestToPersonProfile() 
        {
            CreateMap<PersonUpdateRequest, Person>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Name, y => y.MapFrom(src => src.Name))
                .ForMember(x => x.Cpf, y => y.MapFrom(src => src.Cpf))
                .ForMember(x => x.Birthday, y => y.MapFrom(src => src.Birthday))
                .ForMember(x => x.Status, y => y.Ignore())
                .ReverseMap();
        }
    }
}
