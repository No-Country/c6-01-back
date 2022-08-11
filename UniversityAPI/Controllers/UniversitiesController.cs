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
    public class UniversitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public UniversitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Universities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversities()
        {
            if (_context.Universities == null)
            {
                return NotFound();
            }
            return await _context.Universities.Include(u => u.Faculties).ThenInclude(u => u.Careers).ThenInclude(f => f.stats).ToListAsync();
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<University>> GetUniversity(int id)
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

            return university;
        }

        // PUT: api/Universities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUniversity(int id, UniversityDTO universityDTO)
        {
            University? university = _context.Universities.Find(id);


            if (university == null)
            {
                return BadRequest();
            }

            university.Name = universityDTO.Name;

            university.Address = universityDTO.Address;

            university.Image = universityDTO.Image;

            university.StudentAmount = universityDTO.StudentAmount;

            await _context.SaveChangesAsync();

            return Ok(university);
        }

        // POST: api/Universities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<University>>> PostUniversity(UniversityDTO universityDTO)
        {
            int num = _context.Universities.Where(u => u.Name == universityDTO.Name).Count();

            if (_context.Universities == null)
            {
                return Problem("Entity set 'DataContext.Universities'  is null.");
            }

            if (num > 0)
                return Problem("Into the DB has already exist the name of the university");

            University university = new University
            {
                Name = universityDTO.Name,
                Address = universityDTO.Address,
                Image = universityDTO.Image,
                StudentAmount = universityDTO.StudentAmount,
                Faculties = null,
            };



            _context.Universities.Add(university);
            await _context.SaveChangesAsync();

            return Ok(await GetUniversities());
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
