using System.Text.Json.Serialization;

namespace UniversityAPI.Models
{
    public class Career
    {
        public int Id { get; set; }
        public string Name { get; set; }
           
        public int FacultyId { get; set; }

        [JsonIgnore]
        public Faculty ? Faculty { get; set; }   

        public List<Stats> stats { get; set; }
    }
}
