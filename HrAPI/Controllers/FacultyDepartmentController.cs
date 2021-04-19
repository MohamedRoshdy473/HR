using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR.Domain.Services;
using HR.DTO;
using HR.Models;

namespace HrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyDepartmentController : ControllerBase
    {
        private readonly IFacultyDepartmentService _facultyDepartmentService;

        public FacultyDepartmentController(IFacultyDepartmentService facultyDepartmentService)
        {
            _facultyDepartmentService = facultyDepartmentService;
        }

        // GET: api/FacultyDepartment
        [HttpGet]
        public IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentDTO()
        {
            return _facultyDepartmentService.GetAllFacultyDepartments();
        }

        // GET: api/FacultyDepartment/5
        [HttpGet("{id}")]
        public ActionResult<FacultyDepartmentDTO> GetFacultyDepartmentDTO(int id)
        {
            return _facultyDepartmentService.GetFacultyDepartment(id);
        }
        [HttpGet]
        [Route("GetFacultyDepartmentsByFacultyId/{FacultyId}")]
        public IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentsByFacultyId(int FacultyId)
        {
            return _facultyDepartmentService.GetFacultyDepartmentsByFacultyId(FacultyId);
        }
        // PUT: api/FacultyDepartment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutFacultyDepartmentDTO(int id, FacultyDepartmentDTO facultyDepartmentDTO)
        {
            try
            {
                _facultyDepartmentService.UpdateFacultyDepartment(id, facultyDepartmentDTO);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }

        // POST: api/FacultyDepartment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<FacultyDepartmentDTO> PostFacultyDepartmentDTO(FacultyDepartmentDTO facultyDepartmentDTO)
        {
            _facultyDepartmentService.AddFacultyDepartment(facultyDepartmentDTO);

            return CreatedAtAction("GetFacultyDepartmentDTO", new { id = facultyDepartmentDTO.Id }, facultyDepartmentDTO);
        }

        // DELETE: api/FacultyDepartment/5
        [HttpDelete("{id}")]
        public ActionResult<FacultyDepartment> DeleteFacultyDepartmentDTO(int id)
        {
            try
            {
                _facultyDepartmentService.DeleteFacultyDepartment(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }

    }
}
