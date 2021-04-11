using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using HR.Service.Services.ProfessionsServices;
using HR.Models;
using HR.DTO;

namespace HrAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionsService _professionsService;

        public ProfessionsController(IProfessionsService professionsService)
        {
            _professionsService = professionsService;
        }

        // GET: api/Professions
        [HttpGet]
        public IEnumerable<ProfessionDTO> GetProfessions()
        {
            return _professionsService.GetAllProfessions();
        }

        // GET: api/Professions/5
        [HttpGet("{id}")]
        public ActionResult<Profession> GetProfession(int id)
        {
            return _professionsService.Getprofession(id);
        }

        // PUT: api/Professions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutProfession(int id, Profession profession)
        {
            try
            {
                _professionsService.Updateprofession(id, profession);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }
            return NoContent();
        }

        // POST: api/Professions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Profession> PostProfession(Profession profession)
        {
            _professionsService.Addprofession(profession);

            return CreatedAtAction("GetProfession", new { id = profession.ID }, profession);
        }

        // DELETE: api/Professions/5
        [HttpDelete("{id}")]
        public ActionResult<Profession> DeleteProfession(int id)
        {
            try
            {
                _professionsService.Deleteprofession(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
            }

            return NoContent();
        }
        [Route("GetProfessionByEmployeeId/{EmployeeId}")]
        public IEnumerable<ProfessionDTO> GetProfessionByEmployeeId(int EmployeeId)
        {
            return _professionsService.GetProfessionByEmployeeId(EmployeeId);
        }
    }
}
