using infoManager.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace infoManagerAPI.DTO.PhoneNumber.Request
{
    public class PhoneNumberRequest
    {
        [Required(ErrorMessage = "Person Id is required")]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Number is required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [Range(0, 3, ErrorMessage = "Mobile = 0, Residential = 1, Commercial = 2, EmergencyContact = 3")]
        public PhoneType Type { get; set; }
    }
}
