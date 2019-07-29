using System.Web;
using System.Web.Optimization;

namespace Exam_Result
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region Select Chosen

            bundles.Add(new ScriptBundle("~/bundles/SelectChosen").Include(
                "~/Scripts/lib/SelectChosen/js/chosen.jquery.min.js"
            ));

            bundles.Add(new StyleBundle("~/CustomCSS/SelectChosen").Include(
                "~/Scripts/lib/SelectChosen/css/chosen.css"
            ));

            #endregion

            #region Datatable Bundle

            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                "~/Scripts/lib/DataTables_1_10_13/media/js/jquery.dataTables.min.js",
                "~/Scripts/lib/DataTables_1_10_13/media/js/dataTables.bootstrap.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/FixedColumns/js/dataTables.fixedColumns.min.js",
                "~/Scripts/lib/DataTables_1_10_13/media/js/dataTables.rowsGroup.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/dataTables.buttons.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/buttons.bootstrap.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Export/jszip.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Export/pdfmake.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Export/vfs_fonts.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/buttons.html5.min.js",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/js/buttons.print.min.js"
            ));

            bundles.Add(new StyleBundle("~/styles/DataTable").Include(
                "~/Scripts/lib/DataTables_1_10_13/media/css/dataTables.bootstrap.min.css",
                "~/Scripts/lib/DataTables_1_10_13/extensions/FixedColumns/css/fixedColumns.bootstrap.min.css",
                "~/Scripts/lib/DataTables_1_10_13/extensions/Buttons/css/buttons.dataTables.min.css"
            ));

            #endregion
        }
    }
}
