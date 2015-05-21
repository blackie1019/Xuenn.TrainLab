using System.IO;
using System.Threading;
using System.Web.Optimization;

namespace Xuenn.TrainLab.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.Bundles.Clear();
            bundles.Clear();

            #region Script

            bundles.Add(new ScriptBundle("~/bundles/bower_compnents").Include(
                "~/Content/bower_components/jquery/dist/jquery.min.js",
                "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/bower_components/react/react.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme/js").Include(
                "~/Content/theme/AdminLTE-master/plugins/fastclick/fastclick.min.js",
                "~/Content/theme/AdminLTE-master/plugins/sparkline/jquery.sparkline.min.js",
                "~/Content/theme/AdminLTE-master/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                "~/Content/theme/AdminLTE-master/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                "~/Content/theme/AdminLTE-master/plugins/daterangepicker/daterangepicker.js",
                "~/Content/theme/AdminLTE-master/plugins/datepicker/bootstrap-datepicker.js",
                "~/Content/theme/AdminLTE-master/plugins/iCheck/icheck.min.js",
                "~/Content/theme/AdminLTE-master/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/Content/theme/AdminLTE-master/plugins/chartjs/Chart.min.js",
                "~/Content/bower_components/react/react.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/js").Include(
                "~/Scripts/app.js"));

            #endregion

            #region Style

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/bower_components/bootstrap/dist/css/bootstrap-theme.min.css",
                "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                "~/Content/bower_components/ionicons/css/ionicons.min.css",
                "~/Content/bower_components/plugins/slimScroll/jquery.slimScroll.min.js",
                "~/Content/theme/AdminLTE-master/plugins/morris/morris.min.css",
                "~/Content/theme/AdminLTE-master/plugins/jvectormap/jquery-jvectormap-1.2.2.min.css",
                "~/Content/theme/AdminLTE-master/plugins/daterangepicker/daterangepicker-bs3.css",
                "~/Content/theme/AdminLTE-master/dist/css/AdminLTE.min.css",
                "~/Content/theme/AdminLTE-master/dist/css/skins/_all-skins.min.css"));

            bundles.Add(new StyleBundle("~/bundles/app/css").Include(
                "~/Content/css/app.css"));

            #endregion

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }

    }
}