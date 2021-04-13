using HR.Domain.Repositories;
using HR.DTO;
using HR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HR.Core.Repositories
{
    public class ProfessionsRepository : IProfessionsRepository
    {
        private readonly ApplicationDbContext _context;
        private string msg;

        public ProfessionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Profession profession)
        {
            try
            {
                if (profession != null)
                {
                    _context.Add(profession);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Profession doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        public void Delete(int ProfessionId)
        {
            var profession = _context.Professions.Find(ProfessionId);
            try
            {
                if (profession != null)
                {
                    _context.Professions.Remove(profession);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Profession doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        public Profession Get(int id)
        {
            var profession = _context.Professions.Find(id);

            if (profession == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Profession doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return profession;

            }
        }

        public IEnumerable<ProfessionDTO> GetAll()
        {
            var profs = _context.Professions.Include(p => p.Manager).Select(prof => new ProfessionDTO
            {
                ID = prof.ID,
                ManagerID = prof.ManagerID!=null? (int)prof.ManagerID:0,
                ManagerName = prof.Manager.Name,
                Name = prof.Name
            }).ToList();
            return profs;
        }

        public IEnumerable<ProfessionDTO> GetProfessionByEmployeeId(int EmployeeId)
        {
            var profession = _context.Professions.Where(p => p.ManagerID == EmployeeId).Select(prof => new ProfessionDTO
            {
                ID = prof.ID,
                Name = prof.Name
            }).ToList();
            return profession;
        }

        public void Update(int ProfessionId, Profession profession)
        {
            try
            {
                if (profession != null)
                {
                    _context.Entry(profession).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Profession doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}
