using System;
using System.Collections.Generic;

namespace AppCenfoMusica.Datos.CenfomusicaModel
{
    public partial class Cliente
    {
        public Cliente()
        {
            SolicitudCompras = new HashSet<SolicitudCompra>();
        }

        public int Pkcliente { get; set; }
        public string? IdCedula { get; set; }
        public string? NomCliente { get; set; }
        public int? IndSexo { get; set; }
        public DateTime? FecNacimiento { get; set; }
        public string? NomUsuario { get; set; }
        public string? IndContrasena { get; set; }
        public string? EmlCorreo { get; set; }
        public string? TelCliente { get; set; }
        public int? IndEstado { get; set; }

        public virtual ICollection<SolicitudCompra> SolicitudCompras { get; set; }
    }
}
