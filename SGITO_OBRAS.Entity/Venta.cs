using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Venta : IEntity
    {
        public int ventaId { set; get; }
        [Required(ErrorMessage = "No se puede dejar vacio el campo Nombre Venta"), Display(Name = "nombreVenta")]
        [StringLength(50, ErrorMessage = "Nombre Venta debe tener menos de 50 caracteres.")]
        public string nombreVenta { set; get; }
        [Required(ErrorMessage = "Es necesario una breve descripcion."), Display(Name = "descripcion")]
        public string descripcion { set; get; }
        public int precio { set; get; }
        public string tipo { set; get; }
        public int cantDormitorios { set; get; }
        public int cantBaños { set; get; }
        public int metrosCuadrados { set; get; }
        public int mensajes { set; get; }
        public string provincia { set; get; }
        public int usuarioId { set; get; }
        public virtual Usuario usuario { set; get; }

    }
}
