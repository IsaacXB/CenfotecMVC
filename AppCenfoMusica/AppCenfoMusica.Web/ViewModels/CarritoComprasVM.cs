using AppCenfoMusica.DTO;

namespace AppCenfoMusica.Web.ViewModels
{
    public class CarritoComprasVM
    {
        public List<ProductoDTO> ProductosCarrito { get; set; }

        public List<int> CantidadesXProducto { get; set; }

        public ProductoDTO VistaDeProducto { get; set; }
    }
}
