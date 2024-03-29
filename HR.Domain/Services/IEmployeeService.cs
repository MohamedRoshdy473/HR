﻿
using HR.Data.DTO;
using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
    public interface IEmployeeService
    {
        EmployeeDTO Get(int id);
        IEnumerable<EmployeeDTO> GetAll();
        IEnumerable<EmployeeDTO> GetAllEmployeesByCurrentEmployee(int EmpId);
        IEnumerable<EmployeeDTO> GetAllEmployeesByProfession(int ProfessionId);
        void AddEmployee(Employee employee);
        void DeleteEmployee(int EmployeeId);
        void UpdateEmployee(int EmployeeId,Employee employee);
    }
}
