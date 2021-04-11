using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DataAccess.Repository.Repositories.ProfessionsRepositories
{
    public interface IProfessionsRepository
    {
        Profession Get(int id);
        IEnumerable<ProfessionDTO> GetAll();
        void Add(Profession profession);
        void Delete(int ProfessionId);
        void Update(int ProfessionId, Profession profession);
        IEnumerable<ProfessionDTO> GetProfessionByEmployeeId(int EmployeeId);

    }
}
