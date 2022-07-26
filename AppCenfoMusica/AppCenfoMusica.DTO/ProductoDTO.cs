using AppCenfoMusica.DTO.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AppCenfoMusica.DTO
{
    public class ProductoDTO : BaseDTO
    {
        [Required (ErrorMessage = "El Nombre del Producto es un campo requerido.")]
        [Display (Name = "Nombre del Producto:") ]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [Display(Name = "Tipo Producto:")]
        [Range(1,3)]
        public int TipoProducto { get; set; }

        [Display(Name = "Precio Unitario:")]
        [RegularExpression(@"^(\d*\.)?\d+$")]
        public decimal PrecioUnitario { get;set; }

        [Display(Name = "Cantidad Bodega:")]
        [Range(0,10000)]
        public int CantidadBodega { get; set; }

        [ValidacionHora(ErrorMessage = "La Hora no es valida.")]
        public int HoraRegistro { get; set; }

        [ValidacionMinuto(ErrorMessage = "Los minutos no son validos.")]
        public int MinutoRegistro { get; set; }
    }
}
