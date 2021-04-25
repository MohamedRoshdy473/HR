using HR.Domain.Repositories;
using HR.Data.DTO;
using HR.Data.Models;
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
    public class FacultyRepository: IFacultyRepository
    {
        protected readonly ApplicationDbContext _context;
        string msg;

        public FacultyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(FacultyDTO facultyDTO)
        {
            try
            {
                if (facultyDTO != null)
                {
                    Faculty faculty = new Faculty();
                    faculty.FacultyName = facultyDTO.FacultyName;
                    faculty.UniversityID = facultyDTO.UniversityID;
                    _context.Faculties.Add(faculty);
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

        public void Delete(int FacultyId)
        {
            var faculty = _context.Faculties.Find(FacultyId);
            try
            {
                if (faculty != null)
                {
                    _context.Faculties.Remove(faculty);
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

        public FacultyDTO Get(int id)
        {
            var fac = _context.Faculties.Include(f => f.University).FirstOrDefault(f => f.Id == id);
            var faculty = new FacultyDTO
            {
                Id = fac.Id,
                FacultyName = fac.FacultyName,
                UniversityID = fac.UniversityID,
                UniversityName = fac.University.UniversityName
            };
            if (faculty == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Faculty doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return faculty;

            }
        }

        public IEnumerable<FacultyDTO> GetAll()
        {
            return  _context.Faculties.Select(fac => new FacultyDTO
            {
                Id = fac.Id,
                FacultyName = fac.FacultyName,
                UniversityID = fac.UniversityID,
                UniversityName = fac.University.UniversityName
            }).ToList();
        }

        public IEnumerable<FacultyDTO> GetFacultiesByUniversityId(int UniversityId)
        {
            var faculty = _context.Faculties.Include(f => f.University).Where(f => f.UniversityID == UniversityId).Select(fac => new FacultyDTO
            {
                Id = fac.Id,
                FacultyName = fac.FacultyName,
                UniversityID = fac.UniversityID,
                UniversityName = fac.University.UniversityName
            }).ToList();
            return faculty;
        }

        public void Update(int FacultyId, FacultyDTO facultyDTO)
        {
            try
            {
                if (facultyDTO != null)
                {
                    Faculty faculty = new Faculty();
                    faculty.Id = facultyDTO.Id;
                    faculty.FacultyName = facultyDTO.FacultyName;
                    faculty.UniversityID = facultyDTO.UniversityID;
                    _context.Entry(faculty).State = EntityState.Modified;
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
    }
}
