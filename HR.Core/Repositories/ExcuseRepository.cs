using HR.Domain.Repositories;
using HR.Data.DTO;
using HR.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;

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
            try
            {
                if (excuse != null)
                {
                    _context.Excuses.Add(excuse);
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

        public void Delete(int ExcuseId)
        {
            var excuse = _context.Excuses.Find(ExcuseId);
            try
            {
                if (excuse != null)
                {
                    _context.Excuses.Remove(excuse);
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

        public ExcuseDTO Get(int id)
        {
            var ex = _context.Excuses.Include(e => e.Employee).ThenInclude(e => e.Profession)
                                     .FirstOrDefault(e => e.ID == id);
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
            if (excuseDTO == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Excuse doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return excuseDTO;

            }
        }

        public IEnumerable<ExcuseDTO> GetAll()
        {
            var ExcusesList = _context.Excuses.Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionID = ex.Employee.ProfessionID,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeId = ex.EmployeeID,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToList();
            return ExcusesList;
        }

        public IEnumerable<ExcuseDTO> GetAllExcuseForEmployeeId(int EmployeeId)
        {
            var lstEx = _context.Excuses.Include(e => e.Employee).Where(e => e.EmployeeID == EmployeeId)
                                      .Select(ex => new ExcuseDTO
                                      {
                                          ID = ex.ID,
                                          Approved = ex.Approved,
                                          Comment = ex.Comment,
                                          Date = ex.Date,
                                          ProfessionName = ex.Employee.Profession.Name,
                                          EmployeeName = ex.Employee.Name,
                                          Hours = ex.Hours,
                                          Time = ex.Time
                                      }).ToList();
            return lstEx;
        }

        public IEnumerable<ExcuseDTO> GetApprovedExcuses()
        {
            return _context.Excuses.Where(ex => ex.Approved == "approved").Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToList();
        }

        public IEnumerable<ExcuseDTO> GetApprovedExcusesByManager(int EmployeeId)
        {
            return _context.Excuses.Where(ex => ex.Approved == "approved" && ex.Employee.Profession.ManagerID == EmployeeId).Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToList();
        }

        public IEnumerable<ExcuseDTO> GetDisApprovedExcuses()
        {
            return _context.Excuses.Where(ex => ex.Approved == "disapproved").Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToList();
        }

        public IEnumerable<ExcuseDTO> GetDisApprovedExcusesByManager(int EmployeeId)
        {
            return _context.Excuses.Where(ex => ex.Approved == "disapproved" && ex.Employee.Profession.ManagerID == EmployeeId).Select(ex => new ExcuseDTO
            {
                ID = ex.ID,
                Approved = ex.Approved,
                Comment = ex.Comment,
                Date = ex.Date,
                ProfessionName = ex.Employee.Profession.Name,
                EmployeeName = ex.Employee.Name,
                Hours = ex.Hours,
                Time = ex.Time
            }).ToList();
        }

        public bool GetExcuseByEmployeeId(int EmployeeId)
        {
            var lstEx = _context.Excuses.Include(e => e.Employee).Where(e => e.EmployeeID == EmployeeId && e.Date.Month == DateTime.Today.Month).ToList();
            if (lstEx.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ExcuseDTO> GetExcusesByManager(int EmployeeId)
        {
            var ex = _context.Excuses.Where(ex => ex.Employee.Profession.ManagerID == EmployeeId)
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
                 }).ToList();
            return ex;
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionId(int ProfessionId)
        {
            List<ReportExcuseDTO> lstexcusesDTO = new List<ReportExcuseDTO>();
            var ExcusesList = (from ex in _context.Excuses.Include(ex => ex.Employee.Profession).Where(e => e.Employee.ProfessionID == ProfessionId).ToList()
                               select ex).GroupBy(grp => grp.EmployeeID).ToList();
            foreach (var item in ExcusesList)
            {
                ReportExcuseDTO excuseObj = new ReportExcuseDTO();
                excuseObj.ProfessionName = item.FirstOrDefault().Employee.Profession.Name;
                excuseObj.ProfessionId = item.FirstOrDefault().Employee.ProfessionID;
                excuseObj.EmployeeId = item.FirstOrDefault().EmployeeID;
                excuseObj.EmployeeName = item.FirstOrDefault().Employee.Name;
                excuseObj.lstExcuse = item.ToList();
                lstexcusesDTO.Add(excuseObj);
            }
            return lstexcusesDTO;
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeId(int ProfessionId, int EmployeeId)
        {
            List<ReportExcuseDTO> lstexcusesDTO = new List<ReportExcuseDTO>();
            var ExcusesList = (from ex in _context.Excuses.Include(ex => ex.Employee.Profession).Where(e => e.Employee.ProfessionID == ProfessionId && e.EmployeeID == EmployeeId).ToList()
                               select ex).GroupBy(grp => grp.EmployeeID).ToList();
            foreach (var item in ExcusesList)
            {
                ReportExcuseDTO excuseObj = new ReportExcuseDTO();
                excuseObj.ProfessionName = item.FirstOrDefault().Employee.Profession.Name;
                excuseObj.ProfessionId = item.FirstOrDefault().Employee.ProfessionID;
                excuseObj.EmployeeId = item.FirstOrDefault().EmployeeID;
                excuseObj.EmployeeName = item.FirstOrDefault().Employee.Name;
                excuseObj.lstExcuse = item.ToList();
                lstexcusesDTO.Add(excuseObj);
            }
            return lstexcusesDTO;
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeIdAndDate(int ProfessionId, int EmployeeId, DateTime startDate, DateTime endDate)
        {
            List<ReportExcuseDTO> lstexcusesDTO = new List<ReportExcuseDTO>();
            var ExcusesList = (from ex in _context.Excuses.Include(ex => ex.Employee.Profession).Where(e => e.Employee.ProfessionID == ProfessionId
                               && e.EmployeeID == EmployeeId && e.Date >= startDate && e.Date <= endDate).ToList()
                               select ex).GroupBy(grp => grp.EmployeeID).ToList();
            foreach (var item in ExcusesList)
            {
                ReportExcuseDTO excuseObj = new ReportExcuseDTO();
                excuseObj.ProfessionName = item.FirstOrDefault().Employee.Profession.Name;
                excuseObj.ProfessionId = item.FirstOrDefault().Employee.ProfessionID;
                excuseObj.EmployeeId = item.FirstOrDefault().EmployeeID;
                excuseObj.EmployeeName = item.FirstOrDefault().Employee.Name;
                excuseObj.lstExcuse = item.ToList();
                lstexcusesDTO.Add(excuseObj);
            }
            return lstexcusesDTO;
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesForReport()
        {
            List<ReportExcuseDTO> lstexcusesDTO = new List<ReportExcuseDTO>();
            var ExcusesList = (from ex in _context.Excuses.Include(ex => ex.Employee.Profession).ToList()
                               select ex).GroupBy(grp => grp.EmployeeID).ToList();
            foreach (var item in ExcusesList)
            {
                ReportExcuseDTO excuseObj = new ReportExcuseDTO();
                excuseObj.ProfessionName = item.FirstOrDefault().Employee.Profession.Name;
                excuseObj.ProfessionId = item.FirstOrDefault().Employee.ProfessionID;
                excuseObj.EmployeeId = item.FirstOrDefault().EmployeeID;
                excuseObj.EmployeeName = item.FirstOrDefault().Employee.Name;
                excuseObj.lstExcuse = item.ToList();
                lstexcusesDTO.Add(excuseObj);
            }
            return lstexcusesDTO;
        }

        public IEnumerable<ExcuseDTO> GetPendingExcusesByHR()
        {
            var ex = _context.Excuses.Where(ex => ex.Approved == "pending")
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
                  }).ToList();
            return ex;
        }

        public IEnumerable<ExcuseDTO> GetPendingExcusesByManager(int EmployeeId)
        {
            var ex = _context.Excuses.Where(ex => ex.Approved == "pending")
                 .Where(ex => ex.Employee.Profession.ManagerID == EmployeeId)
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
                 }).ToList();
            return ex;
        }

        public void RejectExcuse(int Id)
        {
            var excuse = _context.Excuses.Find(Id);
            excuse.Approved = "disapproved";
            _context.SaveChanges();
        }

        public void Update(int ExcuseId, Excuse excuse)
        {
            try
            {
                if (excuse != null)
                {
                    var Old = _context.Excuses.Include(e => e.Employee).ThenInclude(e => e.Profession)
                              .FirstOrDefault(e => e.ID == ExcuseId);
                    excuse.EmployeeID = Old.EmployeeID;
                    _context.Entry(Old).State = EntityState.Detached;
                    _context.Entry(excuse).State = EntityState.Modified;
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
    }
}
