using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service.Model
{
    public class Login
    {
        [Required]
        [DisplayName("Usuario")]
        [RegularExpression(@"^([a-zA-Z][a-zA-Z0-9.-]+)\\(?! +)([^\\/[\]:|<>+=;,?*@]+)$", ErrorMessage = "El usuario debe tener el siguiente formato: dominio\\usuario")]
        public string usuario { set; get; }
        [Required]
        [DisplayName("Contraseña")]
        public string contraseña { set; get; }
        public string returnUrl { set; get; }

    }
}