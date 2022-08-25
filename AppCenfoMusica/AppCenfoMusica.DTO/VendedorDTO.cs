using AppCenfoMusica.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.DTO
{
    public class VendedorDTO : BaseDTO
    {
        [Required(ErrorMessage = "La cédula del Vendedor es un campo requerido.")]
        [Display(Name = "Cédula del Vendedor:")]
        [MaxLength(10)]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "El Nombre del Vendedor es un campo requerido.")]
        [Display(Name = "Nombre del Vendedor:")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Display(Name = "Puesto del Vendedor:")]
        [Range(1,5)]
        public int Puesto { get; set; }

        [Required(ErrorMessage = "El nombre de usuario del Vendedor es un campo requerido.")]
        [RegularExpression(@"^([^0-9]*)$")]
        [Display(Name = "Nombre de Usuario:")]
        [MinLength(5)]
        [MaxLength(10)]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña del Vendedor es un campo requerido.")]
        [Display(Name = "Contraseña:")]
        [MinLength(5)]
        [MaxLength(8)]
        //[ValidacionContraseña]
        public string Contrasena { get; set; }

        [Display(Name = "Estado del Vendedor:")]
        public int Estado { get; set; }

    }
}
