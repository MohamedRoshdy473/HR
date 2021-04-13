using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using HR.Models;

using HR.DTO;
using HR.Domain.Services;

namespace HrAPI.Controllers
{
    //[Authorize(AuthenticationSchemes =
    //JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            return _employeeService.GetAll();
        }
        [HttpGet("{id}")]
        public EmployeeDTO GetEmployee(int id)
        {
            return _employeeService.Get(id);
        }
        [HttpGet]
        [Route("EmployeeByProfession/{EmpId}")]
        public IEnumerable<EmployeeDTO> EmployeeByProfession(int EmpId)
        {
            return _employeeService.GetAllEmployeesByCurrentEmployee(EmpId);
        }
        [HttpGet]
        [Route("GetAllEmployeesByProfession/{ProfessionId}")]
        public IEnumerable<EmployeeDTO> GetAllEmployeesByProfession(int ProfessionId)
        {
            return _employeeService.GetAllEmployeesByProfession(ProfessionId);
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(int EmployeeId, Employee employee)
        {
            try
            {
                _employeeService.UpdateEmployee(EmployeeId, employee);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();           
        }

        [HttpPost]
        public int PostEmployee(Employee employee)
        {
            _employeeService.AddEmployee(employee);
            return employee.ID;
        }
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }
        [HttpPost]
        [Route("UploadImage")]
        public ActionResult UploadFile(IFormFile file)
        {
            var ImagesTypes = new List<string>() { "image/jpg", "image/jpeg", "image/png" };
            var FileTypes = new List<string>() { "application/pdf", "application/doc", "application/docs" };
            //var user = GetCurrentUserAsync();
            //var emp = _context.Employees.Where(e => e.Email == user.Result.Email).FirstOrDefault();
            string path;
            if (ImagesTypes.Contains(file.ContentType))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", file.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            }
            else //if(FileTypes.Contains(file.ContentType))
            {
                //path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", file.FileName);
                //using (Stream stream = new FileStream(path, FileMode.Create))
                //{
                //    file.CopyTo(stream);
                //}
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getImage/{ImageName}")]
        public IActionResult ImageGet(string ImageName)
        {
            //ImageName = "#6M@CX79)G77LT&9F&G8^P0XYA2^YNE9J2GO^WCA.jpg";
            if (ImageName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/images", ImageName);

            var memory = new MemoryStream();
            var ext = System.IO.Path.GetExtension(path);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            return File(memory, contentType, Path.GetFileName(path));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getFile/{FName}")]
        public IActionResult getFile(string FName)
        {
            //FName = "H4QV1OHX0A7H5ZQ1EEE4I004TMKRBF79XTZONS1J.jpg";
            if (FName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/leaves/", FName);

            var memory = new MemoryStream();
            var ext = System.IO.Path.GetExtension(path);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/pdf";
            //return File(Path.GetFileName(path), contentType, FName);
            return File(memory, contentType, Path.GetFileName(path));
        }
    }
}
