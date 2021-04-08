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
        IEmployeeRepository employee { get; }
    }
}
