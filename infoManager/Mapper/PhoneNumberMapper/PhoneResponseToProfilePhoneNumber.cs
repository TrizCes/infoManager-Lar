using AutoMapper;
using infoManager.Models;
using infoManagerAPI.DTO.Person.Request;
using infoManagerAPI.DTO.PhoneNumber.Response;

namespace infoManagerAPI.Mapper.PhoneNumberMapper
{
    public class PhoneResponseToProfilePhoneNumber : Profile
    {
        public PhoneResponseToProfilePhoneNumber()
        {
            CreateMap<PhoneNumberResponse, PhoneNumber>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.PersonId, y => y.Ignore())
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Number))
                .ForMember(x => x.Type.ToString(), y => y.MapFrom(z => z.Type))
                .ReverseMap();
        }
    }
}
