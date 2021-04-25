
using HR.Data.DTO;
using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
    public interface IProfessionsService
    {
        Profession Getprofession(int id);
        IEnumerable<ProfessionDTO> GetAllProfessions();
        void Addprofession(Profession profession);
        void Deleteprofession(int professionId);
        void Updateprofession(int professionId, Profession profession);
        IEnumerable<ProfessionDTO> GetProfessionByEmployeeId(int EmployeeId);
    }
}
