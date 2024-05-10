using System.ComponentModel.DataAnnotations;

namespace infoManagerAPI.DTO.User.Request
{
    public class PasswordRequest
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirm is required")]
        public string ConfirmPassword { get; set; }
    }
}
