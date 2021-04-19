using HR.Domain;
using HR.Domain.Services;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UniversityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddUniversity(University University)
        {
            _unitOfWork.university.Add(University);
        }

        public void DeleteUniversity(int UniversityId)
        {
            _unitOfWork.university.Delete(UniversityId);
        }

        public IEnumerable<University> GetAllUniversities()
        {
           return _unitOfWork.university.GetAll();
        }

        public University GetUniversity(int id)
        {
            return _unitOfWork.university.Get(id);
        }

        public void UpdateUniversity(int UniversityId, University University)
        {
            _unitOfWork.university.Update(UniversityId, University);
        }
    }
}
