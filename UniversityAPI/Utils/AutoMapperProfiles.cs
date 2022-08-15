using AutoMapper;
using UniversityAPI.DTOs;
using UniversityAPI.Models;

namespace UniversityAPI.Util
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<University, UniversityDTO>()
                .ForMember(dest => dest, opt => opt.MapFrom(MapStatsUniversity));
            CreateMap<UniversityCreationDTO, University>();

            CreateMap<Faculty, FacultyDTO>()
                .ForMember(dest => dest.Stats, opt => opt.MapFrom(MapStatsFaculty));
            CreateMap<FacultyCreationDTO, Faculty>();

            CreateMap<Career, CareerDTO>()
                .ForMember(dest => dest.Stats, opt => opt.MapFrom(MapStatsCareer));
            CreateMap<CareerCreationDTO, Career>();

            CreateMap<Stats, StatsDTO>();
            CreateMap<StatsCreationDTO, Stats>();
        }

        private StatsDTO MapStatsCareer(Career career, CareerDTO careerDTO)
        {
            var result = new StatsDTO();

            if(career.stats.Count > 0)
            {
                result.TeachersLevels = (int) career.stats.Average(x => x.TeachersLevels);
                result.AcademyLevel = (int)career.stats.Average(x => x.AcademyLevel);
                result.Emviroment = (int)career.stats.Average(x => x.Emviroment);
                result.Time = (int)career.stats.Average(x => x.Time);
                result.FlexibleHours = (int)career.stats.Average(x => x.FlexibleHours);
                result.PublicTransportAccesibility = (int)career.stats.Average(x => x.PublicTransportAccesibility);
            }

            return result;
        }

        private StatsDTO MapStatsFaculty(Faculty faculty, FacultyDTO facultyDTO)
        {
            var result = new StatsDTO();

            if(faculty.Careers.Count > 0)
            {
                //Suma el promedio de las estadisticas de cada carrera a su respectiva variable en las estadisticas de la facultad,
                //luego se divide por cantidad de carreras
                foreach(var career in faculty.Careers)
                {
                    if(career.stats.Count > 0)
                    {
                        result.TeachersLevels += (int)career.stats.Average(x => x.TeachersLevels);
                        result.AcademyLevel += (int)career.stats.Average(x => x.AcademyLevel);
                        result.Emviroment += (int)career.stats.Average(x => x.Emviroment);
                        result.Time += (int)career.stats.Average(x => x.Time);
                        result.FlexibleHours += (int)career.stats.Average(x => x.FlexibleHours);
                        result.PublicTransportAccesibility += (int)career.stats.Average(x => x.PublicTransportAccesibility);
                    }
                }

                var numberCareersInFaculty = faculty.Careers.Count;
                result.TeachersLevels = result.TeachersLevels / numberCareersInFaculty;
                result.AcademyLevel = result.AcademyLevel / numberCareersInFaculty;
                result.Emviroment = result.Emviroment / numberCareersInFaculty;
                result.Time = result.Time / numberCareersInFaculty;
                result.FlexibleHours = result.FlexibleHours / numberCareersInFaculty;
                result.PublicTransportAccesibility = result.PublicTransportAccesibility / numberCareersInFaculty;
            }

            return result;
        }

        private StatsDTO MapStatsUniversity(University university, UniversityDTO universityDTO)
        {
            var result = new StatsDTO();

            if(university.Faculties.Count > 0)
            {
                var facultyStats = new List<StatsDTO>();

                foreach (var faculty in university.Faculties)
                {
                    var resultFaculty = new StatsDTO();


                    if (faculty.Careers.Count > 0)
                    {
                        foreach (var career in faculty.Careers)
                        {
                            if (career.stats.Count > 0)
                            {
                                resultFaculty.TeachersLevels += (int)career.stats.Average(x => x.TeachersLevels);
                                resultFaculty.AcademyLevel += (int)career.stats.Average(x => x.AcademyLevel);
                                resultFaculty.Emviroment += (int)career.stats.Average(x => x.Emviroment);
                                resultFaculty.Time += (int)career.stats.Average(x => x.Time);
                                resultFaculty.FlexibleHours += (int)career.stats.Average(x => x.FlexibleHours);
                                resultFaculty.PublicTransportAccesibility += (int)career.stats.Average(x => x.PublicTransportAccesibility);
                            }
                        }

                        var numberCareersInFaculty = faculty.Careers.Count;
                        resultFaculty.TeachersLevels = resultFaculty.TeachersLevels / numberCareersInFaculty;
                        resultFaculty.AcademyLevel = resultFaculty.AcademyLevel / numberCareersInFaculty;
                        resultFaculty.Emviroment = resultFaculty.Emviroment / numberCareersInFaculty;
                        resultFaculty.Time = resultFaculty.Time / numberCareersInFaculty;
                        resultFaculty.FlexibleHours = resultFaculty.FlexibleHours / numberCareersInFaculty;
                        resultFaculty.PublicTransportAccesibility = resultFaculty.PublicTransportAccesibility / numberCareersInFaculty;
                    }
                    facultyStats.Add(resultFaculty);
                }

                result.TeachersLevels = (int) facultyStats.Average(x => x.TeachersLevels);
                result.AcademyLevel = (int)facultyStats.Average(x => x.AcademyLevel);
                result.Emviroment = (int)facultyStats.Average(x => x.Emviroment);
                result.Time = (int)facultyStats.Average(x => x.Time);
                result.FlexibleHours = (int)facultyStats.Average(x => x.FlexibleHours);
                result.PublicTransportAccesibility = (int)facultyStats.Average(x => x.PublicTransportAccesibility);
            }

            return result;
        }
    }
}
