using System.Web;
using System.Web.Optimization;

namespace ERP
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/_references.js",
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
            // jquery datataables js files
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/dataTables.bootstrap5.min.js",
                        "~/Scripts/dataTables.bootstrap5.js"));

            // jquery datatables css file
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/dataTables.bootstrap5.css"));

            //tree view
            bundles.Add(new StyleBundle("~/Content/Treeview").Include(
                     "~/Content/treeview.css"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
