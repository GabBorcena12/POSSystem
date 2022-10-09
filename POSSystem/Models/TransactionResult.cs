using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class TransactionResult:BaseMessage
    {
        public string ReferenceId { get; set; }
        public string ReferenceTxt { get; set; }
    }
}