﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Data.DTO
{
    public class EvaluationProfessionDTO
    {
        public int ID { get; set; }
        public int EvaluationTypeID { get; set; }
        public string EvaluationTypeName { get; set; }
        public int ProfessionID { get; set; }
        public string ProfessionName { get; set; }

    }
}
