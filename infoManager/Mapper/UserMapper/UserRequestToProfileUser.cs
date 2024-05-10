using AutoMapper;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.Models;

namespace infoManagerAPI.Mapper.UserMapper
{
    public class UserRequestToProfileUser : Profile
    {
        public UserRequestToProfileUser()
        {
            CreateMap<UserRequest, User>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.Password, y => y.MapFrom(z => z.Password))
                .ForMember(x => x.Role, y => y.MapFrom(z => z.Role))
                .ReverseMap();
        }
    }
}
