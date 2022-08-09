namespace UniversityAPI.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int UniversityId { get; set; }
        public University university { get; set; }

        public List<Career> Careers { get; set; }


    }
}
