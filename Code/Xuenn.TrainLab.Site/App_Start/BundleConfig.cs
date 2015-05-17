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

            #endregion

            #region Style

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/bower_components/bootstrap/dist/css/bootstrap-theme.min.css",
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