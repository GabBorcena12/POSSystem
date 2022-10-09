using POSSystem.Models;
using POSSystem.Repository;
using POSSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSSystem.Controllers
{
    public class ManagementController : Controller
    {
         
        public ActionResult Index()
        {
            return View();
        }
        //CATEGORY
        [HttpGet]
        public ActionResult CategoryManagement()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listCategory = new List<Category>();
            InitializeCategoryManagement(viewModel);

            return View(viewModel);
        }

        private void InitializeCategoryManagement(MonitoringViewModel viewModel) {

            ManagementSettings repos = new ManagementSettings();
            UserSettings repos2 = new UserSettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetCategoryList(viewModel.usersession.UserName);

            DataMapper<Category> mapper = new DataMapper<Category>();
            viewModel.listCategory = mapper.Map(ds.Tables[0]).ToList();

            viewModel.StatusList = new List<SelectListItem>();
            var StatusList = repos2.CodeDecodeList("ItemStatusList");
            StatusList.ForEach(x => viewModel.StatusList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

        }
        [HttpPost]
        public ActionResult CategoryManagement(MonitoringViewModel viewModel)
        {
            ManagementSettings repos = new ManagementSettings(); 

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }

            switch (viewModel.ActionButton)
            {
                case "Post":
                        var trans = repos.PostCategoryManagement(viewModel.Category, viewModel.usersession.UserName);

                        ViewBag.Code = trans.Code;
                        ViewBag.Message = trans.Message;
                    break;
                default:
                    break;
            }

            InitializeCategoryManagement(viewModel);
            return View(viewModel);
        }

         
        public ActionResult VendorManagement()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listVendor = new List<Vendor>();
            InitializeVendorManagement(viewModel);
            return View(viewModel);
        }
        private void InitializeVendorManagement(MonitoringViewModel viewModel) {

            ManagementSettings repos = new ManagementSettings();
            UserSettings repos2 = new UserSettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetVendorList(viewModel.usersession.UserName);

            DataMapper<Vendor> mapper = new DataMapper<Vendor>();
            viewModel.listVendor = mapper.Map(ds.Tables[0]).ToList();

            viewModel.StatusList = new List<SelectListItem>();
            var StatusList = repos2.CodeDecodeList("ItemStatusList");
            StatusList.ForEach(x => viewModel.StatusList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));
        }
        [HttpPost]
        public ActionResult VendorManagement(MonitoringViewModel viewModel)
        {
            ManagementSettings repos = new ManagementSettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }

            switch (viewModel.ActionButton)
            {
                case "Post":
                    var trans = repos.PostVendorManagement(viewModel.Vendor, viewModel.usersession.UserName);

                    ViewBag.Code = trans.Code;
                    ViewBag.Message = trans.Message;
                    break;
                default:
                    break;
            }

            InitializeVendorManagement(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult PromoManagement()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel
            {
                usersession = new EmployeeDetails(),
                Promo = new Promo(),
                listPromo = new List<Promo>(),
                PromoDetails = new PromoDetails(),
                listPromoDetails = new List<PromoDetails>() 
            };
            InitializePromoManagement(viewModel);

            return View(viewModel);
        }
        private void InitializePromoManagement(MonitoringViewModel viewModel) {
            ManagementSettings repos = new ManagementSettings(); 

            viewModel.usersession = new EmployeeDetails();
            viewModel.Promo = new Promo();
            viewModel.listPromo = new List<Promo>();
            viewModel.PromoDetails = new PromoDetails();
            viewModel.listPromoDetails = new List<PromoDetails>();

            UserSettings repos2 = new UserSettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetPromoList(viewModel.usersession.UserName);

            DataMapper<Promo> mapper = new DataMapper<Promo>();
            viewModel.listPromo = mapper.Map(ds.Tables[0]).ToList();

            viewModel.StatusList = new List<SelectListItem>();
            var StatusList = repos2.CodeDecodeList("ItemStatusList");
            StatusList.ForEach(x => viewModel.StatusList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

            viewModel.Type = new List<SelectListItem>();
            var Type = repos2.CodeDecodeList("PDType");
            Type.ForEach(x => viewModel.Type.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));


            viewModel.SKUList = new List<SelectListItem>();
            viewModel.SKUList.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            var SKUList = repos.FilterInventorySKU();
            SKUList.ForEach(x => viewModel.SKUList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

            viewModel.VariantList = new List<SelectListItem>();
            viewModel.VariantList.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            var VariantList = repos.FilterInventoryVariantNo("");
            VariantList.ForEach(x => viewModel.VariantList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

            viewModel.BatchList = new List<SelectListItem>(); 
            viewModel.BatchList.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            var BatchList = repos.FilterInventoryBatchNbr("","");
            BatchList.ForEach(x => viewModel.BatchList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

        }
        [HttpPost]
        public ActionResult PromoManagement(MonitoringViewModel viewModel)
        {
            ManagementSettings repos = new ManagementSettings();
            
                if (Session["USER_SESSION"] != null)
                {
                    viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
                }

            switch (viewModel.ActionButton)
            {
                case "Post":
                    var trans = repos.PostPromoManagement(viewModel.Promo, viewModel.listPromoDetails, viewModel.usersession.UserName);

                    ViewBag.Code = trans.Code;
                    ViewBag.Message = trans.Message;
                    break;
                default:
                    break;

            }
                InitializePromoManagement(viewModel);
             
            return View(viewModel);
        }

        public ActionResult InventoryManagement()
        {
            ManagementSettings repos = new ManagementSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listInventory = new List<Inventory>();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetInventoryList(viewModel.usersession.UserName);

            DataMapper<Inventory> mapper = new DataMapper<Inventory>();
            viewModel.listInventory = mapper.Map(ds.Tables[0]).ToList();

            return View(viewModel);
        }
        public ActionResult BatchManagement()
        {
            ManagementSettings repos = new ManagementSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listBatch = new List<Batch>();
            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetBatchList(viewModel.usersession.UserName);

            DataMapper<Batch> mapper = new DataMapper<Batch>();
            viewModel.listBatch = mapper.Map(ds.Tables[0]).ToList();

            return View(viewModel);
        }
        public ActionResult ViewAll()
        {
            ManagementSettings repos = new ManagementSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listSession = new List<Session>();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetSessionList(viewModel.usersession.UserName);

            DataMapper<Session> mapper = new DataMapper<Session>();
            viewModel.listSession = mapper.Map(ds.Tables[0]).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult FilterVariantNo(string param1)
        {
            ManagementSettings repos = new ManagementSettings();

            var variantlist = repos.FilterInventoryVariantNo(param1);
            var result = from a in variantlist
                         select new
                         {
                             flag = 0,
                             cd = a.DecodeTxt,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FilterVariantNo2(string param1)
        {
            ManagementSettings repos = new ManagementSettings();

            var variantlist = repos.FilterVariantNo(param1);
            var result = from a in variantlist
                         select new
                         {
                             flag = 0,
                             cd = a.DecodeTxt,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FilterBatchNbr(string param1,string param2)
        {
            ManagementSettings repos = new ManagementSettings();

            var variantlist = repos.FilterInventoryBatchNbr(param1,param2);
            var result = from a in variantlist
                         select new
                         {
                             flag = 0,
                             cd = a.DecodeTxt,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public JsonResult FilterProductPrice(string param1, string param2, string param3)
        {
            ManagementSettings repos = new ManagementSettings();

            var list = repos.FilterProductPrice(param1, param2, param3);
            var result = from a in list
                         select new
                         {
                             flag = 0,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInventoryOnHand(string param1, string param2, string param3)
        {
            ManagementSettings repos = new ManagementSettings();

            var list = repos.GetInventoryOnHand(param1, param2, param3);
            var result = from a in list
                         select new
                         {
                             cd = a.CodeTxt,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult FilterVendorDetails(string param1)
        {
            ManagementSettings repos = new ManagementSettings();

            var list = repos.FilterVendorDetails(param1);
            var result = from a in list
                         select new
                         {
                             VendorNm = a.VendorNm,
                             VendorLocation = a.VendorLocation,
                             ContactPerson = a.ContactPerson,
                             ContactNbr = a.ContactNbr 
                        };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ProductMaintenance()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listProducts = new List<Product>(); 
            InitializeProductMaintenance(viewModel);

            return View(viewModel);
        }

        private void InitializeProductMaintenance(MonitoringViewModel viewModel)
        {

            ManagementSettings repos = new ManagementSettings();
            UserSettings repos2 = new UserSettings();
            InventorySettings repos3 = new InventorySettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetInitializeProductMaintenance(viewModel.usersession.UserName);

            DataMapper<Product> mapper = new DataMapper<Product>();
            viewModel.listProducts = mapper.Map(ds.Tables[0]).ToList();

            DataMapper<TransactionResult> mapper1 = new DataMapper<TransactionResult>();
            var result = mapper1.Map(ds.Tables[1].Rows[0]);
             
            ViewBag.GetStatus = result.Code;
            ViewBag.GetMessage = result.Message;

            viewModel.IdentifierList = new List<SelectListItem>();
            viewModel.IdentifierList.Add(new SelectListItem { Text = " -- Select Type --", Value = "",Selected=true });
            viewModel.IdentifierList.Add(new SelectListItem { Text = "Calculate by Percentage", Value = "Percentage" });
            viewModel.IdentifierList.Add(new SelectListItem { Text = "Calculate by Exact Amount", Value = "Amount"  });
             

            viewModel.ProdCategoryList = new List<SelectListItem>();
            viewModel.ProdCategoryList.Add(new SelectListItem { Text = " -- Select Categories -- ", Value = "", Selected = true });
            var ProdCategoryList = repos3.GetProdCategoryList();
            ProdCategoryList.ForEach(x => viewModel.ProdCategoryList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));


            viewModel.VendorList = new List<SelectListItem>();
            viewModel.VendorList.Add(new SelectListItem { Text = " -- Select Vendor -- ", Value = "", Selected = true });
            var VendorList = repos3.GetVendorList();
            VendorList.ForEach(x => viewModel.VendorList.Add(new SelectListItem { Text = x.VendorCd + " - " + x.VendorNm, Value = x.VendorCd }));
             
        }

        [HttpPost]
        public ActionResult ProductMaintenance(MonitoringViewModel viewModel)
        {
            ManagementSettings repos = new ManagementSettings();
              
            switch (viewModel.ActionButton)
            {
                case "New":
                    var trans = repos.PostProductMaintenance(viewModel.Product, viewModel.usersession.UserName, viewModel.ActionButton);

                    ViewBag.Status = trans.Code;
                    ViewBag.Message = trans.Message;
                    break;
                case "Edit":
                    var trans2 = repos.PostProductMaintenance(viewModel.Product, viewModel.usersession.UserName, viewModel.ActionButton);

                    ViewBag.Status = trans2.Code;
                    ViewBag.Message = trans2.Message;
                    break;
                default:
                    break;
            }

            InitializeProductMaintenance(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        public JsonResult GetSKUList(string param)
        {
            ManagementSettings repos = new ManagementSettings();

            var sbulist = repos.GetSKUMasterList();
            var result = from a in sbulist
                         select new
                         {
                             flag = 0,
                             cd = a.DecodeTxt,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetVariantNoList(string param)
        {
            ManagementSettings repos = new ManagementSettings();

            var sbulist = repos.FilterVariantNo(param);
            var result = from a in sbulist
                         select new
                         {
                             flag = 0,
                             cd = a.DecodeTxt,
                             txt = a.DecodeTxt
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult NewLineChart(string strOpts1, string strOpts2, string strOpts3, string strOpts4, string strOpts5, string strOpts6,
            string strOpts7, string strOpts8, string strOpts9, string strOpts10, string strOpts11, string strOpts12,
           decimal opts1, decimal opts2, decimal opts3, decimal opts4, decimal opts5, decimal opts6, decimal opts7, decimal opts8, decimal opts9,
           decimal opts10, decimal opts11, decimal opts12)
        {
            opts1 = Math.Round(opts1, 0);
            opts2 = Math.Round(opts2, 0);
            opts3 = Math.Round(opts3, 0);
            opts4 = Math.Round(opts4, 0);
            opts5 = Math.Round(opts5, 0);
            opts6 = Math.Round(opts6, 0);
            opts7 = Math.Round(opts7, 0);
            opts8 = Math.Round(opts8, 0);
            opts9 = Math.Round(opts9, 0);
            opts10 = Math.Round(opts10, 0);
            opts11 = Math.Round(opts11, 0);
            opts12 = Math.Round(opts12, 0); 

            List<object> iData = new List<object>();
            //Creating sample data    
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValues", System.Type.GetType("System.Int32"));

            //For opts  
            DataRow dr = dt.NewRow();
            dr["Expense"] = strOpts1;
            dr["ExpenseValues"] = opts1;
            dt.Rows.Add(dr);
             
            dr = dt.NewRow();
            dr["Expense"] = strOpts2;
            dr["ExpenseValues"] = opts2;
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["Expense"] = strOpts3;
            dr["ExpenseValues"] = opts3;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts4;
            dr["ExpenseValues"] = opts4;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts5;
            dr["ExpenseValues"] = opts5;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts6;
            dr["ExpenseValues"] = opts6;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts7;
            dr["ExpenseValues"] = opts7;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts8;
            dr["ExpenseValues"] = opts8;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts9;
            dr["ExpenseValues"] = opts9;

            dr = dt.NewRow();
            dr["Expense"] = strOpts10;
            dr["ExpenseValues"] = opts10;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts11;
            dr["ExpenseValues"] = opts11;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts12;
            dr["ExpenseValues"] = opts12; 
            dt.Rows.Add(dr);

            //Looping and extracting each DataColumn to List<Object>    
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            ViewBag.ChartData = iData;
            //Source data returned as JSON    
            return Json(iData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult NewBarChart(string strOpts1, string strOpts2, string strOpts3, string strOpts4, string strOpts5, string strOpts6,
            string strOpts7, string strOpts8, string strOpts9, string strOpts10,
           decimal opts1, decimal opts2, decimal opts3, decimal opts4, decimal opts5, decimal opts6, decimal opts7, decimal opts8, decimal opts9,
           decimal opts10)
        {
            opts1 = Math.Round(opts1, 0);
            opts2 = Math.Round(opts2, 0);
            opts3 = Math.Round(opts3, 0);
            opts4 = Math.Round(opts4, 0);
            opts5 = Math.Round(opts5, 0);
            opts6 = Math.Round(opts6, 0);
            opts7 = Math.Round(opts7, 0);
            opts8 = Math.Round(opts8, 0);
            opts9 = Math.Round(opts9, 0);
            opts10 = Math.Round(opts10, 0); 

            List<object> iData = new List<object>();
            //Creating sample data    
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValues", System.Type.GetType("System.Int32"));

            //For opts  
            DataRow dr = dt.NewRow();
            dr["Expense"] = strOpts1;
            dr["ExpenseValues"] = opts1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts2;
            dr["ExpenseValues"] = opts2;
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["Expense"] = strOpts3;
            dr["ExpenseValues"] = opts3;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts4;
            dr["ExpenseValues"] = opts4;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts5;
            dr["ExpenseValues"] = opts5;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts6;
            dr["ExpenseValues"] = opts6;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts7;
            dr["ExpenseValues"] = opts7;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts8;
            dr["ExpenseValues"] = opts8;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Expense"] = strOpts9;
            dr["ExpenseValues"] = opts9;

            dr = dt.NewRow();
            dr["Expense"] = strOpts10;
            dr["ExpenseValues"] = opts10;
            dt.Rows.Add(dr);
             

            //Looping and extracting each DataColumn to List<Object>    
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            ViewBag.ChartData = iData;
            //Source data returned as JSON    
            return Json(iData, JsonRequestBehavior.AllowGet);
        }
    }


}