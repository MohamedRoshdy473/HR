using HR.Data.DTO;
using HR.Data.Models;
using HR.Domain;
using HR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Services
{
    public class PositionsService : IPositionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddPosition(Positions position)
        {
            _unitOfWork.position.Add(position);
        }

        public void DeletePosition(int positionId)
        {
            _unitOfWork.position.Delete(positionId);
        }

        public IEnumerable<Positions> GetAllPositions()
        {
           return _unitOfWork.position.GetAll();
        }

        public Positions GetPosition(int id)
        {
            return _unitOfWork.position.Get(id);
        }

        public void UpdatePosition(int positionId, Positions position)
        {
            _unitOfWork.position.Update(positionId,position);
        }
    }
}
