using System.Text.Json.Serialization;

namespace UniversityAPI.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
      
        public int UniversityId { get; set; } = 0;
        [JsonIgnore]
        public University University { get; set; } = new University();

        public List<Career> Careers { get; set; }


    }
}
