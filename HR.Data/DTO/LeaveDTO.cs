﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Data.DTO
{
    public class LeaveDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Date { get; set; }
        public int Days { get; set; }
        public int AlternativeEmpID { get; set; }

        public string AlternativeEmp { get; set; }
      
        public string Comment { get; set; }
       
        public string AlternativeAddress { get; set; }
        public string Status { get; set; }
        public string EmployeeName { get; set; }
        public int ProfessionID { get; set; }
        public string Profession { get; set; }
        public string LeavesFiles { get; set; }


    }
}
