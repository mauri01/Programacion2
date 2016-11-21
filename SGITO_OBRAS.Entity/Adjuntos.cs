using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Adjuntos : IEntity
    {
        public int AdjuntosId { set; get; }

        public string tipo { set; get; }

        public string nombreAdjunto { set; get; }

        public string rutaAdjunto { set; get; }

        public int usuarioId { set; get; }

        public int? ventaId { set; get; }

        public virtual Venta venta { set; get; }
        
        public virtual Usuario usuario { set; get; }
    }
}
