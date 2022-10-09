using System.Web;
using System.Web.Optimization;

namespace POSSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
             
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/js/adminlte.min.js",
                      "~/Scripts/select2/dist/js/select2.full.min.js",
                      "~/Scripts/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                      "~/Scripts/moment/min/moment.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Styles/css").Include(
                      "~/Content/bootstrap/dist/css/bootstrap.css",
                      "~/Content/select2/dist/css/select2.min.css",
                      "~/Content/css/AdminLTE.min.css",
                      "~/Content/css/skins/_all-skins.min.css",
                      "~/Content/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      "~/Content/font-awesome/css/font-awesome.min.css"
                      ));
        }
    }
}
