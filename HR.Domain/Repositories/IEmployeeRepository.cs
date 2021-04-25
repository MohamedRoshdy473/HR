using HR.Data.DTO;
using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Domain.Repositories
{
    public interface IEmployeeRepository
        //:IRepository<Employee>
    {
        IEnumerable<EmployeeDTO> EmployeeByProfession(int EmpId);
        IEnumerable<EmployeeDTO> GetAllEmployeesByProfession(int ProfessionId);
        IEnumerable<EmployeeDTO> GetAllEmployeesDTO();
        int AddEmployee(Employee employee);
        void DeleteEmployee(int EmployeeId);
        void UpdateEmployee(int EmployeeId, Employee employee);
        EmployeeDTO GetEmployee(int id);
        //void UploadFile(IFormFile file);
    }
}
