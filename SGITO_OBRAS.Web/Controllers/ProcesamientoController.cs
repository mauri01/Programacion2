using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Web.ControlDeAccesos;
using SGITO_OBRAS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProcesamientoService = SGITO_OBRAS.Service.ProcesamientoService;

namespace SGITO_OBRAS.Web.Controllers
{
    public class ProcesamientoController : Controller
    {
        IUnitOfWork unitOfWork;
        ProcesamientoService Proc;
        public ProcesamientoController() : this(new UnitOfWork()) { }
        //Accesos
        [ControDeAccesos(Proceso = "PROCESAMIENTO", Funcionalidad = "Listado")]
        public ActionResult Index()
        {
            return View();  
        }
        public ProcesamientoController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            Proc = new ProcesamientoService(uow);
            
        }
        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
     
            var allLogMailCount = Proc.VW_Mail();

            var filteredLogMail = Proc.GetProcesamientoGrid();

            var filteredLogMailCount = allLogMailCount;
            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
            //filteredObra = allObra.Where(c => c.datoBasico.ToUpper().Contains(param.sSearch.ToUpper()) || c.grafoEP.ToUpper().Contains(param.sSearch.ToUpper()) || c.sitioRBCentral.ToUpper().Contains(param.sSearch.ToUpper()) || c.tipoObra.descripcion.ToUpper().Contains(param.sSearch.ToUpper())).ToList();
            //}
            var displayedLogMail = filteredLogMail.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from c in displayedLogMail
                         select new[] { c.logMailId.ToString(), c.fuente, c.carpeta, c.fecha, c.status_procesamiento };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allLogMailCount,
                iTotalDisplayRecords = filteredLogMailCount,
                aaData = result
            },
            JsonRequestBehavior.AllowGet);

        }
    }
}