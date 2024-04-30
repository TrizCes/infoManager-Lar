using infoManager.Models.Enums;

namespace infoManager.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }

        public virtual Person Person { get; set; }
    }
}
