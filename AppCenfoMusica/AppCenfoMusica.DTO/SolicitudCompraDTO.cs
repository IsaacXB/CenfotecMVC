using AppCenfoMusica.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.DTO
{
    public class SolicitudCompraDTO : BaseDTO
    {
        [Required(ErrorMessage = "El Tipo de entrega es un campo requerido.")]
        [Display(Name = "Tipo de Entrega:")]
        public int TipoEntrega { get; set; }

        [Display(Name = "Estado:")]
        public int Estado { get; set; }

        [Display(Name = "Fecha Solicitud:")]
        public DateTime FechaSolicitud { get; set; }

        [ValidacionFechaEntrega]
        [Display(Name = "Fecha Entrega:")]
        public DateTime FechaEntrega { get; set; }
    }
}
