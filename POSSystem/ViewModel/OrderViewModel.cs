using POSSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSSystem.ViewModel
{
    public class OrderViewModel
    {  
        public EmployeeDetails employeeDetails { get; set; }
        public EmployeeDetails usersession { get; set; } 
        public Product product { get; set; }
        public List<Product> listproduct { get; set; }
        public Batch batch { get; set; }
        public List<SelectListItem> VendorList { get; set; }
        public string ActionButton { get; set; }

        public List<SelectListItem> SKUList { get; set; }
        public List<SelectListItem> BatchList { get; set; }
        public List<SelectListItem> VariantList { get; set; }
        public List<SelectListItem> ProdCategoryList { get; set; }
        public List<SelectListItem> YesNoList { get; set; }
    }
}