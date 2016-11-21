using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGITO_OBRAS.Web.Models
{
    public class UserSqlModel
    {
        public int usuarioId { set; get; }
        public string usuarioRed { set; get; }
        public string dominio { set; get; }
        public int perfilId { set; get; }
    }
}