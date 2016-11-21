using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service;
using SGITO_OBRAS.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SGITO_OBRAS.Web.Controllers
{
    public class HomeController : Controller
    {

        IUnitOfWork unitOfWork;

        VentaService ventaService;
        public HomeController() : this(new UnitOfWork()) { }
        public HomeController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            ventaService = new VentaService(unitOfWork);
        }
        public ActionResult Index()
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            if (usrLog != null)
            {
                var perfilId = usrLog.perfilId;
                var perfil = unitOfWork.PerfilRepository.Filter(x => x.perfilId == perfilId).Select(x => x.descripcion).FirstOrDefault();
                var usuId = usrLog.usuarioId;
                List<ModelVentaHome> VentaHome = new List<ModelVentaHome>();
                VentaHome = ventaService.GetModeloVentaHome(usuId);
                TempData["ventaList"] = VentaHome;
                TempData["perfil"] = perfil;

            }
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}