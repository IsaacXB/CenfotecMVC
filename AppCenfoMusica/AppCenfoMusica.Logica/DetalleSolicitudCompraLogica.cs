using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;
using AppCenfoMusica.Datos;

namespace AppCenfoMusica.Logica
{
    public class DetalleSolicitudCompraLogica
    {
        #region Variables

        CenfomusicaContext contexto;

        #endregion

        #region Constructor

        public DetalleSolicitudCompraLogica()
        {
            contexto = new CenfomusicaContext();
        }

        #endregion

        #region Metodos

        #region

        internal static DetalleSolicitudCompraDTO ConvertirEntidadDetalleSolicitudCompraDTO(DetalleSolicitudCompra detalleSolicitudCompra)
        {
            return new DetalleSolicitudCompraDTO
            {
                cantidadProductos = Convert.ToInt32(detalleSolicitudCompra.CantProductos),
                Estado = Convert.ToInt32(detalleSolicitudCompra.IndEstado),
                IdEntidad = Convert.ToInt32(detalleSolicitudCompra.PkdetalleSolicitudCompra),
                Monto = Convert.ToDecimal(detalleSolicitudCompra.MtoLinea)
            };
        }

        internal static DetalleSolicitudCompra ConvertirDTODetalleSolicitudCompraAEntidad(DetalleSolicitudCompraDTO detalleSolicitudCompraDTO)
        {
            return new DetalleSolicitudCompra
            {
                CantProductos = detalleSolicitudCompraDTO.cantidadProductos,
                IndEstado = detalleSolicitudCompraDTO.Estado,
                PkdetalleSolicitudCompra = detalleSolicitudCompraDTO.IdEntidad,
                MtoLinea = detalleSolicitudCompraDTO.Monto
            };
        }
        #endregion

        #region Busquedas

        public List<BaseDTO> ListaDetalleCompraSinEncabezado()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                DetalleSolicitudCompraDatos intermedio = new DetalleSolicitudCompraDatos(contexto);

                var resultado = intermedio.ListarDetalleSolicitudSinEncabezado();

                if (resultado.Codigo > 0)
                {
                    //Respuesta positiva
                    var detalleSolicitudes = (List<DetalleSolicitudCompra>)resultado.Contenido;

                    foreach (var item in detalleSolicitudes)
                    {
                        var itemConvertido = ConvertirEntidadDetalleSolicitudCompraDTO(item);
                        respuesta.Add(itemConvertido);
                    }

                    return respuesta;

                }
                {
                    var error = (ErrorDTO)resultado.Contenido;
                    respuesta.Add(error);
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta.Clear();
                var errorCatch = new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
                respuesta.Add(errorCatch);

                return respuesta;
            }
        }
        #endregion

        #endregion
    }
}
