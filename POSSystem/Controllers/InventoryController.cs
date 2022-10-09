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
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult ApprovalForm(string Id)
        {

            OrderViewModel viewModel = new OrderViewModel()
            {
                employeeDetails = new EmployeeDetails(),
                batch = new Batch(),
                listproduct = new List<Product>()
            };

            InitializeApprovalForm(Id,viewModel);
            return View(viewModel);
        }

        private void InitializeApprovalForm(string Id,OrderViewModel viewModel)
        {

            InventorySettings repos = new InventorySettings();
            ManagementSettings repos2 = new ManagementSettings();
            viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];

            DataSet ds = repos.GetInventoryDetails(Id, viewModel.usersession.UserName);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataMapper<TransactionResult> mapper3 = new DataMapper<TransactionResult>();
                    var cd = mapper3.Map(ds.Tables[3].Rows[0]);

                    ViewBag.GetStatus = cd.Code;
                    ViewBag.GetMessage = cd.Message;

                    DataMapper<Batch> mapper0 = new DataMapper<Batch>();
                    var profile = mapper0.Map(ds.Tables[0].Rows[0]);

                    viewModel.batch.BatchNbr = profile.BatchNbr;
                    viewModel.batch.PaidAmt = profile.PaidAmt;
                    viewModel.batch.ReceiverNm = profile.ReceiverNm;
                    viewModel.batch.ReceivedDate = profile.ReceivedDate;


                    DataMapper<Batch> mapper = new DataMapper<Batch>();
                    var vendor = mapper.Map(ds.Tables[1].Rows[0]);

                    viewModel.batch.VendorCd = vendor.VendorCd;
                    viewModel.batch.VendorNm = vendor.VendorNm;
                    viewModel.batch.VendorLocation = vendor.VendorLocation;
                    viewModel.batch.vendorContactPerson = vendor.vendorContactPerson;
                    viewModel.batch.VendorContactNbr = vendor.VendorContactNbr;


                    DataMapper<Product> mapper2 = new DataMapper<Product>();
                    viewModel.listproduct = mapper2.Map(ds.Tables[2]).ToList(); 

                }
            }
        }
        [HttpPost]
        public ActionResult ApprovalForm(OrderViewModel viewModel)
        {
            InventorySettings repos = new InventorySettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }

            switch (viewModel.ActionButton)
            {
                case "Approve":
                    var trans = repos.PostApprovalForm(viewModel.ActionButton, viewModel.usersession.UserName, viewModel.batch.BatchNbr);

                    ViewBag.Status = trans.Code;
                    ViewBag.Message = trans.Message;
                    break;
                case "Reject":
                    var trans1 = repos.PostApprovalForm(viewModel.ActionButton, viewModel.usersession.UserName, viewModel.batch.BatchNbr);

                    ViewBag.Status = trans1.Code;
                    ViewBag.Message = trans1.Message;
                    break;
                default:
                    break;

            }
            InitializeApprovalForm(viewModel.batch.BatchNbr, viewModel);

            return View(viewModel);
        }
        public ActionResult OrderReceiving()
        {

            OrderViewModel viewModel = new OrderViewModel() {
                employeeDetails = new EmployeeDetails(),
                batch = new Batch(),
                listproduct = new List<Product>()
            };
            InitializeOrderReceiving(viewModel);
            return View(viewModel);
        }

        private void InitializeOrderReceiving(OrderViewModel viewModel) {

            InventorySettings repos = new InventorySettings();
            ManagementSettings repos2 = new ManagementSettings();
            viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];

            DataSet ds = repos.GetOrderReceiving(viewModel.usersession.UserName);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataMapper<TransactionResult> mapper3 = new DataMapper<TransactionResult>();
                    var cd = mapper3.Map(ds.Tables[2].Rows[0]);

                    ViewBag.GetStatus = cd.Code;
                    ViewBag.GetMessage = cd.Message;

                    DataMapper<EmployeeDetails> mapper0 = new DataMapper<EmployeeDetails>();
                    var profile = mapper0.Map(ds.Tables[0].Rows[0]);

                    viewModel.employeeDetails.FullNm = profile.FullNm;
                    viewModel.employeeDetails.FirstNm = profile.FirstNm;
                    viewModel.employeeDetails.MiddleNm = profile.MiddleNm;
                    viewModel.employeeDetails.LastNm = profile.LastNm;
                    viewModel.employeeDetails.Role = profile.Role;

                    DataMapper<Batch> mapper1 = new DataMapper<Batch>();
                    var requestheader = mapper1.Map(ds.Tables[1].Rows[0]);

                    viewModel.batch.BatchNbr = requestheader.BatchNbr;
                    viewModel.batch.PaidAmt = "0.00";

                    if (viewModel.listproduct == null)
                    {
                        viewModel.listproduct = new List<Product>();
                    }


                    viewModel.ProdCategoryList = new List<SelectListItem>();
                    viewModel.ProdCategoryList.Add(new SelectListItem { Text = " -- Select Categories -- ", Value = "", Selected = true });
                    var ProdCategoryList = repos.GetProdCategoryList();
                    ProdCategoryList.ForEach(x => viewModel.ProdCategoryList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));


                    viewModel.VendorList = new List<SelectListItem>();
                    viewModel.VendorList.Add(new SelectListItem { Text = " -- Select Vendor -- ", Value = "", Selected = true });
                    var VendorList = repos.GetVendorList();
                    VendorList.ForEach(x => viewModel.VendorList.Add(new SelectListItem { Text = x.VendorCd + " - " + x.VendorNm, Value = x.VendorCd }));

                    viewModel.SKUList = new List<SelectListItem>();
                    viewModel.SKUList.Add(new SelectListItem { Text = " -- Select SKU -- ", Value = "", Selected = true });
                    var SKUList = repos2.GetSKUMasterList();
                    SKUList.ForEach(x => viewModel.SKUList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));


                    viewModel.VariantList = new List<SelectListItem>();
                    viewModel.VariantList.Add(new SelectListItem { Text = " -- Select Variant No -- ", Value = "", Selected = true });
                    var VariantList = repos2.GetVariantNoMasterList();
                    VariantList.ForEach(x => viewModel.VariantList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

                    viewModel.YesNoList = new List<SelectListItem>();
                    viewModel.YesNoList.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
                    viewModel.YesNoList.Add(new SelectListItem { Text = "No", Value = "No" });

                }
            }
        }
        [HttpPost]
        public ActionResult OrderReceiving(OrderViewModel viewModel)
        {
            InventorySettings repos = new InventorySettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }

            switch (viewModel.ActionButton)
            {
                case "Submit":
                    var trans = repos.PostOrderReceiving(viewModel.batch,viewModel.employeeDetails, viewModel.listproduct,viewModel.usersession.UserName);

                    ViewBag.Status = trans.Code;
                    ViewBag.Message = trans.Message;
                    break;
                default:
                    break;

            }
            InitializeOrderReceiving(viewModel);

            return View(viewModel);
        }
        [HttpPost]
        public JsonResult FilterProductDetails(string param1, string param2)
        {
            InventorySettings repos = new InventorySettings();

            var details = repos.FilterProductDetails(param1, param2);
            var result = from a in details
                         select new
                         {
                             flag = 0,
                             ProductDesc1 = a.ProductDesc1,
                             ProductDesc2 = a.ProductDesc2,
                             ProductDesc3 = a.ProductDesc3,
                             UOM = a.UOM,
                             ProductCategory = a.ProductCategory
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult OrderApproval()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listBatch = new List<Batch>();
            InitializeOrderApproval(viewModel);

            return View(viewModel);
        }

        private void InitializeOrderApproval(MonitoringViewModel viewModel)
        {

            InventorySettings repos = new InventorySettings();
            UserSettings repos2 = new UserSettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.GetOrderApproval(viewModel.usersession.UserName);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataMapper<Batch> mapper = new DataMapper<Batch>();
                    viewModel.listBatch = mapper.Map(ds.Tables[0]).ToList();
                }
            }

        }

        [HttpGet]
        public ActionResult SubmittedList()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.listBatch = new List<Batch>();
            InitializeSubmittedList(viewModel);

            return View(viewModel);
        }

        private void InitializeSubmittedList(MonitoringViewModel viewModel)
        {

            InventorySettings repos = new InventorySettings();
            UserSettings repos2 = new UserSettings();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var ds = repos.SubmittedList(viewModel.usersession.UserName);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataMapper<Batch> mapper = new DataMapper<Batch>();
                    viewModel.listBatch = mapper.Map(ds.Tables[0]).ToList();
                }
            }

        }

        // GET: Inventory
        public ActionResult ViewForm(string Id)
        {

            OrderViewModel viewModel = new OrderViewModel()
            {
                employeeDetails = new EmployeeDetails(),
                batch = new Batch(),
                listproduct = new List<Product>()
            };

            InitializeViewForm(Id, viewModel);
            return View(viewModel);
        }
        public ActionResult PrintForm(string Id)
        {

            OrderViewModel viewModel = new OrderViewModel()
            {
                employeeDetails = new EmployeeDetails(),
                batch = new Batch(),
                listproduct = new List<Product>()
            };

            InitializeViewForm(Id, viewModel);
            return View(viewModel);
        }

        private void InitializeViewForm(string Id, OrderViewModel viewModel)
        {

            InventorySettings repos = new InventorySettings();
            ManagementSettings repos2 = new ManagementSettings();
            viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];

            DataSet ds = repos.GetInventoryDetails(Id, viewModel.usersession.UserName);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataMapper<TransactionResult> mapper3 = new DataMapper<TransactionResult>();
                    var cd = mapper3.Map(ds.Tables[3].Rows[0]);

                    ViewBag.GetStatus = cd.Code;
                    ViewBag.GetMessage = cd.Message;

                    DataMapper<Batch> mapper0 = new DataMapper<Batch>();
                    var profile = mapper0.Map(ds.Tables[0].Rows[0]);

                    viewModel.batch.BatchNbr = profile.BatchNbr;
                    viewModel.batch.PaidAmt = profile.PaidAmt;
                    viewModel.batch.ReceiverNm = profile.ReceiverNm;
                    viewModel.batch.ReceivedDate = profile.ReceivedDate;
                    viewModel.batch.Status = profile.Status;


                    DataMapper<Batch> mapper = new DataMapper<Batch>();
                    var vendor = mapper.Map(ds.Tables[1].Rows[0]);

                    viewModel.batch.VendorCd = vendor.VendorCd;
                    viewModel.batch.VendorNm = vendor.VendorNm;
                    viewModel.batch.VendorLocation = vendor.VendorLocation;
                    viewModel.batch.vendorContactPerson = vendor.vendorContactPerson;
                    viewModel.batch.VendorContactNbr = vendor.VendorContactNbr;


                    DataMapper<Product> mapper2 = new DataMapper<Product>();
                    viewModel.listproduct = mapper2.Map(ds.Tables[2]).ToList();

                }
            }
        }
    }
}