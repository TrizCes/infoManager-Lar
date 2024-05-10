using infoManagerAPI.Models.Enums;
using infoManagerAPI.Models;
using System.ComponentModel.DataAnnotations;
namespace infoManagerAPI.DTO.Person.Request
{
    public class PersonRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF is required")]
        [MaxLength(11)]
        [MinLength(11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateOnly), "01/01/1900", "31/12/2050", ErrorMessage = "The date entered must be valid")]
        public DateOnly Birthday { get; set; }
    }
}
