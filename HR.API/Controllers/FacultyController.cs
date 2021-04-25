using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR.Domain.Services;
using HR.Data.DTO;
using HR.Data.Models;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        // GET: api/Faculty
        [HttpGet]
        public IEnumerable<FacultyDTO> GetFacultyDTO()
        {
            return _facultyService.GetAllFaculties();
        }

        // GET: api/Faculty/5
        [HttpGet("{id}")]
        public ActionResult<FacultyDTO> GetFacultyDTO(int id)
        {
            return _facultyService.GetFaculty(id);
        }
        [HttpGet]
        [Route("GetFacultiesByUniversityId/{UniversityId}")]
        public IEnumerable<FacultyDTO> GetFacultiesByUniversityId(int UniversityId)
        {
            return _facultyService.GetFacultiesByUniversityId(UniversityId);
        }
        // PUT: api/Faculty/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutFacultyDTO(int id, FacultyDTO facultyDTO)
        {
            try
            {
                _facultyService.UpdateFaculty(id, facultyDTO);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }

        // POST: api/Faculty
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<FacultyDTO> PostFacultyDTO(FacultyDTO facultyDTO)
        {
            _facultyService.AddFaculty(facultyDTO);

            return CreatedAtAction("GetFacultyDTO", new { id = facultyDTO.Id }, facultyDTO);
        }

        // DELETE: api/Faculty/5
        [HttpDelete("{id}")]
        public ActionResult<Faculty> DeleteFacultyDTO(int id)
        {
            try
            {
                _facultyService.DeleteFaculty(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }
    }
}
