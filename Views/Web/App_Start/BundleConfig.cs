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
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate.min.js",
                        "~/Scripts/jquery/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                    "~/Scripts/bootstrap/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                    "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/utils").Include(
                     "~/Scripts/app/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include(
                      "~/Scripts/jquery/jquery.slimscroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sparkline").Include(
                     "~/Scripts/jquery/jquery.sparkline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                      "~/Scripts/gridmvc/gridmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mask").Include(
                    "~/Scripts/jquery/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                    "~/Scripts/inputmask/inputmask.js",
                    "~/Scripts/inputmask/jquery.inputmask.js",
                    "~/Scripts/inputmask/inputmask.extensions.js",
                    "~/Scripts/inputmask/inputmask.date.extensions.js",
                    "~/Scripts/inputmask/inputmask.numeric.extensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-inputmask").Include(
                         "~/Scripts/jquery/jquery.inputmask.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                    "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-notify").Include(
                    "~/Scripts/bootstrap/bootstrap-notify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts")
                .IncludeDirectory("~/Scripts/highcharts", "*.js", true));

            #endregion Scripts

            #region Style

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/bootstrap/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datepicker").Include(
                        "~/Content/bootstrap/datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                        "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/fonts.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/grid").Include(
                        "~/Content/gridmvc/Gridmvc.css"));

            #endregion Style

            BundleTable.EnableOptimizations = true;
        }
    }
}
