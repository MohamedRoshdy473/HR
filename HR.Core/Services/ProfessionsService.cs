using HR.Data.DTO;
using HR.Data.Models;
using HR.Domain;
using HR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class ProfessionsService : IProfessionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfessionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Addprofession(Profession profession)
        {
            _unitOfWork.professions.Add(profession);
        }

        public void Deleteprofession(int professionId)
        {
            _unitOfWork.professions.Delete(professionId);
        }

        public IEnumerable<ProfessionDTO> GetAllProfessions()
        {
           return _unitOfWork.professions.GetAll();
        }

        public Profession Getprofession(int id)
        {
            return _unitOfWork.professions.Get(id);
        }

        public IEnumerable<ProfessionDTO> GetProfessionByEmployeeId(int EmployeeId)
        {
            return _unitOfWork.professions.GetProfessionByEmployeeId(EmployeeId);
        }

        public void Updateprofession(int professionId, Profession profession)
        {
            _unitOfWork.professions.Update(professionId, profession);
        }
    }
}
