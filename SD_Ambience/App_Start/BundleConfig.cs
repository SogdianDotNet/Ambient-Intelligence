using System.Web;
using System.Web.Optimization;

namespace SD_Ambience
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                      "~/Assets/css/GlobalStyle.css"
                ));

            bundles.Add(new ScriptBundle("~/Assets/js/Pages").Include(
                      "~/Assets/js/Pages/Dashboard/Global.js",
                      "~/Assets/js/Pages/User/Global.js",
                      "~/Assets/js/Pages/Teacher/Global.js",
                      "~/Assets/js/Pages/Administrator/Global.js",
                      "~/Assets/js/Pages/Logs/Global.js",
                      "~/Assets/js/Pages/Session/Global.js",
                      "~/Assets/js/Ajax/JSONPass.js"
                ));

            bundles.Add(new ScriptBundle("~/Assets/js/Validation").Include(
                      "~/Assets/js/Validation/ErrorMessages.js"
                ));
        }
    }
}
