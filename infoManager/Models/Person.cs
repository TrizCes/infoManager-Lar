using infoManagerAPI.Models.Enums;

namespace infoManagerAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf {  get; set; }
        public DateOnly Birthday { get; set; }
        public StatusEnum Status {  get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

    }
}
