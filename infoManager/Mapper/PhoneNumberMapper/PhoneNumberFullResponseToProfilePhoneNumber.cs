﻿using AutoMapper;
using infoManagerAPI.Models;
using infoManagerAPI.DTO.PhoneNumber.Response;

namespace infoManagerAPI.Mapper.PhoneNumberMapper
{
    public class PhoneNumberFullResponseToProfilePhoneNumber : Profile
    {
        public PhoneNumberFullResponseToProfilePhoneNumber()
        {
            CreateMap<PhoneNumberFullResponse, PhoneNumber>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.PersonId, y => y.Ignore())
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Number))
                .ForMember(x => x.Type, y => y.MapFrom(z => z.Type))
                .ReverseMap();
        }
    }
}
