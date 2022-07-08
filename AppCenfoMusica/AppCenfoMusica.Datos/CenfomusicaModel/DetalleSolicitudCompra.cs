using System;
using System.Collections.Generic;

namespace AppCenfoMusica.Datos.CenfomusicaModel
{
    public partial class DetalleSolicitudCompra
    {
        public int PkdetalleSolicitudCompra { get; set; }
        public int? Fkproducto { get; set; }
        public int? CantProductos { get; set; }
        public decimal? MtoLinea { get; set; }
        public int? FksolicitudCompra { get; set; }
        public int? IndEstado { get; set; }

        public virtual Producto? FkproductoNavigation { get; set; }
        public virtual SolicitudCompra? FksolicitudCompraNavigation { get; set; }
    }
}
