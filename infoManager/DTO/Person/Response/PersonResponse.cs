using infoManager.Models.Enums;
using infoManager.Models;

namespace infoManagerAPI.DTO.Person.Response
{
    public class PersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateOnly Birthday { get; set; }
        public StatusEnum Status { get; set; }
        public List<PhoneNumber> Phones { get; set; }
    }
}
