using SGITO_OBRAS.Entity.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class Usuario :IEntity
    {
        public int usuarioId { set; get; }
        [Required(ErrorMessage = "No se puede dejar vacio el campo Usuario Red."), Display(Name = "USUARIO RED")]
        [StringLength(50, ErrorMessage = "Usuario Red debe tener menos de 50 caracteres.")]
        public string usuarioRed { set; get; }
        [Required(ErrorMessage = "No se puede dejar vacio el campo Dominio."), Display(Name = "DOMINIO")]
        [StringLength(50, ErrorMessage = "Dominio debe tener menos de 50 caracteres.")]
        public string dominio { set; get; }
        [Required(ErrorMessage = "No se puede dejar vacio el campo Nombre y apellido."), Display(Name = "NOMBRE Y APELLIDO")]
        [StringLength(100, ErrorMessage = "Nombre y apellido debe tener menos de 100 caracteres.")]
        public string nombreApellido { set; get; }
        public int perfilId { set; get; }
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Formato de email invalido"), Display(Name = "E-MAIL")]
        public string email { set; get; }
        [Display(Name = "TIPO DOCUMENTO")]
        [StringLength(10, ErrorMessage = "Tipo Documento debe tener menos de 10 caracteres.")]
        public string tipoDocumento { set; get; }
        [Display(Name = "NRO. DOCUMENTO")]
        [StringLength(20, ErrorMessage = "Tipo Documento debe tener menos de 20 caracteres.")]
        public string nroDocumento { set; get; }
        public string pass { set; get; }
        public virtual Perfil perfil { set; get; }
    }
}
