using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Repositories
{
    public interface IUniversityRepository
    {
        University Get(int id);
        IEnumerable<University> GetAll();
        void Add(University University);
        void Delete(int UniversityId);
        void Update(int UniversityId, University University);
    }
}
