using HR.DTO;
using HR.Models;
using HR.Repositories;
using HR.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.Services
{
    public class EmployeeService: IEmployeeService

    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService()
        {

        }
        public EmployeeService( IUnitOfWork unitOfWork)
        {
           // _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Delete(Employee employee)
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO Get(int id)
        {
            return _unitOfWork.employee.GetEmployee(id);
          //  return _employeeRepository.GetEmployee(id);
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
           return _unitOfWork.employee.GetAllEmployeesDTO();
        }

        public IEnumerable<EmployeeDTO> GetAllEmployeesByCurrentEmployee(int EmpId)
        {
            return _unitOfWork.employee.EmployeeByProfession(EmpId);
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
