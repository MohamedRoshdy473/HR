using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Services
{
    public interface IPositionsLevelService
    {
        PositionLevel GetPositionsLevel(int id);
        IEnumerable<PositionLevel> GetAllPositionsLevels();
        void AddPositionsLevel(PositionLevel PositionsLevel);
        void DeletePositionsLevel(int PositionsLevelId);
        void UpdatePositionsLevel(int PositionsLevelId, PositionLevel positionLevel);
    }
}
