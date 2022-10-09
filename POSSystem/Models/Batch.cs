using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Batch
    {
        public string BatchNbr { get; set; }
        public string VendorCd { get; set; }
        public string VendorNm { get; set; }
        public string VendorLocation { get; set; }
        public string vendorContactPerson { get; set; }
        public string VendorContactNbr { get; set; } 
        public string ReceiverNm { get; set; }
        public string ReceivedDate { get; set; }
        public string PaidAmt { get; set; }
        public string IsActive { get; set; }

        public string CreateDttm { get; set; }

        public string CreateUser { get; set; }
        public string StatusCd { get; set; }
        public string Status { get; set; }
    }
}