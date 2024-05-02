using AutoMapper;
using infoManager.Models;
using infoManagerAPI.DTO.PhoneNumber.Request;

namespace infoManagerAPI.Mapper.PhoneNumberMapper
{
    public class PhoneRequestUpdateToProfilePhoneNumber : Profile
    {
        public PhoneRequestUpdateToProfilePhoneNumber()
        {
            CreateMap<PhoneNumberRequestUpdate, PhoneNumber>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.PersonId, y => y.Ignore())
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Number))
                .ForMember(x => x.Type, y => y.MapFrom(z => z.Type))
                .ReverseMap();
        }
    }
}
