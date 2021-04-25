using HR.Domain;
using HR.Domain.Services;
using HR.Data.DTO;
using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class FacultyDepartmentService : IFacultyDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacultyDepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddFacultyDepartment(FacultyDepartmentDTO facultyDepartmentDTO)
        {
            _unitOfWork.facultyDepartment.Add(facultyDepartmentDTO);
        }

        public void DeleteFacultyDepartment(int FacultyDepartmentId)
        {
            _unitOfWork.facultyDepartment.Delete(FacultyDepartmentId);
        }

        public IEnumerable<FacultyDepartmentDTO> GetAllFacultyDepartments()
        {
          return  _unitOfWork.facultyDepartment.GetAll();
        }

        public FacultyDepartmentDTO GetFacultyDepartment(int id)
        {
            return _unitOfWork.facultyDepartment.Get(id);
        }

        public IEnumerable<FacultyDepartmentDTO> GetFacultyDepartmentsByFacultyId(int FacultyId)
        {
            return _unitOfWork.facultyDepartment.GetFacultyDepartmentsByFacultyId(FacultyId);
        }

        public void UpdateFacultyDepartment(int FacultyDepartmentId, FacultyDepartmentDTO facultyDepartmentDTO)
        {
            _unitOfWork.facultyDepartment.Update(FacultyDepartmentId, facultyDepartmentDTO);
        }
    }
}
