using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class PromoDetails
    {
        public string SKU { get; set; }
        public string VariantNo { get; set; }
        public string BatchNbr { get; set; }
        public string Qty { get; set; }
        public string Price {get; set; }
    }
}