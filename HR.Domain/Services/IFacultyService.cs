using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
    public interface IFacultyService
    {
        FacultyDTO GetFaculty(int id);
        IEnumerable<FacultyDTO> GetAllFaculties();
        void AddFaculty(FacultyDTO facultyDTO);
        void DeleteFaculty(int FacultyId);
        void UpdateFaculty(int FacultyId, FacultyDTO facultyDTO);
        IEnumerable<FacultyDTO> GetFacultiesByUniversityId(int UniversityId);
    }
}
