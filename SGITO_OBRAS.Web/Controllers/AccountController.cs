using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using SGITO_OBRAS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SGITO_OBRAS.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [DllImport("advapi32.dll")]
        public static extern bool LogonUser(string name, string domain, string pass, int logType, int logpv, ref IntPtr pht);

        public ActionResult Login(string ReturnUrl)
        {
            string url = Request.Form["ReturnUrl"];
            Login model = new Login();
            model.returnUrl = ReturnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Login model, string returnUrl)
        {
            
            string decodeUrl = string.Empty;
            if (!string.IsNullOrEmpty(model.returnUrl)) { decodeUrl = Server.UrlDecode(model.returnUrl); }
            else
            {
                string url2 = Request.Form["ReturnUrl"];
                if (!string.IsNullOrEmpty(url2)) { decodeUrl = Server.UrlDecode(url2); }
            }

            if (ModelState.IsValid)
            {
                string[] domUser = model.usuario.Split('\\');
                string pass = model.contraseña;
                IntPtr th = IntPtr.Zero;

                bool log = LogonUser(domUser[1], domUser[0], pass, 2, 0, ref th);
                log = true;
                if (log)
                {
                    string dominio = domUser[0].ToString(); //nombre de usuario
                    string nombre = domUser[1].ToString(); //nombre del dominio

                    FormsAuthentication.SetAuthCookie(model.usuario, true);
                    UnitOfWork uow = new UnitOfWork();
                    Usuario usuario = uow.UsuarioRepository.Filter(a => a.usuarioRed == nombre && a.dominio == dominio).FirstOrDefault();  //usuario sector del usuario logueado
                    if (usuario != null)
                    {
                        Session["DatosUsuario"] = (Usuario)Session["DatosUsuario"];
                        Session["DatosUsuario"] = usuario;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Error"] = "El usuario no existe.";
                        return View(model);
                    }
                }
                else
                {
                    ViewData["Error"] = "Usuario y/o contraseña incorrectos";
                    return View(model);
                }
                throw new NotImplementedException();
            }
            else
            {
                ViewData["Error"] = "Usuario y/o contraseña incorrectos";
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["datosUsuario"] = (Usuario)Session["DatosUsuario"];
            Session.Remove("datosUsuario");
            //redirijo al login
            return RedirectToAction("Login", "Account");
        }
    }
}