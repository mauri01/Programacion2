using System.Web;
using System.Web.Optimization;

namespace SGITO_OBRAS.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/dataTables.tableTools.js"
                        ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/Site.css",
                      "~/Content/jquery.dataTables.css",
                      "~/Content/bootstrap-dialog.css",
                      "~/Content/bootstrap-chosen.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.min.css",
                      "~/Content/customInputFile/css/custominputfile.min.css",
                      "~/Content/customInputFile/css/jquery.Jcrop.min.css",
                      "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                      "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css",
                     "~/Content/bootstrap-switch.min.css"));
            bundles.Add(new StyleBundle("~/Content/cssLogin").Include(
                       "~/Content/font-awesome.min.css",
                       "~/Content/fontgoogle.css",
                       "~/Content/style.css",
                       "~/Content/form-elements.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-dialog").Include(
                      "~/Scripts/bootstrap-dialog.js",
                      "~/Scripts/Validaciones/mensajesValidacion.js"));
            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
                      "~/Scripts/chosen.jquery.js"));
            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                      "~/Scripts/jQuery.FileUpload/jQuery.ui.widget.js",
                      //"~/Scripts/jQuery.FileUpload/tmpl.min.js",
                      "~/Scripts/jQuery.FileUpload/load-image.all.min.js",
                      "~/Scripts/jQuery.FileUpload/canvas-to-blob.min.js",
                      "~/Scripts/jQuery.FileUpload/jquery.blueimp-gallery.min.js",
                      "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                      "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                      "~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
                      "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                      "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/switch").Include(
                     "~/Scripts/bootstrap-switch.min.js"
               ));
        }
    }
}
