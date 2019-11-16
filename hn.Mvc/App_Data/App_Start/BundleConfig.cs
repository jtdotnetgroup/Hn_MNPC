using System.Web;
using System.Web.Optimization;

namespace hn.Mvc
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/script")
                .Include("~/Scripts/jquery/jquery-1.10.2.min.js")
                .Include("~/Scripts/jquery/jquery.nicescroll.js")
                .Include("~/Scripts/layer/layer.min.js")
                .Include("~/Scripts/layout.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/Business/easyui").Include("~/Scripts/Business/BindComboBox.js").Include("~/Scripts/Business/BindComboTree.js"));


            //bundles.Add(new StyleBundle("~/Content/default").Include("~/Content/default/style.css"));

            //bundles.Add(new StyleBundle("~/Content/default/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}