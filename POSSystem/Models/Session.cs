using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Session:EmployeeDetails
    {
        public string UserId { get; set; }
        public string CounterNbr { get; set; }
        public string Status { get; set; }
        public string CreateDttm { get; set; }
    }
}