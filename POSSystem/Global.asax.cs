using POSSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace POSSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            var url = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString();

            if (Session["USER_SESSION"] == null)
            {
                UserSettings userSettings = new UserSettings();
                Session["USER_SESSION"] = null;

                if (Session["USER_SESSION"] == null)
                {
                    Server.ClearError();
                    Response.Clear();
                    Response.Redirect("~/Account/Login");
                }

                //string userId = Request.LogonUserIdentity.Name.ToString(); 

                //if (userId != null)
                //{
                //    Session["USER_SESSION"] = userId;
                //}
                //else
                //{
                //    Session["USER_SESSION"] = null;
                //}
            }
        }

        protected void Session_End()
        {
            Session["USER_SESSION"] = null;
        }
        protected void Logout()
        { 
            Session["USER_SESSION"] = null;
        }
        protected void Application_Error()
        {
            /*Exception err = Server.GetLastError().GetBaseException();
            string url = Request.Url.ToString();
            string message = err.Message;

            Session["ErrorURl"] = url;
            Session["ErrorMessage"] = message;*/

            //Please log this to database ..
        }
    }
}
