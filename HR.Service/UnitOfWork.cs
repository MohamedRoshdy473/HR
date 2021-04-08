using HR.Models;
using HR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
   public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IEmployeeRepository _employeeRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEmployeeRepository employeeRepository => new EmployeeRepositories(_context);

        public int CommitAsync()
        {
            return _context.SaveChanges();
        }
         public IEmployeeRepository employee => _employeeRepository = _employeeRepository ?? new EmployeeRepositories(_context);

    }
}

