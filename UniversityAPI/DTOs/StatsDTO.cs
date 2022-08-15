namespace UniversityAPI.DTOs
{
    public class StatsDTO
    {
        public int AcademyLevel { get; set; }
        public int TeachersLevels { get; set; }
        public int Emviroment { get; set; }
        public int FlexibleHours { get; set; }// rated between 1-5
        public int Time { get; set; }
        public int PublicTransportAccesibility { get; set; }
    }
}
