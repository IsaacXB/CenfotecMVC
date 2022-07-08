using System;
using System.Collections.Generic;

namespace AppCenfoMusica.Datos.CenfomusicaModel
{
    public partial class SolicitudCompra
    {
        public SolicitudCompra()
        {
            DetalleSolicitudCompras = new HashSet<DetalleSolicitudCompra>();
        }

        public int PksolicitudCompra { get; set; }
        public int? Fkcliente { get; set; }
        public int? Fkvendedor { get; set; }
        public int? IndTipoEntrega { get; set; }
        public int? IndEstado { get; set; }
        public DateTime? FecSolicitud { get; set; }
        public DateTime? FecEntrega { get; set; }

        public virtual Cliente? FkclienteNavigation { get; set; }
        public virtual Vendedor? FkvendedorNavigation { get; set; }
        public virtual ICollection<DetalleSolicitudCompra> DetalleSolicitudCompras { get; set; }
    }
}
