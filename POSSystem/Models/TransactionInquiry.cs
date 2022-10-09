using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class TransactionInquiry:BaseMessage
    {
        public string Category { get; set; }
        public string MessageTxt { get; set; }
        public string UserId { get; set; }
    }
}