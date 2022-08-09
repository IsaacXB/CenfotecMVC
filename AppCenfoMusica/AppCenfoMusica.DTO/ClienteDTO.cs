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
        [Required(ErrorMessage = "La cédula del Cliente es un campo requerido.")]
        [Display(Name = "Cédula del Cliente:")]
        [MaxLength(10)]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "El Nombre del Cliente es un campo requerido.")]
        [Display(Name = "Nombre del Cliente:")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Display(Name = "Sexo:")]
        [Range(1,3)]
        public int Sexo { get; set; }

        [Display(Name = "Fecha de Nacimiento:")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El nombre de usuario del Vendedor es un campo requerido.")]
        [Display(Name = "Nombre de Usuario:")]
        [MinLength(5)]
        [MaxLength(10)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña de usuario del Vendedor es un campo requerido.")]
        [Display(Name = "Contraseña:")]
        [MinLength(8)]
        [MaxLength(8)]
        [ValidacionContraseña]
        public string? Contrasena { get; set; }


        [ValidacionEmailCenfotec(ErrorMessage = "El email no es valido debe tener el formato @ucenfotec.ac.cr.")]
        [RegularExpression(@"\w+@ucenfotec.ac.cr")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Teléfono:")]
        [Phone]
        public string? Telefono { get; set; }

        [Display(Name = "Estado del Cliente:")]
        public int Estado { get; set; }
    }
}
