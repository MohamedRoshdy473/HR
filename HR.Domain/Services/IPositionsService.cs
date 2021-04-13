using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
   public interface IPositionsService
    {
        Positions GetPosition(int id);
        IEnumerable<Positions> GetAllPositions();
        void AddPosition(Positions position);
        void DeletePosition(int positionId);
        void UpdatePosition(int positionId,Positions position);
    }
}
