using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using SGITO_OBRAS.Web.ControlDeAccesos;
using SGITO_OBRAS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfilService = SGITO_OBRAS.Service.PerfilService;

namespace SGITO_OBRAS.Web.Controllers
{
    public class PerfilController : Controller
    {

        IUnitOfWork unitOfWork;
        PerfilService perf;

        public PerfilController() : this(new UnitOfWork()) { }

        public PerfilController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            perf = new PerfilService(uow);
        }
        
        public ActionResult Index()
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Listado";
            var Proceso = "PERFIL";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allPerfil = unitOfWork.PerfilRepository.All().ToList();
            var filteredPerfil = allPerfil;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredPerfil = allPerfil.Where(c => c.descripcion.ToUpper().Contains(param.sSearch.ToUpper())).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Perfil, string> orderingStringFunction = null;
            switch (sortColumnIndex)
            {
                case 0:
                    orderingStringFunction = (c => c.descripcion);
                    break;
                case 1:
                    orderingStringFunction = (c => c.descripcion);
                    break;
            }
            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
            {
                filteredPerfil = filteredPerfil.OrderBy(orderingStringFunction).ToList();
            }
            else
            {
                filteredPerfil = filteredPerfil.OrderByDescending(orderingStringFunction).ToList();
            }

            var displayedContratistaes = filteredPerfil.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedContratistaes
                         select new[] { c.perfilId.ToString(), c.descripcion };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allPerfil.Count(),
                iTotalDisplayRecords = filteredPerfil.Count(),
                aaData = result
            },
            JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Alta";
            var Proceso = "PERFIL";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            PerfilModel modelo = new PerfilModel();
            modelo = perf.GetCreate();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Create(PerfilModel modeloPerfil)
        {
            int result = perf.Create(modeloPerfil);
            if (result == 1)
            {
                TempData["Exito"] = "Perfil creado de forma correcta.";
                return RedirectToAction("Index", "Perfil");
            }
            else if (result == 2)
            {
                TempData["Error"] = "Ya existe un perfil con el mismo nombre.";
                return View(modeloPerfil);
            }
            else
            {
                TempData["Error"] = "Hubo un problema al guardar el perfil.";
                return View(modeloPerfil);
            }
        }
        
        
        public ActionResult Edit(int id)
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfiId = usrLog.perfilId;
            var funcionalidad = "Modificación";
            var Proceso = "PERFIL";
            var mensajePermiso = PermisosDeUsuarios(perfiId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            PerfilModel result = perf.GetEdit(id);
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Perfil");
            }
        }

        [HttpPost]
        public ActionResult Edit(PerfilModel modeloPerfil)
        {
            var result = perf.Edit(modeloPerfil);
            if (result == 1)
            {
                TempData["Exito"] = "Perfil modificado de forma correcta.";
                return RedirectToAction("Index", "Perfil");
            }
            else if (result == 2)
            {
                TempData["Error"] = "Ya existe un perfil con el mismo nombre.";
                return View(modeloPerfil);
            }
            else
            {
                TempData["Error"] = "Hubo un problema al guardar el perfil.";
                return View(modeloPerfil);
            }
        }
        public ActionResult Delete(int perfilId)
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfiId = usrLog.perfilId;
            var funcionalidad = "Baja";
            var Proceso = "PERFIL";
            var mensajePermiso = PermisosDeUsuarios(perfiId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            var result = perf.Delete(perfilId, usrLog.perfilId);
            if (result == 1)
            {
                TempData["Exito"] = "Perfil eliminado de forma correcta.";
                return RedirectToAction("Index", "Perfil");
            }
            else if (result == 2)
            {
                TempData["Error"] = "No es posible eliminar el perfil asignado a usted mismo.";
                return RedirectToAction("Index", "Perfil");
            }
            else
            {
                TempData["Error"] = "El perfil no se pudo eliminar porque se encuentra asignado a uno o más usuarios.";
                return RedirectToAction("Index", "Perfil");
            }
        }

        public string PermisosDeUsuarios(int perfilId, string funcionalidad, string proceso)
        {
            var retorno = "No Tiene Permisos";
            List<int> permisoId = unitOfWork.PerfilPermisoRepository.Filter(x => x.perfilId == perfilId).Select(x => x.permisoId).ToList();
            foreach (var num in permisoId)
            {
                var funcion = unitOfWork.PermisoRepository.Find(x => x.permisoId == num);
                if (funcion.funcionalidad == funcionalidad)
                {
                    var pro = unitOfWork.ProcesoRepository.Find(x => x.procesoId == funcion.procesoId);

                    if (pro.descripcion == proceso)
                    {
                        retorno = "ok";
                    }
                }

            }
            return retorno;
        }


    }
}