using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Mensajes : IEntity
    {
        public int MensajesId { set; get; }

        public string mensaje { set; get; }

        public int Usuario { set; get; }

        public int ventaId { set; get; }
        public virtual Venta venta { set; get; }
    }
}
