using System;
using System.Collections.Generic;

namespace AppCenfoMusica.Datos.CenfomusicaModel
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleSolicitudCompras = new HashSet<DetalleSolicitudCompra>();
        }

        public int Pkproducto { get; set; }
        public string? NomProducto { get; set; }
        public int? TipProducto { get; set; }
        public int? CantProducto { get; set; }
        public decimal? MtoPrecio { get; set; }

        public virtual ICollection<DetalleSolicitudCompra> DetalleSolicitudCompras { get; set; }
    }
}
