using System;
using System.Collections.Generic;

namespace AppCenfoMusica.Datos.CenfomusicaModel
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            SolicitudCompras = new HashSet<SolicitudCompra>();
        }

        public int Pkvendedor { get; set; }
        public string? IndCedula { get; set; }
        public string? NomVendedor { get; set; }
        public int? IndPuesto { get; set; }
        public string? NomUsuario { get; set; }
        public string? IndContrasena { get; set; }
        public int? IndEstado { get; set; }

        public virtual ICollection<SolicitudCompra> SolicitudCompras { get; set; }
    }
}
