using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HR.Domain.Services;
using HR.Data.Models;
using HR.Data.DTO;

namespace HR.API.Controllers
{
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcusesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IExcuseService _excuseService;
        public ExcusesController(IExcuseService excuseService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
            _excuseService = excuseService;
            this.httpContextAccessor = httpContextAccessor;

        }
        private Employee CurrentEmployee()
        {
            var CurrentEmp = new Employee();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var lstUsers = _context.Employees.Where(e => e.Email == email).ToList();
            if (lstUsers.Count > 0)
            {
                CurrentEmp = lstUsers[0];
            }
            return CurrentEmp;
        }
        // GET: api/Excuses
        [HttpGet]
        public IEnumerable<ExcuseDTO> GetExcuses()
        {
            return _excuseService.GetAllExcuses();
        }

        [Route("GetExcusesForReport")]
        public IEnumerable<ReportExcuseDTO> GetExcusesForReport()
        {
            return _excuseService.GetExcusesForReport();
        }

        [Route("GetExcusesByProfessionId/{ProfessionId}")]
        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionId(int ProfessionId)
        {
            return _excuseService.GetExcusesByProfessionId(ProfessionId);
        }

        [Route("GetExcusesByProfessionIdAndEmployeeId/{ProfessionId}/{EmployeeId}")]
        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeId(int ProfessionId, int EmployeeId)
        {
            return _excuseService.GetExcusesByProfessionIdAndEmployeeId(ProfessionId, EmployeeId);
        }

        [Route("GetExcusesByProfessionIdAndEmployeeIdAndDate/{ProfessionId}/{EmployeeId}/{startDate}/{endDate}")]
        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeIdAndDate(int ProfessionId, int EmployeeId, DateTime startDate, DateTime endDate)
        {
            return _excuseService.GetExcusesByProfessionIdAndEmployeeIdAndDate(ProfessionId, EmployeeId, startDate, endDate);
        }

        [Route("GetExcusesByManager/{EmployeeId}")]
        public IEnumerable<ExcuseDTO> GetExcusesByManager(int EmployeeId)
        {
           return  _excuseService.GetExcusesByManager(EmployeeId);
        }

        [Route("ApprovedExcuses")]
        public IEnumerable<ExcuseDTO> GetApprovedExcuses()
        {
            return _excuseService.GetApprovedExcuses();
        }

        [Route("ApprovedExcusesByManager/{EmployeeId}")]
        public IEnumerable<ExcuseDTO> GetApprovedExcusesByManager(int EmployeeId)
        {
            return _excuseService.GetApprovedExcusesByManager(EmployeeId);
        }

        [Route("DisApprovedExcuses")]
        public IEnumerable<ExcuseDTO>GetDisApprovedExcuses()
        {
            return _excuseService.GetDisApprovedExcuses();
        }

        [Route("DisApprovedExcusesByManager/{EmployeeId}")]
        public IEnumerable<ExcuseDTO> GetDisApprovedExcusesByManager(int EmployeeId)
        {
            return _excuseService.GetDisApprovedExcusesByManager(EmployeeId);
        }

        [Route("PendingExcuses/{EmployeeId}")]
        public IEnumerable<ExcuseDTO> GetPendingExcuses(int EmployeeId)
        {
            return _excuseService.GetPendingExcusesByManager(EmployeeId);
        }

        [Route("PendingExcusesByHR")]
        public IEnumerable<ExcuseDTO> GetPendingExcusesByHR()
        {
            return _excuseService.GetPendingExcusesByHR();
        }

        //[HttpGet("{id}")]
        [Route("AcceptExcuse/{id}")]
        public ActionResult AcceptExcuse(int id)
        {
            _excuseService.AcceptExcuse(id);
            return Ok();
        }

        [Route("RejectExcuse/{id}")]
        public ActionResult RejectExcuse(int id)
        {
            _excuseService.RejectExcuse(id);
            return Ok();
        }

        //GET: api/Excuses/5
        [HttpGet("{id}")]
        public ActionResult<ExcuseDTO> GetExcuse(int id)
        {
          return  _excuseService.GetExcuse(id);
        }

        [Route("GetExcuseByEmployeeId/{EmployeeId}")]
        public Boolean GetExcuseByEmployeeId(int EmployeeId)
        {
           return _excuseService.GetExcuseByEmployeeId(EmployeeId);
        }

        [Route("GetAllExcuseForEmployeeId/{EmployeeId}")]
        public IEnumerable<ExcuseDTO> GetAllExcuseForEmployeeId(int EmployeeId)
        {
            return _excuseService.GetAllExcuseForEmployeeId(EmployeeId);
        }
        // PUT: api/Excuses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPut("{id}")]
        public IActionResult PutExcuse(int id, Excuse excuse)
        {
            _excuseService.UpdateExcuse(id, excuse);
            return CreatedAtAction("GetExcuse", new { id = excuse.ID }, excuse);
        }

        // POST: api/Excuses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Excuse> PostExcuse(Excuse excuse)
        {
            _excuseService.AddExcuse(excuse);

            return CreatedAtAction("GetExcuse", new { id = excuse.ID }, excuse);
        }

        // DELETE: api/Excuses/5
        [HttpDelete("{id}")]
        public ActionResult<Excuse> DeleteExcuse(int id)
        {
            _excuseService.DeleteExcuse(id);
            return Ok();
        }
    }
}
