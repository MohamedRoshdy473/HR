using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Repositories
{
    public interface IEmployeeRepository
        //:IRepository<Employee>
    {
        IEnumerable<EmployeeDTO> EmployeeByProfession(int EmpId);
        IEnumerable<EmployeeDTO> GetAllEmployeesDTO();
        EmployeeDTO GetEmployee(int id);
    }
}
