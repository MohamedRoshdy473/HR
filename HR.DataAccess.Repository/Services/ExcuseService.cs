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
    public class ExcuseService : IExcuseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcuseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AcceptExcuse(int Id)
        {
            throw new NotImplementedException();
        }

        public void AddExcuse(Excuse excuse)
        {
            _unitOfWork.excuse.Add(excuse);
        }

        public void DeleteExcuse(int ExcuseId)
        {
            _unitOfWork.excuse.Delete(ExcuseId);
        }

        public IEnumerable<ExcuseDTO> GetAllExcuseForEmployeeId(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcuseDTO> GetAllExcuses()
        {
            return _unitOfWork.excuse.GetAll();
        }

        public IEnumerable<ExcuseDTO> GetApprovedExcuses()
        {
            return _unitOfWork.excuse.GetApprovedExcuses();
        }

        public IEnumerable<ExcuseDTO> GetApprovedExcusesByManager(int EmployeeId)
        {
            return _unitOfWork.excuse.GetApprovedExcusesByManager(EmployeeId);
        }

        public IEnumerable<ExcuseDTO> GetDisApprovedExcuses()
        {
            return _unitOfWork.excuse.GetDisApprovedExcuses();
        }

        public IEnumerable<ExcuseDTO> GetDisApprovedExcusesByManager(int EmployeeId)
        {
            return _unitOfWork.excuse.GetDisApprovedExcusesByManager(EmployeeId);
        }

        public ExcuseDTO GetExcuse(int id)
        {
            return _unitOfWork.excuse.Get(id);
        }

        public bool GetExcuseByEmployeeId(int EmployeeId)
        {
            return _unitOfWork.excuse.GetExcuseByEmployeeId(EmployeeId);
        }

        public IEnumerable<ExcuseDTO> GetExcusesByManager(int EmployeeId)
        {
            return _unitOfWork.excuse.GetExcusesByManager(EmployeeId);
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionId(int ProfessionId)
        {
            return _unitOfWork.excuse.GetExcusesByProfessionId(ProfessionId);
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeId(int ProfessionId, int EmployeeId)
        {
            return _unitOfWork.excuse.GetExcusesByProfessionIdAndEmployeeId(ProfessionId, EmployeeId);
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeIdAndDate(int ProfessionId, int EmployeeId, DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.excuse.GetExcusesByProfessionIdAndEmployeeIdAndDate(ProfessionId, EmployeeId, startDate,endDate);
        }

        public IEnumerable<ReportExcuseDTO> GetExcusesForReport()
        {
            return _unitOfWork.excuse.GetExcusesForReport();
        }

        public IEnumerable<ExcuseDTO> GetPendingExcusesByHR()
        {
            return _unitOfWork.excuse.GetPendingExcusesByHR();
        }

        public IEnumerable<ExcuseDTO> GetPendingExcusesByManager(int EmployeeId)
        {
            return _unitOfWork.excuse.GetPendingExcusesByManager(EmployeeId);
        }

        public IEnumerable<ExcuseDTO> PreviousExcuses()
        {
            throw new NotImplementedException();
        }

        public void RejectExcuse(int Id)
        {
            _unitOfWork.excuse.RejectExcuse(Id);
        }

        public void UpdateExcuse(int ExcuseId, Excuse excuse)
        {
            throw new NotImplementedException();
        }
    }
}
