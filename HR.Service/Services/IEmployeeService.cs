using HR.DTO;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Services
{
    public interface IEmployeeService
    {
        EmployeeDTO Get(int id);
        IEnumerable<EmployeeDTO> GetAll();
        IEnumerable<EmployeeDTO> GetAllEmployeesByCurrentEmployee(int EmpId);
        Task Add(Employee employee);
        void Delete(Employee employee);
        void Update(Employee employee);
    }
}
