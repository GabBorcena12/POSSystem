using POSSystem.Models;
using POSSystem.Repository;
using POSSystem.ViewModel;
using POSSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Razor;

namespace POSSystem.Controllers
{
    public class AccountController:Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ViewFormViewModel viewModel = new ViewFormViewModel()
            {
                userAuthentication = new UserAuthentication()
            };
            return View(viewModel);
        }
        // POST: Account
        [HttpPost]
        public ActionResult Login(ViewFormViewModel viewModel)
        {
            UserSettings repos = new UserSettings();

            DataSet ds = repos.AuthenticateLogIn(viewModel.userAuthentication.Username, viewModel.userAuthentication.Password);  

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataMapper<UserAuthentication> mapper = new DataMapper<UserAuthentication>();
                    var userDetails = mapper.Map(ds.Tables[0].Rows[0]);


                    DataMapper<BaseMessage> mapper2 = new DataMapper<BaseMessage>();
                    var baseMessage = mapper2.Map(ds.Tables[1].Rows[0]);

                    if (userDetails.isAuthenticated == "Y")
                    {
                        var employeeDetails = repos.GetEmployeeDetails(viewModel.userAuthentication.Username);

                        if (employeeDetails.UserName != null)
                        {
                            Session["USER_SESSION"] = employeeDetails;
                        }
                        else
                        {
                            // Redirect to error page (Failed to authenticate user due to null userId).
                            // Log to ErrorLogging
                        }

                        ViewBag.Status = baseMessage.Code;
                        ViewBag.Message = baseMessage.Message;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Status = baseMessage.Code;
                        ViewBag.Message = baseMessage.Message;
                    }
                }
            }


            return View();
        }
        [HttpGet]
        public ActionResult UserManagement() {
            UserSettings repos = new UserSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];                
            }
            var ds = repos.UserAccountList(viewModel.usersession.UserName);

            DataMapper<EmployeeDetails> tranWorklistMapper = new DataMapper<EmployeeDetails>();
            viewModel.listemployeeDetails = tranWorklistMapper.Map(ds.Tables[0]).ToList();

            return View(viewModel);
        }
        [HttpGet] 
        public ActionResult LogOff()
        {

            UserSettings repos = new UserSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();
            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }
            var userid = viewModel.usersession.UserName;
            var ds = repos.LogSession(userid, "Log Off");

            Session["USER_SESSION"] = null;
            viewModel.usersession = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(ViewFormViewModel model)
        { 
            return RedirectToAction("Login", "Account");
        } 
        [HttpGet]
        public ActionResult ForgotPassword()
        {

            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            UserRegistration model = new UserRegistration();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegistration model)
        {
            UserSettings repos = new UserSettings();

            if (ModelState.IsValid)
            {
                var transaction = repos.RegisterUser(model);
                ViewBag.Status = transaction.Code;
                ViewBag.Message = transaction.Message;
            }
            //return RedirectToAction("Index", "Home", new { area = "" });
            return View();
        }
         
        [HttpGet]
        public ActionResult UserProfile(string Id)
        {

            MonitoringViewModel viewmodel = new MonitoringViewModel
            {
                usersession = new EmployeeDetails(),
                employeeDetails = new EmployeeDetails()

            };

            InitializeUserProfile(Id, viewmodel);
            return View(viewmodel);
        }

        private void InitializeUserProfile(string Id, MonitoringViewModel viewmodel) {
            UserSettings repos = new UserSettings(); 
            viewmodel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"]; 
            DataSet ds = repos.ViewProfile(Id, viewmodel.usersession.UserName);


            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataMapper<TransactionResult> mapper2 = new DataMapper<TransactionResult>();
                    var cd = mapper2.Map(ds.Tables[1].Rows[0]);
                     
                        ViewBag.GetStatus = cd.Code;
                        ViewBag.GetMessage = cd.Message; 

                    if (cd.Code != "0")
                    {
                        DataMapper<EmployeeDetails> mapper = new DataMapper<EmployeeDetails>();
                        var profile = mapper.Map(ds.Tables[0].Rows[0]);

                        viewmodel.employeeDetails.Id = profile.Id;
                        viewmodel.employeeDetails.UserName = profile.UserName;
                        viewmodel.employeeDetails.Role = profile.Role;
                        viewmodel.employeeDetails.FirstNm = profile.FirstNm;
                        viewmodel.employeeDetails.LastNm = profile.LastNm;
                        viewmodel.employeeDetails.MiddleNm = profile.MiddleNm;
                        viewmodel.employeeDetails.IsActive = profile.IsActive;
                        viewmodel.employeeDetails.Email = profile.Email;
                        viewmodel.employeeDetails.PhoneNumber = profile.PhoneNumber;
                    }

                }
            }

            viewmodel.StatusList = new List<SelectListItem>();
            var StatusList = repos.CodeDecodeList("StatusList");
            StatusList.ForEach(x => viewmodel.StatusList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

            viewmodel.RoleList = new List<SelectListItem>();
            var RoleList = repos.CodeDecodeList("RoleList");
            RoleList.ForEach(x => viewmodel.RoleList.Add(new SelectListItem { Text = x.DecodeTxt, Value = x.CodeTxt }));

        }
        [HttpPost]
        public ActionResult UserProfile(MonitoringViewModel viewModel) {

            UserSettings repos = new UserSettings();
            viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];

            switch (viewModel.ActionButton)
            {
                case "Update":
                    var transactionResult = repos.UpdateUserProfile(new EmployeeDetails
                    {
                        Id = viewModel.employeeDetails.Id,
                        FirstNm = viewModel.employeeDetails.FirstNm,
                        LastNm = viewModel.employeeDetails.LastNm,
                        MiddleNm = viewModel.employeeDetails.MiddleNm,
                        PhoneNumber = viewModel.employeeDetails.PhoneNumber,
                        Email = viewModel.employeeDetails.Email,
                        Role = viewModel.employeeDetails.Role,
                        IsActive = viewModel.employeeDetails.IsActive
                    },viewModel.usersession.UserName);

                    ViewBag.Status = transactionResult.Code;
                    ViewBag.Message = transactionResult.Message; 

                    if (transactionResult.Code == "1")
                    {
                        viewModel.ActionButton = string.Empty; 
                    }

                    break; 
                default:
                    ViewBag.Status = "0";
                    ViewBag.Message = "Invalid operation for the specified action.";
                    break;
            }
            InitializeUserProfile(viewModel.employeeDetails.Id, viewModel);
            return View(viewModel);
        } 
    }
}