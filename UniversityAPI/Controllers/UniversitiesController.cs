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
    public class UniversitiesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UniversitiesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Universities
        [HttpGet]
        public async Task<ActionResult<List<UniversityDTO>>> GetUniversities()
        {
            if (_context.Universities == null)
            {
                return NotFound();
            }
            var universities = await _context.Universities.Include(x => x.Faculties).ThenInclude(x => x.Careers).ThenInclude(x => x.stats).ToListAsync();
            return _mapper.Map<List<UniversityDTO>>(universities);
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityDTO>> GetUniversity(int id)
        {
            if (_context.Universities == null)
            {
                return NotFound();
            }
            var university = await _context.Universities.
                Include(x => x.Faculties).ThenInclude(x => x.Careers).ThenInclude(x => x.stats).FirstOrDefaultAsync(x => x.Id == id);

            if (university == null)
            {
                return NotFound();
            }



            return _mapper.Map<UniversityDTO>(university);
        }

        // PUT: api/Universities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUniversity(int id, UniversityCreationDTO universityCreationDTO)
        {
            var universityExists = await _context.Universities.AnyAsync(x => x.Id == id);


            if (!universityExists)
            {
                return BadRequest();
            }

            var university = _mapper.Map<University>(universityCreationDTO);
            _context.Update(university);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Universities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUniversity(UniversityCreationDTO universityCreationDTO)
        {
            int num = _context.Universities.Where(u => u.Name == universityCreationDTO.Name).Count();

            if (_context.Universities == null)
            {
                return Problem("Entity set 'DataContext.Universities'  is null.");
            }

            if (num > 0)
                return Problem("Into the DB has already exist the name of the university");

            var university = _mapper.Map<University>(universityCreationDTO);

            _context.Add(university);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Universities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            if (_context.Universities == null)
            {
                return NotFound();
            }
            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();

            return Ok("university deleted successfully");
        }

        private bool UniversityExists(int id)
        {
            return (_context.Universities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
