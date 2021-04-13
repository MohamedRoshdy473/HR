using HR.Core;
using HR.Domain;
using HR.Domain.Services;
using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class EmployeeService: IEmployeeService

    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService()
        {

        }
        public EmployeeService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddEmployee(Employee employee)
        {
             _unitOfWork.employee.AddEmployee(employee);
            _unitOfWork.CommitAsync();
        }

        public void DeleteEmployee(int EmployeeId)
        {
            _unitOfWork.employee.DeleteEmployee(EmployeeId);
            _unitOfWork.CommitAsync();
        }

        public EmployeeDTO Get(int id)
        {
            return _unitOfWork.employee.GetEmployee(id);
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
           return _unitOfWork.employee.GetAllEmployeesDTO();
        }
        public IEnumerable<EmployeeDTO> GetAllEmployeesByCurrentEmployee(int EmpId)
        {
            return _unitOfWork.employee.EmployeeByProfession(EmpId);
        }
        public IEnumerable<EmployeeDTO> GetAllEmployeesByProfession(int ProfessionId)
        {
            return _unitOfWork.employee.GetAllEmployeesByProfession(ProfessionId);
        }
        public void UpdateEmployee(int EmployeeId,Employee employee)
        {
            _unitOfWork.employee.UpdateEmployee(EmployeeId, employee);
            _unitOfWork.CommitAsync();
        }
    }
}
