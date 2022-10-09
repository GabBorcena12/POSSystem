using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Vendor
    {
        public string VendorCd { get; set; }
        public string VendorNm { get; set; }
        public string VendorLocation { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNbr { get; set; }
        public string IsActive { get; set; } 
    }
}