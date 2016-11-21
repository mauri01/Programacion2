using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Util;
using SGITO_OBRAS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentaService = SGITO_OBRAS.Service.VentaService;
using SGITO_OBRAS.Service.Model;
using System.Net;
using SGITO_OBRAS.Web.ControlDeAccesos;

namespace SGITO_OBRAS.Web.Controllers
{
    public class GestionController : Controller
    {
        IUnitOfWork unitOfWork;

        VentaService ventaService;
        DesplegablesUtil dDown;
        public GestionController() : this(new UnitOfWork()) { }
        public GestionController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            ventaService = new VentaService(unitOfWork);
            dDown = new DesplegablesUtil(unitOfWork);
        }
        public ActionResult Index()
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfilId = usrLog.perfilId;
            var funcionalidad = "Listado";
            var Proceso = "PUBLICAR";
            var mensajePermiso = PermisosDeUsuarios(perfilId, funcionalidad, Proceso);
            if (mensajePermiso != "ok")
            {
                return PartialView("NoAutorizado", "Shared");
            }
            LimpiarAdjObs();
            VentaModel modelo = new VentaModel();
            ViewData["desplegableAdjuntos"] = dDown.desplegableAdjuntos();
            ViewData["desplegableTipo"] = dDown.desplegableTipo();
            ViewData["provincias"] = dDown.provincias();
            return View();
        }

        public ActionResult DetailVenta(int ventaId)
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var perfil = usrLog.perfil.descripcion;
            VentaModel ventaModel = new VentaModel();
            var venta = unitOfWork.VentaRepository.Find(x => x.ventaId == ventaId);
            string lista = "";
            var c = 0;
            List<string> adjuntosId = unitOfWork.AdjuntosRepository.Filter(x => x.ventaId == ventaId).Select(x => x.rutaAdjunto).ToList();
            foreach (var num in adjuntosId)
            {
                if (c > 0)
                {
                    lista = lista + ";" + "." + num.ToString();
                }
                else
                {
                    lista = "." + num.ToString();
                }
                c++;
            }

            ventaModel.strAdjId = lista;
            ventaModel.venta = venta;
            TempData["venta"] = ventaModel;
            TempData["Perfil"] = perfil;
            ViewData["desplegableTipo"] = dDown.desplegableTipo(ventaModel.venta.tipo);
            ViewData["provincias"] = dDown.provincias(ventaModel.venta.provincia);
            return View();
        }

        public ActionResult DeleteVenta(int ventaId)
        {
            VentaModel ventaModel = new VentaModel();
            var venta = unitOfWork.VentaRepository.Find(x => x.ventaId == ventaId);
            var result = ventaService.DeleteVenta(ventaId);
            ventaModel.venta = venta;

            if (result == "ok")
            {
                TempData["Exito"] = "Venta Eliminada con Exito.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = result;
                ViewData["desplegableTipo"] = dDown.desplegableTipo(ventaModel.venta.tipo);
                ViewData["provincias"] = dDown.provincias(ventaModel.venta.provincia);
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Create()
        {
            var modelo = new VentaModel();
            return View(modelo);
        }

        public ActionResult Mensajes()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(VentaModel ventas)
        {
            var usrLog = (Usuario)Session["DatosUsuario"];
            var usuId = usrLog.usuarioId;
            if (ModelState.IsValid)
            {
                var result = ventaService.InsertVenta(ventas, usuId);
                if (result == "ok")
                {
                    List<ModelVentaHome> VentaHome = new List<ModelVentaHome>();
                    TempData["Exito"] = "Venta creada con exito.";
                    TempData["idUsu"] = usuId.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = result;
                    ViewData["desplegableTipo"] = dDown.desplegableTipo(ventas.venta.tipo);
                    ViewData["provincias"] = dDown.provincias(ventas.venta.provincia);
                    ViewData["desplegableAdjuntos"] = dDown.desplegableAdjuntos();
                    return View("Index", ventas);
                    //return View(ventas);
                }
            }
            else
            {
                ViewData["desplegableTipo"] = dDown.desplegableTipo(ventas.venta.tipo);
                ViewData["provincias"] = dDown.provincias(ventas.venta.provincia);
                ViewData["desplegableAdjuntos"] = dDown.desplegableAdjuntos();
                return View("Index", ventas);
            }
        }


        [HttpPost]
        public ContentResult Subir(int procesoId)
        {
            Adjuntos adj = new Adjuntos();
            var result = "{\"adjuntoId\":\"sin adjunto\"}";
            var usrLog = (Usuario)Session["DatosUsuario"];
            var usrId = usrLog.usuarioId;

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase adjunto = Request.Files[file] as HttpPostedFileBase;
                if (adjunto.ContentLength == 0)
                {
                    continue;
                }
                else
                {
                    adj.nombreAdjunto = adjunto.FileName;
                    adj.tipo = adjunto.ContentType;
                    adj.usuarioId = usrId;
                    //string archivo = (adjunto.FileName).ToLower();
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + adjunto.FileName).ToLower();
                    adjunto.SaveAs(Server.MapPath("~/Uploads/" + archivo));
                    adj.rutaAdjunto = "./Uploads/" + archivo;
                    unitOfWork.AdjuntosRepository.Create(adj);
                    unitOfWork.AdjuntosRepository.Save();


                    result = "{\"adjuntoId\":\"" + adj.AdjuntosId + "\",\"nombreArchivo\":\"" + adj.nombreAdjunto + "\"}";

                }
            }
            return Content(result, "application /json");

        }

        public ActionResult Mensaje(string msjes, int valor)
        {

            var idVenta = valor;
            var msj = msjes;

            var usrLog = (Usuario)Session["DatosUsuario"];
            var usuId = usrLog.usuarioId;
            var result = ventaService.InsertMensaje(idVenta, msj, usuId);

            if (result == "ok")
            {
                TempData["Exito"] = "Su mensaje fue registrado.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = result;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult VerMensaje(int ventaId)
        {
            string lista = "";
            int c = 0;
            List<string> Mensajes = unitOfWork.MensajesRepository.Filter(x => x.ventaId == ventaId).Select(x => x.mensaje).ToList();
            foreach (var num in Mensajes)
            {
                var msj = unitOfWork.MensajesRepository.Find(x => x.mensaje == num);
                var usuario = msj.Usuario;
                var nombreUsuario = unitOfWork.UsuarioRepository.Find(x => x.usuarioId == usuario);
                var mailUsu = nombreUsuario.email;
                var NombreCompleto = nombreUsuario.nombreApellido;
                if (c > 0)
                {
                    lista = lista + ";" + "\n" + "Mensaje de:" + NombreCompleto + "\n" + "Email:" + mailUsu + "\n" + "Mensaje:" + num;
                }
                else
                {
                    lista = "\n" + "Mensaje de:" + NombreCompleto + "\n" + "Email:" + mailUsu + "\n" + "Mensaje:" + num + "\n";
                }
                c++;
            }
            TempData["venta"] = lista;
            return View();



        }


        public string DeleteAdjuntos(int id, string proceso)
        {
            var result = "";

            unitOfWork.AdjuntosRepository.Delete(x => x.AdjuntosId == id);
            unitOfWork.AdjuntosRepository.Save();

            result = "ok";

            return result;

        }

        public void LimpiarAdjObs()
        {
            int[] aii = unitOfWork.AdjuntosRepository.Filter(x => x.ventaId == null).Select(x => x.AdjuntosId).ToArray();
            if (aii.Length != 0)
            {
                unitOfWork.AdjuntosRepository.Delete(x => x.ventaId == null);
                unitOfWork.AdjuntosRepository.Save();
            }

        }

        public string PermisosDeUsuarios(int perfilId,string funcionalidad, string proceso)
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