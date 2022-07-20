using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppCenfoMusica.Logica
{
    public class ProductoLogica
    {
        #region Variables
        CenfomusicaContext contexto;
        #endregion

        #region Constructor 
        public ProductoLogica()
        {
            this.contexto = new CenfomusicaContext();
        }
        #endregion

        #region Metodos 
        
        #region Traductores

        internal static ProductoDTO ConvertirEntidadProductoADTO(Producto producto)
        {
            return new ProductoDTO
            {
                CantidadBodega = Convert.ToInt32(producto.CantProducto),
                IdEntidad = producto.Pkproducto,
                Nombre = producto.NomProducto,
                PrecioUnitaro = Convert.ToDecimal(producto.MtoPrecio),
                TipoProducto = Convert.ToInt32(producto.TipProducto)
            };
        }

        internal static Producto ConvertirProductoDTOAEntidad(Producto producto)
        {
            return new Producto
            {
                CantProducto = producto.CantProducto,
                MtoPrecio = producto.MtoPrecio,
                NomProducto = producto.NomProducto,
                TipProducto = producto.TipProducto
            };
        }
        #endregion  

        #endregion
    }
}
