using System.Web;
using System.Web.Optimization;

namespace KarmicEnergy.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/utils").Include(
                     "~/Scripts/app/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include(
                      "~/Scripts/jquery.slimscroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sparkline").Include(
                     "~/Scripts/jquery.sparkline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                      "~/Scripts/gridmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mask").Include(
                    "~/Scripts/jquery/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                    "~/Scripts/jquery.inputmask/inputmask.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                    "~/Scripts/jquery.inputmask/inputmask.extensions.js",
                    "~/Scripts/jquery.inputmask/inputmask.date.extensions.js",                    
                    "~/Scripts/jquery.inputmask/inputmask.numeric.extensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                    "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/notify").Include(
              "~/Scripts/bootstrap-notify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts")
                .IncludeDirectory("~/Scripts/highcharts", "*.js", true));

            #endregion Scripts

            #region Style

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                     "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                     "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/fonts.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/grid").Include(
                  "~/Content/Gridmvc.css"));

            #endregion Style

            //BundleTable.EnableOptimizations = true;
        }
    }
}
