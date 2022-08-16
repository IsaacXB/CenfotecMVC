using AppCenfoMusica.DTO;

namespace AppCenfoMusica.Web.ViewModels
{
    public class GestionClientesVM
    {
        public ClienteDTO Cliente { get; set; }

        public List<ClienteDTO> ListaClientes { get; set; }

        public ErrorDTO Error { get; set; }
    }
}
