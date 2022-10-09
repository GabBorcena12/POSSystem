using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Promo
    {
        public string Type { get; set; }
        public string PromoCd { get; set; }
        public string PromoDesc { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Price { get; set; }
        public string IsActive { get; set; } 
    }
}