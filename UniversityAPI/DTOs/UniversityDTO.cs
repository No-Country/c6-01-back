using UniversityAPI.Models;

namespace UniversityAPI.DTOs
{
    public class UniversityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int StudentAmount { get; set; }
       
    }
}
