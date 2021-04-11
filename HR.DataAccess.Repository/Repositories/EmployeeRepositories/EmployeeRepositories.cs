using HR.DTO;
using HR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HR.Repositories
{
    public class EmployeeRepositories : IEmployeeRepository
    {
        protected readonly ApplicationDbContext _context;
        string msg;

        public EmployeeRepositories(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<EmployeeDTO> EmployeeByProfession(int EmpId)
        {
            List<EmployeeDTO> lstEmployeeDTO = new List<EmployeeDTO>();
            var LstEmpUsers = _context.Employees.Where(e => e.ID == EmpId).ToList();
            if (LstEmpUsers.Count > 0)
            {
                var EmployeeObj = LstEmpUsers[0];


                lstEmployeeDTO = _context.Employees.Where(e => e.ProfessionID == EmployeeObj.ProfessionID && e.ID != EmpId).Select(e => new EmployeeDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    ProfessionName = e.Profession.Name,
                    GraduatioYear = e.GraduatioYear,
                    Address = e.Address,
                    Code = e.Code,
                    DateOfBirth = e.DateOfBirth,
                    Email = e.Email,
                    gender = e.gender,
                    HiringDateHiringDate = e.HiringDateHiringDate,
                    MaritalStatus = e.MaritalStatus,
                    Phone = e.Phone,
                    RelevantPhone = e.RelevantPhone,
                    Photo = e.photo,

                    EmailCompany = e.EmailCompany,
                    Mobile = e.Mobile,
                    NationalId = e.NationalId,
                    FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                    FacultyDepartmentId = (int)e.FacultyDepartmentId,
                    FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                    UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                    PositionId = e.PositionId,
                    PositionName = e.Positions.PositionName,
                    PositionlevelId = e.PositionlevelId,
                    LevelName = e.PositionLevel.LevelName

                }).ToList();
            }
            return lstEmployeeDTO;
        }

        public EmployeeDTO GetEmployee(int id)
        {
            var e = _context.Employees.Include(e => e.Profession).Include(e => e.FacultyDepartment)
                                      .Include(e => e.FacultyDepartment.Faculty).Include(e => e.FacultyDepartment.Faculty.University)
                                      .Include(e => e.PositionLevel).Include(e => e.Positions)
                                      .FirstOrDefault(e => e.ID == id);
            var emp = new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name,
                ProfessionName = e.Profession.Name,
                ProfessionID = e.Profession.ID,
                GraduatioYear = e.GraduatioYear,
                Address = e.Address,
                Code = e.Code,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                gender = e.gender,
                HiringDateHiringDate = e.HiringDateHiringDate,
                MaritalStatus = e.MaritalStatus,
                Phone = e.Phone,
                RelevantPhone = e.RelevantPhone,
                Photo = e.photo,

                EmailCompany = e.EmailCompany,
                Mobile = e.Mobile,
                NationalId = e.NationalId,
                FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                FacultyDepartmentId = (int)e.FacultyDepartmentId,
                FacultyId = e.FacultyDepartment.FacultyId,
                FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                UniversityId = e.FacultyDepartment.Faculty.UniversityID,
                UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                PositionId = e.PositionId,
                PositionName = e.Positions.PositionName,
                PositionlevelId = e.PositionlevelId,
                LevelName = e.PositionLevel.LevelName
            };
            if (emp == null)
            {
                return null;
            }

            return emp;
        }

        public IEnumerable<EmployeeDTO> GetAllEmployeesDTO()
        {
            var emps = _context.Employees.Select(e => new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name,
                ProfessionName = e.Profession.Name,
                GraduatioYear = e.GraduatioYear,
                Address = e.Address,
                Code = e.Code,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                gender = e.gender,
                HiringDateHiringDate = e.HiringDateHiringDate,
                MaritalStatus = e.MaritalStatus,
                Phone = e.Phone,
                RelevantPhone = e.RelevantPhone,
                Photo = e.photo,
                EmailCompany = e.EmailCompany,
                Mobile = e.Mobile,
                NationalId = e.NationalId,
                FacultyDepartmentName = e.FacultyDepartment.FacultyDepartmentName,
                FacultyDepartmentId = (int)e.FacultyDepartmentId,
                FacultyName = e.FacultyDepartment.Faculty.FacultyName,
                UniversityName = e.FacultyDepartment.Faculty.University.UniversityName,
                PositionId = e.PositionId,
                PositionName = e.Positions.PositionName,
                PositionlevelId = e.PositionlevelId,
                LevelName = e.PositionLevel.LevelName

            }).ToList();
            return emps;
        }

        public IEnumerable<EmployeeDTO> GetAllEmployeesByProfession(int ProfessionId)
        {
            var emps = _context.Employees.Where(e => e.ProfessionID == ProfessionId).Select(e => new EmployeeDTO
            {
                ID = e.ID,
                Name = e.Name,
                ProfessionName = e.Profession.Name,
                GraduatioYear = e.GraduatioYear,
                Address = e.Address,
                Code = e.Code,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                gender = e.gender,
                HiringDateHiringDate = e.HiringDateHiringDate,
                MaritalStatus = e.MaritalStatus,
                Phone = e.Phone,
                RelevantPhone = e.RelevantPhone,
                Photo = e.photo
            }).ToList();
            return emps;
        }

        public int AddEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Employee doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return employee.ID;
        }

        public void DeleteEmployee(int EmployeeId)
        {
            var employee = _context.Employees.Find(EmployeeId);
            try
            {
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
                else
                {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Employee doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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

        public void UpdateEmployee(int EmployeeId, Employee employee)
        {
            if (EmployeeId != employee.ID)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Employee doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}