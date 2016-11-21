using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Permiso :IEntity
    {
        public int permisoId { set; get; }
        public int procesoId { set; get; }
        [Display(Name = "FUNCIONALIDAD")]
        [StringLength(20, ErrorMessage = "Funcionalidad debe tener menos de 20 caracteres.")]
        public string funcionalidad { set; get; }
        public virtual Proceso proceso { set; get; }
    }
}
