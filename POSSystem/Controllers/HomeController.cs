using POSSystem.Models;
using POSSystem.Repository;
using POSSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.chatlist = new List<Chat>();
            viewModel.Chat = new Chat();
            InitializeDashBoard(viewModel); 

            return View(viewModel);
        }
        public ActionResult UpdateChatDetails()
        {
            ManagementSettings repos = new ManagementSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();
            viewModel.chatlist = new List<Chat>();


            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
                var ds = repos.GetChatDetails(viewModel.usersession.UserName, "Admin");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataMapper<Chat> mapper2 = new DataMapper<Chat>();
                        viewModel.chatlist = mapper2.Map(ds.Tables[0]).ToList();
                    }
                }
            }
            else {
                viewModel.chatlist = new List<Chat>();
            }

            return PartialView("_ChatBox", viewModel); 
        }

         
        private void InitializeDashBoard(MonitoringViewModel viewModel) {

            ManagementSettings repos = new ManagementSettings();
            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];
            }

            var ds = repos.GetChatDetails(viewModel.usersession.UserName, "Admin"); 

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataMapper<Chat> mapper2 = new DataMapper<Chat>();
                    viewModel.chatlist = mapper2.Map(ds.Tables[0]).ToList();


                    DataMapper<Chat> mapper3 = new DataMapper<Chat>();
                    var profile = mapper3.Map(ds.Tables[1].Rows[0]);
                    viewModel.Chat.ReceiverNm = profile.ReceiverNm;
                    viewModel.Chat.Notification = profile.Notification;
                }
            }

            //Info Box Data
            ViewBag.Info1 = 50;
            ViewBag.Info2 = 20;
            ViewBag.Info3 = 15;
            ViewBag.Info4 = 15;
            ViewBag.Info5 = 2000000;
            ViewBag.InfoPercentage5 = 30;
            ViewBag.Info6 = 40000;
            ViewBag.InfoPercentage6 = 40;
            ViewBag.Info7 = 10;
            ViewBag.InfoPercentage7 = 50;
            ViewBag.Info8 = 10;
            ViewBag.InfoPercentage8 = 20;
            //Info Box Label
            ViewBag.Label1 = "Request Submitted";
            ViewBag.Label2 = "Pending Requests";
            ViewBag.Label3 = "Approved Requests";
            ViewBag.Label4 = "Rejected Requests";
            ViewBag.Label5 = "Total Sales";
            ViewBag.Label6 = "Total Transactions";
            ViewBag.Label7 = "Total Products";
            ViewBag.Label8 = "Total Users";

            //Line Chart Data
            ViewBag.Opts1 = 5000;
            ViewBag.Opts2 = 4500;
            ViewBag.Opts3 = 4000;
            ViewBag.Opts4 = 13500;
            ViewBag.Opts5 = 3000;
            ViewBag.Opts6 = 2500;
            ViewBag.Opts7 = 20200;
            ViewBag.Opts8 = 1500;
            ViewBag.Opts9 = 11000;
            ViewBag.Opts10 = 22500;
            ViewBag.Opts11 = 2250;
            ViewBag.Opts12 = 11200;

            //Bar Chart Label
            ViewBag.Top1 = "Skyflakes";
            ViewBag.Top2 = "Cream-O Choco";
            ViewBag.Top3 = "Sunsilk Shampoo";
            ViewBag.Top4 = "Lysol";
            ViewBag.Top5 = "Alcohol";
            ViewBag.Top6 = "Kopiko 3 in 1";
            ViewBag.Top7 = "Pampangas Best Tocino";
            ViewBag.Top8 = "Tender Juicy Hotodg";
            ViewBag.Top9 = "Off Lotion";
            ViewBag.Top10 = "Paracetamol";


            //Bar Chart Data
            ViewBag.ChartOpts1 = 5000;
            ViewBag.ChartOpts2 = 4500;
            ViewBag.ChartOpts3 = 4000;
            ViewBag.ChartOpts4 = 3500;
            ViewBag.ChartOpts5 = 3000;
            ViewBag.ChartOpts6 = 2500;
            ViewBag.ChartOpts7 = 2000;
            ViewBag.ChartOpts8 = 1500;
            ViewBag.ChartOpts9 = 1000;
            ViewBag.ChartOpts10 = 500;
            ViewBag.ChartOpts11 = 250;
            ViewBag.ChartOpts12 = 100;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public JsonResult SendMessage(string message)
        {
            UserSettings repos = new UserSettings();
            MonitoringViewModel viewModel = new MonitoringViewModel();

            if (Session["USER_SESSION"] != null)
            {
                viewModel.usersession = (POSSystem.Models.EmployeeDetails)Session["USER_SESSION"];

                var variantlist = repos.SendMessage(message, viewModel.usersession.UserName, "Admin");
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}