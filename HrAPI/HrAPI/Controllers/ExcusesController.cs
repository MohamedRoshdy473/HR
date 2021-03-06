using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrAPI.Models;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HrAPI.DTO;

namespace HrAPI.Controllers
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
        private Employee CurrentEmp;

        public ExcusesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
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
        public async Task<ActionResult<IEnumerable<ExcuseDTO>>> GetExcuses()
        {
            return await _context.Excuses.Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToListAsync();
        }
        [Route("ApprovedExcuses")]
        public async Task<ActionResult<IEnumerable<ExcuseDTO>>> GetApprovedExcuses()
        {
            return await _context.Excuses.Where(ex => ex.Approved == "approved").Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToListAsync();
        }
        [Route("DisApprovedExcuses")]
        public async Task<ActionResult<IEnumerable<ExcuseDTO>>> GetDisApprovedExcuses()
        {
            return await _context.Excuses.Where(ex => ex.Approved == "disapproved").Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToListAsync();
        }
        [Route("PendingExcuses")]
        public async Task<ActionResult<IEnumerable<ExcuseDTO>>> GetPendingExcuses()
        {
            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            //var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            //var CurrentEmp = _context.Employees.Where(e => e.Email == email).FirstOrDefault();
            var ex = _context.Excuses.Where(ex =>
             ex.Date.Date.Year == DateTime.Today.Date.Year
             && ex.Date.Date.Month == DateTime.Today.Date.Month
             && ex.Date.Date.Day >= DateTime.Today.Date.Day && ex.Approved == "pending")
                 .Where(ex => ex.Employee.Profession.ManagerID == CurrentEmployee().ID)
                 .Select(ex => new ExcuseDTO
                 {
                     ID = ex.ID,
                     Approved = ex.Approved,
                     Comment = ex.Comment,
                     Date = ex.Date,
                     ProfessionID = ex.Employee.ProfessionID,
                     ProfessionName = ex.Employee.Profession.Name,
                     EmployeeName = ex.Employee.Name,
                     Hours = ex.Hours,
                     Time = ex.Time
                 }).ToListAsync();
            return await ex;
        }

        //[HttpGet("{id}")]
        [Route("AcceptExcuse/{id}")]
        public ActionResult AcceptExcuse(int id)
        {
            var excuse = _context.Excuses.Find(id);

            if (excuse == null)
            {
                return NotFound();
            }
            excuse.Approved = "approved";
            _context.SaveChanges();
            return Ok();
        }
        [Route("RejectExcuse/{id}")]
        public ActionResult RejectExcuse(int id)
        {
            var excuse = _context.Excuses.Find(id);

            if (excuse == null)
            {
                return NotFound();
            }
            excuse.Approved = "disapproved";
            _context.SaveChanges();
            return Ok();
        }
        [Route("PreviousExcuses")]
        public async Task<ActionResult<IEnumerable<ExcuseDTO>>> PreviousExcuses()
        {
            var emp = new Employee();
            List<ExcuseDTO> lstExecuses = new List<ExcuseDTO>();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var lstUsers = _context.Employees.Where(e => e.Email == email).ToList();
            if (lstUsers.Count > 0)
            {
                emp = lstUsers[0];


                lstExecuses = await _context.Excuses.Where(ex => ex.Employee.Email == emp.Email).Select(ex => new ExcuseDTO
                {
                    ID = ex.ID,
                    Approved = ex.Approved,
                    Comment = ex.Comment,
                    Date = ex.Date,
                    ProfessionName = ex.Employee.Profession.Name,
                    EmployeeName = ex.Employee.Name,
                    Hours = ex.Hours,
                    Time = ex.Time
                }).ToListAsync();
            }
            return lstExecuses;
        }
        //GET: api/Excuses/5
        [HttpGet("{id}")]
        public ActionResult<ExcuseDTO> GetExcuse(int id)
        {
            var ex = _context.Excuses.Include(e => e.Employee).ThenInclude(e => e.Profession)
                .FirstOrDefault(e => e.ID == id);

            if (ex == null)
            {
                return NotFound();
            }
            ExcuseDTO excuseDTO = new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            };

            return excuseDTO;
        }

        // PUT: api/Excuses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExcuse(int id, Excuse excuse)
        {
            if (id != excuse.ID)
            {
                return BadRequest();
            }

            var Old = _context.Excuses.Include(e => e.Employee).ThenInclude(e => e.Profession)
                 .FirstOrDefault(e => e.ID == id);
            //Excuse NewExcuse = new Excuse
            //{
            //    Approved = Old.Approved,
            //    Comment = Old.Comment,
            //    Date = Old.Date,
            //    Employee = Old.Employee,
            //    EmployeeID = Old.EmployeeID,
            //    Hours = Old.Hours,
            //    Time = Old.Time
            //};
            excuse.EmployeeID = Old.EmployeeID;
            excuse.Employee = Old.Employee;
            _context.Entry(Old).State = EntityState.Detached;
            _context.Entry(excuse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcuseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        // POST: api/Excuses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Excuse>> PostExcuse(Excuse excuse)
        {
            if (excuse.EmployeeID == 0)
            {
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var lstUsers = _context.Employees.Where(e => e.Email == email).ToList();
                if (lstUsers.Count > 0)
                {
                    var emp = lstUsers[0];
                    excuse.Employee = emp;
                    excuse.EmployeeID = emp.ID;
                }
            }

            _context.Excuses.Add(excuse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExcuse", new { id = excuse.ID }, excuse);
        }

        // DELETE: api/Excuses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Excuse>> DeleteExcuse(int id)
        {
            var excuse = await _context.Excuses.FindAsync(id);
            if (excuse == null)
            {
                return NotFound();
            }

            _context.Excuses.Remove(excuse);
            await _context.SaveChangesAsync();

            return excuse;
        }

        private bool ExcuseExists(int id)
        {
            return _context.Excuses.Any(e => e.ID == id);
        }
    }
}
