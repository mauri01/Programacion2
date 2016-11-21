using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using SGITO_OBRAS.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace SGITO_OBRAS.Web.ControlDeAccesos
{
    public class ControDeAccesosAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        /// <summary>
        /// Proceso que el usuario quiere consultar.
        /// </summary>
        /// <value>Valor para la propiedad.</value>
        public string Proceso { get; set; }

        /// <summary>
        /// Funcionalidad del proceso que el usuario quiere consultar.
        /// </summary>
        /// <value>Valor para la propiedad.</value>
        public string Funcionalidad { get; set; }

        IUnitOfWork unitOfWork = new UnitOfWork();

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            
            // usuario de red que intenta acceder
            string[] usuarioRed = filterContext.HttpContext.User.Identity.Name.Split('\\');
            string usuario = usuarioRed[1];
            string dominio = usuarioRed[0];

            // proceso asociado al action
            string proceso = this.Proceso;

            // funcionalidad del proceso asociada al action
            string funcionalidad = this.Funcionalidad;

            Controller controller = filterContext.Controller as Controller;

            if (controller == null)
            {
                throw new AuthenticationException("Error en la Autorización. No se puede obtener el Controlador Base.");
            }


            var userTemp = GetUser(usuario, dominio);

            if (userTemp != null)
            {
                int perfilId = userTemp.perfilId;

                var perfil = unitOfWork.PerfilPermisoRepository.Filter(p => p.perfilId == perfilId && p.permiso.proceso.descripcion == this.Proceso && p.permiso.funcionalidad == this.Funcionalidad).ToList();

                int resultado = perfil.Count;

                if (resultado == 0)
                {
                    filterContext.Result = new System.Web.Mvc.ViewResult() { ViewName = "../Shared/NoAutorizado" };
                }

                if (resultado == -1)
                {
                    filterContext.Result = new System.Web.Mvc.ViewResult() { ViewName = "../Shared/Error" };
                }
            }
            else
            {
                filterContext.Result = new System.Web.Mvc.ViewResult() { ViewName = "../Shared/NoAutorizado" };
            }


        }

        public string GetConnectionString(string dbName)
        {
            var conect = ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
            return conect;
        }

        public UserSqlModel GetUser(string usuario, string dominio)
        {
            var userSql = new UserSqlModel();
            var usrLog = (Usuario)HttpContext.Current.Session["DatosUsuario"];

            var conect = GetConnectionString("BigData");
            using (SqlConnection connection = new SqlConnection(conect))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    string usuarioQuery = "";
                    string dominioQuery = "";
                    string sectorQuery = "";

                    usuarioQuery = " usuarioRed = @usuario ";
                    dominioQuery = " and dominio = @dominio ";
                    //sectorQuery = " and us.sectorId = " + usrLog.sectorDefecto.sectorId.ToString();

                    command.CommandText = "select top 1 usuarioId, perfilId, usuarioRed, dominio " +
                    "FROM Usuario " +
                    " where " + usuarioQuery + dominioQuery + sectorQuery;

                    SqlParameter usu = new SqlParameter("@usuario", SqlDbType.VarChar);
                    usu.Value = usuario;
                    command.Parameters.Add(usu);

                    SqlParameter domi = new SqlParameter("@dominio", SqlDbType.VarChar);
                    domi.Value = dominio;
                    command.Parameters.Add(domi);

                    SqlDataReader dato = command.ExecuteReader();

                    while (dato.Read())
                    {
                        userSql.usuarioId = dato.GetInt32(dato.GetOrdinal("usuarioId"));
                        userSql.perfilId = dato.GetInt32(dato.GetOrdinal("perfilId"));
                        userSql.usuarioRed = dato.GetString(dato.GetOrdinal("usuarioRed"));
                        userSql.dominio = dato.GetString(dato.GetOrdinal("dominio"));
                    }
                }
                connection.Close();
            }
            return userSql;
        }
    }
}