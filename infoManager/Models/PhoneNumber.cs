using infoManager.Models.Enums;
using System.Text.Json.Serialization;

namespace infoManager.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }
    }
}
