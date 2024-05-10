using infoManagerAPI.Models.Enums;

namespace infoManagerAPI.DTO.User.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public RoleEnum Role { get; set; }
    }
}
