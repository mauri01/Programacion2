using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Proceso :IEntity
    {
        public int procesoId { set; get; }
        [Required(ErrorMessage = "No se puede dejar vacio el campo Proceso."), Display(Name = "PROCESO")]
        [StringLength(100, ErrorMessage = "Proceso debe tener menos de 100 caracteres.")]
        public string descripcion { set; get; }
        public string nombre { set; get; }
    }
}
