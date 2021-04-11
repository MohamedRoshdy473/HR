using HR.DataAccess.Repository.Repositories.PositionLevelsRepositories;
using HR.DataAccess.Repository.Repositories.PositionsRepositories;
using HR.DataAccess.Repository.Repositories.ProfessionsRepositories;
using HR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service
{
    public interface IUnitOfWork
    {
        int CommitAsync();
        void Rollback();
        IEmployeeRepository employee { get; }
        IPositionsRepository position { get; }
        IPositionLevelsRepository positionLevels { get; }
        IProfessionsRepository professions { get; }

    }
}
