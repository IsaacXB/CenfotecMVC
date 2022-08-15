using AppCenfoMusica.DTO;

namespace AppCenfoMusica.Web.ViewModels
{
    public class GestionProductosVM
    {
        public ProductoDTO Producto { get; set; }

        public List<ProductoDTO> ListaProductos { get; set; }

        public ErrorDTO Error { get; set; }
    }
}
