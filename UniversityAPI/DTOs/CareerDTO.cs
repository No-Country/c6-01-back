using UniversityAPI.Models;

namespace UniversityAPI.DTOs
{
    public class CareerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatsDTO Stats { get; set; }
    }
}
