using HR.Domain;
using HR.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain
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
