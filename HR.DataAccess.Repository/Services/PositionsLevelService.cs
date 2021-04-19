using HR.Domain;
using HR.Domain.Services;
using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class PositionsLevelService : IPositionsLevelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionsLevelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddPositionsLevel(PositionLevel PositionsLevel)
        {
            _unitOfWork.positionLevels.Add(PositionsLevel);
        }

        public void DeletePositionsLevel(int PositionsLevelId)
        {
            _unitOfWork.positionLevels.Delete(PositionsLevelId);
        }

        public IEnumerable<PositionLevel> GetAllPositionsLevels()
        {
            return _unitOfWork.positionLevels.GetAll();
        }

        public PositionLevel GetPositionsLevel(int id)
        {
           return _unitOfWork.positionLevels.Get(id);
        }

        public void UpdatePositionsLevel(int PositionsLevelId, PositionLevel positionLevel)
        {
            _unitOfWork.positionLevels.Update(PositionsLevelId, positionLevel);
        }
    }
}
