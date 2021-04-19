using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR.Domain.Services;
using HR.Models;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversitiesController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        // GET: api/Universities
        [HttpGet]
        public IEnumerable<University> GetUniversities()
        {
            return _universityService.GetAllUniversities();
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public ActionResult<University> GetUniversity(int id)
        {
            var university = _universityService.GetUniversity(id);

            if (university == null)
            {
                return NotFound();
            }

            return university;
        }

        // PUT: api/Universities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutUniversity(int id, University university)
        {
            try
            {
                _universityService.UpdateUniversity(id, university);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }
            return NoContent();
        }

        // POST: api/Universities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<University> PostUniversity(University university)
        {
            _universityService.AddUniversity(university);
            return CreatedAtAction("GetUniversity", new { id = university.Id }, university);
        }

        // DELETE: api/Universities/5
        [HttpDelete("{id}")]
        public ActionResult<University> DeleteUniversity(int id)
        {
            try
            {
                _universityService.DeleteUniversity(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }
    }
}
