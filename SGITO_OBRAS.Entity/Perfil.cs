using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Perfil : IEntity
    {
        public int perfilId { get; set; }
        [Required(ErrorMessage = "No se puede dejar vacio el campo Perfil."), Display(Name = "PERFIL")]
        [StringLength(50, ErrorMessage = "Perfil debe tener menos de 50 caracteres.")]
        public string descripcion { set; get; }
    }
}
