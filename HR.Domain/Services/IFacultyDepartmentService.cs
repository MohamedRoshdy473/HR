using HR.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
    public interface IFacultyDepartmentService
    {
        FacultyDepartmentDTO GetFacultyDepartment(int id);
        IEnumerable<FacultyDepartmentDTO> GetAllFacultyDepartments();
        void AddFacultyDepartment(FacultyDepartmentDTO facultyDepartmentDTO);
        void DeleteFacultyDepartment(int FacultyDepartmentId);
        void UpdateFacultyDepartment(int FacultyDepartmentId, FacultyDepartmentDTO facultyDepartmentDTO);
        IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentsByFacultyId(int FacultyId);
    }
}
