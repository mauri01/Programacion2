using SGITO_OBRAS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service.Model
{
    public class PerfilModel
    {
        public int perfilModelId { get; set; }
        public Perfil perfil { get; set; }
        public List<ProcesoFuncionalidad> procesoFuncionalidadList { get; set; }
        public class ProcesoFuncionalidad
        {
            public Proceso proceso { get; set; }
            public List<FuncionalidadPermiso> funcionalidadPermisoList { get; set; }
        }

        public class FuncionalidadPermiso
        {
            public Permiso permiso { get; set; }
            public bool valor { get; set; }
        }
    }
}
