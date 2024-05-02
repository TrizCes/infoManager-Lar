using AutoMapper;
using infoManager.Models;
using infoManagerAPI.DTO.PhoneNumber.Request;

namespace infoManagerAPI.Mapper.PhoneNumberMapper
{
    public class PhoneRequestToProfilePhoneNumber : Profile
    {
        public PhoneRequestToProfilePhoneNumber()
        {
            CreateMap<PhoneNumberRequest, PhoneNumber>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.PersonId, y => y.MapFrom( z => z.PersonId))
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Number))
                .ForMember(x => x.Type.ToString(), y => y.MapFrom(z => z.Type))
                .ReverseMap();
        }
    }
}
