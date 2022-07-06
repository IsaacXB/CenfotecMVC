using AppCenfoMusica.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.DTO
{
    public class ClienteDTO : BaseDTO
    {
        [Required(ErrorMessage = "El Nombre del Cliente es un campo requerido.")]
        [Display(Name = "Nombre del Cliente:")]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [Display(Name = "Fecha de Nacimiento:")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Nombre de Usuario:")]
        [MinLength(5)]
        [MaxLength(50)]
        public int NombreUsuario { get; set; }

        [Display(Name = "Contraseña:")]
        [MinLength(8)]
        [MaxLength(15)]
        public int Contrasena { get; set; }


        [ValidacionEmailCenfotec(ErrorMessage = "El email no es valido debe tener el formato @ucenfotec.ac.cr.")]
        [RegularExpression(@"\w+@ucenfotec.ac.cr")]
        public int MinutoRegistro { get; set; }

        [Display(Name = "Teléfono:")]
        [Phone]
        public string? Telefono { get; set; }
    }
}
