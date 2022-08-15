using AutoMapper;
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
        private readonly IMapper _mapper;

        public CareersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> PutCareer(int id,CareerCreationDTO careerCreationDTO)
        {
            bool careerExists = await _context.Careers.AnyAsync(x => x.Id == id);

            if (!careerExists)
            {
                return BadRequest();
            }

            Faculty faculty = _context.Faculties.FirstOrDefault(x => x.Id == careerCreationDTO.FacultyId);

            if (faculty == null) { return BadRequest(); }

            var career = _mapper.Map<Career>(careerCreationDTO);
            career.Id = id;

            _context.Update(career);
            await _context.SaveChangesAsync();
            return Ok(career);
        }

        // POST: api/Careers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCareer(CareerCreationDTO careerCreationDTO)
        {
            Faculty faculty = _context.Faculties.FirstOrDefault(x => x.Id == careerCreationDTO.FacultyId);
            if (_context.Faculties == null)
            {
                return Problem("Entity set 'DataContext.Faculty'  is null.");
            }

            if (faculty == null)
                return BadRequest();

            var career = _mapper.Map<Career>(careerCreationDTO);

            _context.Add(career);
            
            await _context.SaveChangesAsync();

            return Ok();
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
