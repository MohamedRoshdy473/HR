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
    public class FacultyDepartmentRepository : IFacultyDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private string msg;

        public FacultyDepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(FacultyDepartmentDTO facultyDepartmentDTO)
        {
            try
            {
                if (facultyDepartmentDTO != null)
                {
                    FacultyDepartment facultyDepartment = new FacultyDepartment();
                    facultyDepartment.Id = facultyDepartmentDTO.Id;
                    facultyDepartment.FacultyDepartmentName = facultyDepartmentDTO.FacultyDepartmentName;
                    facultyDepartment.FacultyId = facultyDepartmentDTO.FacultyId;
                    _context.FacultyDepartments.Add(facultyDepartment);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Faculty doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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

        public void Delete(int FacultyDepartmentId)
        {
            var facultyDept = _context.FacultyDepartments.Find(FacultyDepartmentId);
            try
            {
                if (facultyDept != null)
                {
                    _context.FacultyDepartments.Remove(facultyDept);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Faculty Department doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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

        public FacultyDepartmentDTO Get(int id)
        {
            var facultyDep =  _context.FacultyDepartments.Include(f => f.Faculty).Include(f => f.Faculty.University).FirstOrDefault(f => f.Id == id);
            var facultyDepartment = new FacultyDepartmentDTO
            {
                Id = facultyDep.Id,
                FacultyDepartmentName = facultyDep.FacultyDepartmentName,
                FacultyId = facultyDep.FacultyId,
                FacultyName = facultyDep.Faculty.FacultyName,
                UniversityId = facultyDep.Faculty.UniversityID,
                UniversityName = facultyDep.Faculty.University.UniversityName
            };
            if (facultyDepartment == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Faculty Department doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return facultyDepartment;

            }
        }

        public IEnumerable<FacultyDepartmentDTO> GetAll()
        {
            return _context.FacultyDepartments.Include(f => f.Faculty).Include(f => f.Faculty.University).Select(facultyDep => new FacultyDepartmentDTO
            {
                Id = facultyDep.Id,
                FacultyDepartmentName = facultyDep.FacultyDepartmentName,
                FacultyId = facultyDep.FacultyId,
                FacultyName = facultyDep.Faculty.FacultyName,
                UniversityId = facultyDep.Faculty.UniversityID,
                UniversityName = facultyDep.Faculty.University.UniversityName
            }).ToList();
        }

        public IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentsByFacultyId(int FacultyId)
        {
            var facultyDepartments = _context.FacultyDepartments.Include(f => f.Faculty).Where(f => f.FacultyId == FacultyId).Select(fac => new FacultyDepartmentDTO
            {
                Id = fac.Id,
                FacultyDepartmentName = fac.FacultyDepartmentName
            }).ToList();
            return facultyDepartments;
        }

        public void Update(int FacultyDepartmentId, FacultyDepartmentDTO facultyDepartmentDTO)
        {

            try
            {
                if (facultyDepartmentDTO != null)
                {
                    FacultyDepartment facultyDepartment = new FacultyDepartment();
                    facultyDepartment.Id = facultyDepartmentDTO.Id;
                    facultyDepartment.FacultyDepartmentName = facultyDepartmentDTO.FacultyDepartmentName;
                    facultyDepartment.FacultyId = facultyDepartmentDTO.FacultyId;
                    _context.Entry(facultyDepartment).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Faculty Department doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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
