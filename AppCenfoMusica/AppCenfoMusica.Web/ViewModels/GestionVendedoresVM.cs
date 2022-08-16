using AppCenfoMusica.DTO;

namespace AppCenfoMusica.Web.ViewModels
{
    public class GestionVendedoresVM
    {
        public VendedorDTO Vendedor { get; set; }

        public List<VendedorDTO> ListarVendedores { get; set; }

        public ErrorDTO Error { get; set; }
    }
}
