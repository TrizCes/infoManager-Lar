using infoManagerAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace infoManagerAPI.DTO.User.Request
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public RoleEnum Role { get; set; }
    }
}
