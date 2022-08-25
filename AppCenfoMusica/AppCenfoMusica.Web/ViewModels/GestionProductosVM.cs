using AppCenfoMusica.DTO;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppCenfoMusica.Web.ViewModels
{
    public class GestionProductosVM
    {
        public ProductoDTO Producto { get; set; }

        public List<ProductoDTO> ListaProductos { get; set; }
        public ErrorDTO Error { get; set; }

        [Display(Name = "Digite la cantidad de productos a comprar:")]
        public int CantidadXProducto { get; set; }
    }
}
