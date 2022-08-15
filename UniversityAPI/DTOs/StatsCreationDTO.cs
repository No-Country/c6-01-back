using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTOs
{
    public class StatsCreationDTO
    {
        public int CareerId { get; set; }
        [Range(0, 100)]
        public int AcademyLevel { get; set; }

        [Range(0, 100)]
        public int TeachersLevels { get; set; }

        [Range(0, 100)]
        public int Emviroment { get; set; }

        [Range(0, 100)]
        public int FlexibleHours { get; set; }// rated between 1-5

        [Range(0, 100)]
        public int Time { get; set; }

        [Range(0, 100)]
        public int PublicTransportAccesibility { get; set; }
    }
}
