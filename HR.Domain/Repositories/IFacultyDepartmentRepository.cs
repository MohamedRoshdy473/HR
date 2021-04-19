using HR.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Repositories
{
    public interface IFacultyDepartmentRepository
    {
        FacultyDepartmentDTO Get(int id);
        IEnumerable<FacultyDepartmentDTO> GetAll();
        void Add(FacultyDepartmentDTO facultyDepartmentDTO);
        void Delete(int FacultyDepartmentId);
        void Update(int FacultyDepartmentId, FacultyDepartmentDTO facultyDepartmentDTO);
        IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentsByFacultyId(int FacultyId);

    }
}
