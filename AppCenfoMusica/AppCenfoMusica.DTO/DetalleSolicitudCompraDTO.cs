using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.DTO
{
    public class DetalleSolicitudCompraDTO :BaseDTO
    {
        [Required(ErrorMessage = "La cantidad de productos es requerida.")]
        [Display(Name = "Cantidad de Productos:")]
        public int cantidadProductos { get; set; }

        [Display(Name = "Monto del Producto:")]
        public decimal Monto { get; set; }

        [Display(Name = "Estado del Producto:")]
        public int Estado { get; set; }
    }
}
