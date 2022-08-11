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
    public class FacultiesController : ControllerBase
    {
        private readonly DataContext _context;

        public FacultiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Faculties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculty()
        {
            if (_context.Faculty == null)
            {
                return NotFound();
            }
            return await _context.Faculty.Include(f=> f.Careers).ThenInclude(f => f.stats).ToListAsync();
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
            if (_context.Faculty == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculty.FindAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

        // PUT: api/Faculties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculty(int id, FacultyDTO facultyDTO)
        {
            Faculty? faculty = _context.Faculty.Find(id);


            if (faculty == null)
            {
                return BadRequest();
            }

            faculty.Name = facultyDTO.Name;

            faculty.Address = facultyDTO.Address;

            faculty.University = _context.Universities.FirstOrDefault(u=> u.Name==facultyDTO.UniversityName);

            faculty.UniversityId = faculty.University.Id;

            await _context.SaveChangesAsync();

            return Ok(faculty);
        }

        // POST: api/Faculties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Faculty>>> PostFaculty(FacultyDTO facultyDTO)
        {

            University university = _context.Universities.FirstOrDefault(u=> u.Name== facultyDTO.UniversityName);
            if (_context.Faculty == null)
            {
                return Problem("Entity set 'DataContext.Faculty'  is null.");
            }

            Faculty faculty = new Faculty
            {
               
                Name = facultyDTO.Name,
                Address = facultyDTO.Address,
                UniversityId=university.Id,
                University=university,
                Careers=null,
            };


            university?.Faculties?.Add(faculty);
            await _context.SaveChangesAsync();

            return Ok(await GetFaculty());
        }

        // DELETE: api/Faculties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            if (_context.Faculty == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculty.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.Faculty.Remove(faculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultyExists(int id)
        {
            return (_context.Faculty?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
