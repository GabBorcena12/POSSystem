using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Inventory
    {
        public string SKU { get; set; }
        public string VariantNbr { get; set; }
        public string ProductDesc1 { get; set; }
        public string ProductDesc2 { get; set; }
        public string ProductDesc3 { get; set; }
        public string BatchNbr { get; set; }
        public string BatchListPrice { get; set; }
        public string BatchRetailPrice { get; set; }
        public string ListPrice { get; set; }
        public string RetailPrice { get; set; }
        public string ReceivedQty { get; set; }
        public string InventoryOnHand { get; set; }
        public string UOM { get; set; }
        public string ExpiryDate { get; set; }
        public string WithVAT { get; set; }
        public string IsActive { get; set; }
    }
}