using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Util;
using SGITO_OBRAS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsuarioService = SGITO_OBRAS.Service.UsuarioService;
using SGITO_OBRAS.Service.Model;
using SGITO_OBRAS.Web.ControlDeAccesos;

namespace SGITO_OBRAS.Web.Controllers
{
    public class UsuarioController : Controller
    {
        IUnitOfWork unitOfWork;
        UsuarioService Usua;
        Desplegables desplegable;

        public UsuarioController() : this(new UnitOfWork()) { }
        
        public ActionResult Index()
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Listado";
            var Proceso = "USUARIOS";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            return View();
        }

        public UsuarioController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            Usua = new UsuarioService(uow);
            desplegable = new Desplegables(uow);
        }
        
        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allUsuario = unitOfWork.UsuarioRepository.All().ToList();
            var filteredUsuario = allUsuario;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsuario = allUsuario.Where(c => c.usuarioRed.ToUpper().Contains(param.sSearch.ToUpper()) || c.dominio.ToUpper().Contains(param.sSearch.ToUpper()) || c.nombreApellido.ToUpper().Contains(param.sSearch.ToUpper()) || c.email.ToUpper().Contains(param.sSearch.ToUpper())).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Usuario, string> orderingStringFunction = null;

            switch (sortColumnIndex)
            {
                case 1:
                    orderingStringFunction = (c => c.usuarioRed);
                    break;
                case 2:
                    orderingStringFunction = (c => c.dominio);
                    break;
                case 3:
                    orderingStringFunction = (c => c.nombreApellido);
                    break;
                case 4:
                    orderingStringFunction = (c => c.email);
                    break;
            }
            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
            {
                filteredUsuario = filteredUsuario.OrderBy(orderingStringFunction).ToList();
            }
            else
            {
                filteredUsuario = filteredUsuario.OrderByDescending(orderingStringFunction).ToList();
            }

            var displayedUsuarios = filteredUsuario.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

            var result = from c in displayedUsuarios
                         select new[] { c.usuarioId.ToString(), c.usuarioRed, c.dominio.ToUpper(), c.nombreApellido, c.email };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allUsuario.Count(),
                iTotalDisplayRecords = filteredUsuario.Count(),
                aaData = result
            },
            JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUsuario(int id)
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Baja";
            var Proceso = "USUARIOS";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            var result = Usua.DeleteUsuario(id);
            if (result == 1)
            {
                TempData["Exito"] = "El Usuario se eliminó de forma correcta.";
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                TempData["Error"] = "El Usuario no se pudo eliminar.";
                return RedirectToAction("Index", "Usuario");
            }
        }
        public ActionResult Edit(int id)
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Modificación";
            var Proceso = "USUARIOS";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            Usuario usuario = unitOfWork.UsuarioRepository.Find(x => x.usuarioId == id);
            ViewData["perfilId"] = desplegable.desplegablePerfiles(usuario.perfilId);
            ViewData["tipoDocumento"] = desplegable.desplegableDNI(usuario.tipoDocumento);
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                string result = Usua.UpdateUsuario(usuario);
                if (result == "ok")
                {
                    TempData["Exito"] = "Usuario modificado de forma correcta.";
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewData["error"] = result;
                    ViewData["perfilId"] = desplegable.desplegablePerfiles(usuario.perfilId);
                    ViewData["tipoDocumento"] = desplegable.desplegableDNI("DNI");
                    return View(usuario);
                }
            }
            else
            {
                ViewData["perfilId"] = desplegable.desplegablePerfiles(usuario.perfilId);
                ViewData["tipoDocumento"] = desplegable.desplegableDNI(usuario.tipoDocumento);
                return View(usuario);
            }
        }
        public ActionResult Create()
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Alta";
            var Proceso = "USUARIOS";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            Usuario usuario = new Usuario();
            ViewData["perfilId"] = desplegable.desplegablePerfiles();
            ViewData["tipoDocumento"] = desplegable.desplegableDNI();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                string result = Usua.InsertUsuario(usuario);
                if (result == "ok")
                {
                    TempData["Exito"] = "Usuario creado.";
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewData["error"] = result;
                    ViewData["perfilId"] = desplegable.desplegablePerfiles(usuario.perfilId);
                    ViewData["tipoDocumento"] = desplegable.desplegableDNI(usuario.tipoDocumento);
                    return View(usuario);
                }
            }
            else
            {
                ViewData["perfilId"] = desplegable.desplegablePerfiles(usuario.perfilId);
                ViewData["tipoDocumento"] = desplegable.desplegableDNI(usuario.tipoDocumento);
                return View(usuario);
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