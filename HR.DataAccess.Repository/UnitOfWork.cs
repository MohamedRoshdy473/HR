using HR.Models;
using HR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Domain.Repositories;
using HR.Core.Repositories;

namespace HR.Core
{
   public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IEmployeeRepository _employeeRepository;
        private IPositionsRepository _positionsRepository;
        private IPositionLevelsRepository _positionLevelsRepository;
        private IProfessionsRepository _professionsRepository;
        private IUniversityRepository _universityRepository;
        private IFacultyRepository _facultyRepository;
        private IFacultyDepartmentRepository _facultyDepartmentRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEmployeeRepository employeeRepository => new EmployeeRepositories(_context);
        public IPositionsRepository positionsRepository => new PositionsRepository(_context);
        public IPositionLevelsRepository positionLevelsRepository => new PositionLevelsRepository(_context);
        public IProfessionsRepository professionsRepository => new ProfessionsRepository(_context);
        public IUniversityRepository universityRepository => new UniversityRepository(_context);
        public IFacultyRepository facultyRepository => new FacultyRepository(_context);
        public IFacultyDepartmentRepository facultyDepartmentRepository => new FacultyDepartmentRepository(_context);

        public int CommitAsync()
        {
            return _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.Dispose();
        }

        public IEmployeeRepository employee => _employeeRepository = _employeeRepository ?? new EmployeeRepositories(_context);

        public IPositionsRepository position => _positionsRepository = _positionsRepository ?? new PositionsRepository(_context);

        public IPositionLevelsRepository positionLevels => _positionLevelsRepository = _positionLevelsRepository ?? new PositionLevelsRepository(_context);

        public IProfessionsRepository professions => _professionsRepository = _professionsRepository ?? new ProfessionsRepository(_context);

        public IUniversityRepository university => _universityRepository = _universityRepository ?? new UniversityRepository(_context);

        public IFacultyRepository faculty => _facultyRepository = _facultyRepository ?? new FacultyRepository(_context);

        public IFacultyDepartmentRepository facultyDepartment => _facultyDepartmentRepository = _facultyDepartmentRepository ?? new FacultyDepartmentRepository(_context);
    }
}

