using POSSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSSystem.ViewModel
{
    public class MonitoringViewModel
    { 
        public List<EmployeeDetails> listemployeeDetails { get; set; }
        public EmployeeDetails employeeDetails { get; set; }
        public EmployeeDetails usersession { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> IdentifierList { get; set; }
        public List<SelectListItem> VendorList { get; set; }
        public List<Category> listCategory { get; set; }
        public Category Category { get; set; }
        public List<Vendor> listVendor { get; set; }
        public Vendor Vendor { get; set; }
        public List<Promo> listPromo { get; set; }
        public Promo Promo { get; set; }
        public List<Inventory> listInventory { get; set; }
        public Inventory Inventory { get; set; }
        public List<Batch> listBatch { get; set; }
        public Batch Batch { get; set; }
        public Session Session { get; set; }
        public List<Session> listSession { get; set; }
        public List<SelectListItem> Type { get; set; }
        public List<PromoDetails> listPromoDetails { get; set; }
        public PromoDetails PromoDetails { get; set; }
        public List<SelectListItem> SKUList { get; set; }
        public List<SelectListItem> BatchList { get; set; }
        public List<SelectListItem> VariantList { get; set; }
        public List<SelectListItem> ProdCategoryList { get; set; }
        public string ActionButton { get; set; }
        public List<Product> listProducts { get; set; }
        public Product Product { get; set; }
        public List<Chat> chatlist { get; set; }
        public Chat Chat { get; set; }
    }
}