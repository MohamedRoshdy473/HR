﻿using HR.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.Repositories
{
    public interface IPositionLevelsRepository
    {
        PositionLevel Get(int id);
        IEnumerable<PositionLevel> GetAll();
        void Add(PositionLevel positionLevel);
        void Delete(int PositionLevelId);
        void Update(int PositionLevelId, PositionLevel positionLevel);
    }
}
