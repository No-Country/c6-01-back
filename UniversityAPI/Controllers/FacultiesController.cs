using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public FacultiesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Faculties
        [HttpGet]
        public async Task<ActionResult<List<FacultyDTO>>> GetFaculty()
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculties = await _context.Faculties.Include(f=> f.Careers).ThenInclude(f => f.stats).ToListAsync();

            return _mapper.Map<List<FacultyDTO>>(faculties);
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyDTO>> GetFaculty(int id)
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }

            return _mapper.Map<FacultyDTO>(faculty);
        }

        // PUT: api/Faculties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculty(int id, FacultyCreationDTO facultyCreationDTO)
        {
            var exists = await _context.Faculties.AnyAsync(x => x.Id == id);


            if (!exists)
            {
                return BadRequest();
            }

            var faculty = _mapper.Map<Faculty>(facultyCreationDTO);
            faculty.Id = id;


            _context.Update(faculty);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Faculties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFaculty(FacultyCreationDTO facultyCreationDTO)
        {
            var universityExists = await _context.Universities.AnyAsync(x => x.Id == facultyCreationDTO.UniversityId);

            if (!universityExists) { return BadRequest(); }

            var faculty = _mapper.Map<Faculty>(facultyCreationDTO);

            _context.Add(faculty);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Faculties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultyExists(int id)
        {
            return (_context.Faculties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
