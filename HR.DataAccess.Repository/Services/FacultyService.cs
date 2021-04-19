using HR.Domain;
using HR.Domain.Services;
using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacultyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddFaculty(FacultyDTO facultyDTO)
        {
            _unitOfWork.faculty.Add(facultyDTO);
        }

        public void DeleteFaculty(int FacultyId)
        {
            _unitOfWork.faculty.Delete(FacultyId);
        }

        public IEnumerable<FacultyDTO> GetFacultiesByUniversityId(int UniversityId)
        {
           return _unitOfWork.faculty.GetFacultiesByUniversityId(UniversityId);
        }
        public void UpdateFaculty(int FacultyId, FacultyDTO facultyDTO)
        {
            _unitOfWork.faculty.Update(FacultyId, facultyDTO);
        }

        IEnumerable<FacultyDTO> IFacultyService.GetAllFaculties()
        {
            return _unitOfWork.faculty.GetAll();
        }

        FacultyDTO IFacultyService.GetFaculty(int id)
        {
            return _unitOfWork.faculty.Get(id);
        }
    }
}
