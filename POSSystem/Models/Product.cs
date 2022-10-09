using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Product
    {
        public string ItemId { get; set; }
        public string VariantNbr { get; set; }
        public string SKU { get; set; }
        public string ProductDesc1 { get; set; }
        public string ProductDesc2 { get; set; }
        public string ProductDesc3 { get; set; }
        public string ProductCategory { get; set; }
        public string BatchNbr { get; set; }
        public string BatchListPrice { get; set; }
        public string BatchRetailAmount { get; set; }
        public string ReceivedQty { get; set; }
        public string ListPrice { get; set; }
        public string RetailPrice { get; set; }
        public string InventoryOnHand { get; set; }
        public string UOM { get; set; }
        public string ExpiryDate { get; set; }
        public string WithVat { get; set; }
        public string IsActive { get; set; }
        public string NetIncome { get; set; }
        public string VendorCd { get; set; }
        public string VendorNm { get; set; }
        public string NetIncomeIdentifier { get; set; }
    }
}