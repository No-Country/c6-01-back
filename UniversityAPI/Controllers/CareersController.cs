using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.DTOs;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        private readonly DataContext _context;

        public CareersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Careers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Career>>> GetCareers()
        {
            if (_context.Careers == null)
            {
                return NotFound();
            }
            return await _context.Careers.Include(x => x.stats).ToListAsync();
        }

        // GET: api/Careers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Career>> GetCareer(int id)
        {
            if (_context.Careers == null)
            {
                return NotFound();
            }
            var career = await _context.Careers.FindAsync(id);

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }

        // PUT: api/Careers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCareer(int id,CareerDTO careerDTO)
        {
            Career? career = _context.Careers.Find(id);

           
            if (career == null)
            {
                return BadRequest();
            }

            career.Name = careerDTO.Name;

            career.Faculty= _context.Faculty.FirstOrDefault(f=>f.Name==careerDTO.FacultyName);

            career.FacultyId = career.Faculty.Id;

            await _context.SaveChangesAsync();



            return Ok(career);
        }

        // POST: api/Careers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Career>>> PostCareer(CareerDTO careerDTO)
        {
            Faculty faculty = _context.Faculty.FirstOrDefault(u => u.Name == careerDTO.FacultyName);
            if (_context.Faculty == null)
            {
                return Problem("Entity set 'DataContext.Faculty'  is null.");
            }

            if (faculty == null)
                return Problem(string.Empty);
            Career career = new Career
            {

                Name = careerDTO.Name,
                Faculty = faculty,
                FacultyId = faculty.Id,

            };


            faculty?.Careers?.Add(career);
            await _context.SaveChangesAsync();

            return Ok(_context.Faculty.Include(x => x.Careers).ToList());
        }

        // DELETE: api/Careers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareer(int id)
        {
            if (_context.Careers == null)
            {
                return NotFound();
            }
            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }

            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CareerExists(int id)
        {
            return (_context.Careers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
