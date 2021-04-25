using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
   public interface IUniversityService
    {
        University GetUniversity(int id);
        IEnumerable<University> GetAllUniversities();
        void AddUniversity(University University);
        void DeleteUniversity(int UniversityId);
        void UpdateUniversity(int UniversityId, University University);

    }
}
