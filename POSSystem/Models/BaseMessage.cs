using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class BaseMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}