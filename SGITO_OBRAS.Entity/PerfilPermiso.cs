using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class PerfilPermiso :IEntity
    {
        public int perfilPermisoId { set; get; }
        public int perfilId { set; get; }
        public int permisoId { set; get; }
        public virtual Perfil perfil { set; get; }
        public virtual Permiso permiso { set; get; }
    }
}
