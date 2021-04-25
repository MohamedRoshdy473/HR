
using HR.Data.DTO;
using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
    public interface IExcuseService
    {
        ExcuseDTO GetExcuse(int id);
        IEnumerable<ExcuseDTO> GetAllExcuses();
        void AddExcuse(Excuse excuse);
        void DeleteExcuse(int ExcuseId);
        void UpdateExcuse(int ExcuseId, Excuse excuse);
        IEnumerable<ReportExcuseDTO> GetExcusesForReport();
        IEnumerable<ReportExcuseDTO> GetExcusesByProfessionId(int ProfessionId);
        IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeId(int ProfessionId, int EmployeeId);
        IEnumerable<ReportExcuseDTO> GetExcusesByProfessionIdAndEmployeeIdAndDate(int ProfessionId, int EmployeeId, DateTime startDate, DateTime endDate);
        IEnumerable<ExcuseDTO> GetExcusesByManager(int EmployeeId);
        IEnumerable<ExcuseDTO> GetApprovedExcuses();
        IEnumerable<ExcuseDTO> GetApprovedExcusesByManager(int EmployeeId);
        IEnumerable<ExcuseDTO> GetDisApprovedExcuses();
        IEnumerable<ExcuseDTO> GetDisApprovedExcusesByManager(int EmployeeId);
        IEnumerable<ExcuseDTO> GetPendingExcusesByManager(int EmployeeId);
        IEnumerable<ExcuseDTO> GetPendingExcusesByHR();
        void AcceptExcuse(int Id);
        void RejectExcuse(int Id);
        Boolean GetExcuseByEmployeeId(int EmployeeId);
        IEnumerable<ExcuseDTO> GetAllExcuseForEmployeeId(int EmployeeId);
    }
}
