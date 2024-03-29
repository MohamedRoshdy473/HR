﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Data.DTO
{
    public class ExcuseDTO
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int Hours { get; set; }
        public int ProfessionID { get; set; }
        public string ProfessionName { get; set; }
        public string Approved { get; set; }
     
        public string Comment { get; set; }
    }
}
