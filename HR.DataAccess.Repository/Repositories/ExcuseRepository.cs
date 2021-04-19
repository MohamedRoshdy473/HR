using HR.Domain.Repositories;
using HR.DTO;
using HR.Models;
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
    public class ExcuseRepository : IExcuseRepository
    {
        private readonly ApplicationDbContext _context;
        private string msg;

        public ExcuseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AcceptExcuse(int Id)
        {
            var excuse = _context.Excuses.Find(Id);
            try
            {
                if (excuse != null)
                {
                    excuse.Approved = "approved";
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Excuse doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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

        public void Add(Excuse excuse)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ExcuseId)
        {
            throw new NotImplementedException();
        }

        public ExcuseDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetAllExcuseForEmployeeId(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetApprovedExcuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetApprovedExcusesByManager(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetDisApprovedExcuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetDisApprovedExcusesByManager(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public bool GetExcuseByEmployeeId(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetExcusesByManager(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionId(int ProfessionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeId(int ProfessionId, int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeIdAndDate(int ProfessionId, int EmployeeId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesForReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetPendingExcusesByHR()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetPendingExcusesByManager(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> PreviousExcuses()
        {
            throw new NotImplementedException();
        }

        public void RejectExcuse(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(int ExcuseId, Excuse excuse)
        {
            throw new NotImplementedException();
        }
    }
}
