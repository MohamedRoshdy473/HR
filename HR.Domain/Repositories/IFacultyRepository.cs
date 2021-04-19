using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Repositories
{
    public interface IFacultyRepository
    {
        FacultyDTO Get(int id);
        IEnumerable<FacultyDTO> GetAll();
        void Add(FacultyDTO facultyDTO);
        void Delete(int FacultyId);
        void Update(int FacultyId, FacultyDTO facultyDTO);
        IEnumerable<FacultyDTO> GetFacultiesByUniversityId(int UniversityId);

    }
}
