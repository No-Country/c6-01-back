using System.Text.Json.Serialization;

namespace UniversityAPI.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int StudentAmount { get; set;}

      
        public List<Faculty>? Faculties { get; set; } = new List<Faculty>();
    }
}
