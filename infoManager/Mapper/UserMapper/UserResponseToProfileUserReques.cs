using AutoMapper;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Models;

namespace infoManagerAPI.Mapper.UserMapper
{
    public class UserResponseToProfileUserRequest : Profile
    {
        public UserResponseToProfileUserRequest()
        {
            CreateMap<UserResponse, UserRequest>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.Password, y => y.Ignore())
                .ForMember(x => x.Role, y => y.MapFrom(z => z.Role))
                .ReverseMap();
        }
    }
}
