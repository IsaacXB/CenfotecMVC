using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;

namespace AppCenfoMusica.Logica
{
    public class SolicitudCompraLogica
    {
        #region Variables

        CenfomusicaContext contexto;

        #endregion

        #region Constructor

        public SolicitudCompraLogica()
        {
            contexto = new CenfomusicaContext();
        }

        #endregion

        #region Métodos 

        #region Traductores

        internal static SolicitudCompraDTO ConvertirEntidadSolicitudCompraDTO(SolicitudCompra solicitudCompra)
        {
            return new SolicitudCompraDTO
            {
                Estado = Convert.ToInt32(solicitudCompra.IndEstado),
                FechaEntrega = Convert.ToDateTime(solicitudCompra.FecEntrega),
                FechaSolicitud = Convert.ToDateTime(solicitudCompra.FecSolicitud),
                IdEntidad = Convert.ToInt32(solicitudCompra.PksolicitudCompra),
                TipoEntrega = Convert.ToInt32(solicitudCompra.IndTipoEntrega)

            };
        }

        internal static SolicitudCompra ConvertirDTOSolicitudCompraAEntidad(SolicitudCompraDTO solicitudCompraDTO)
        {
            return new SolicitudCompra
            {
                FecEntrega = solicitudCompraDTO.FechaEntrega,
                FecSolicitud = solicitudCompraDTO.FechaSolicitud,
                IndEstado = solicitudCompraDTO.Estado,
                IndTipoEntrega = solicitudCompraDTO.TipoEntrega,
                PksolicitudCompra = solicitudCompraDTO.IdEntidad
            };
        }

        #endregion

        #region Inserciones
        public List<BaseDTO> RegistrarSolicitudCompraCompletaConTransaccionEnLogica(SolicitudCompraDTO solicitud, List<DetalleSolicitudCompraDTO> detalles)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();

            // Podríamos tener el using a nivel global del método para que el catch también haga dispose

            try
            {
                using (var transaccion = contexto.Database.BeginTransaction())
                {
                    SolicitudCompraDatos intermedioSolicitud = new SolicitudCompraDatos(contexto);
                    DetalleSolicitudCompraDatos intermedioDetalle = new DetalleSolicitudCompraDatos(contexto);

                    var solicitudConvertida = ConvertirDTOSolicitudCompraAEntidad(solicitud);

                    var resultadoSolicitud = intermedioSolicitud.AgregarSolicitudCompra(solicitudConvertida);

                    if (resultadoSolicitud.Codigo > 0)
                    {
                        //Inserción de Detalles

                        respuesta.Add(ConvertirEntidadSolicitudCompraDTO((SolicitudCompra)resultadoSolicitud.Contenido));

                        foreach (var item in detalles)
                        {
                            var detalleConvertido = DetalleSolicitudCompraLogica.ConvertirDTODetalleSolicitudCompraAEntidad(item);

                            detalleConvertido.FksolicitudCompra = ((SolicitudCompra)resultadoSolicitud.Contenido).PksolicitudCompra;

                            var resultadoDetalle = intermedioDetalle.AgregarDetalleSolicitudCompra(detalleConvertido);

                            if (resultadoDetalle.Codigo > 0)
                            {
                                respuesta.Add(DetalleSolicitudCompraLogica.ConvertirEntidadDetalleSolicitudCompraDTO((DetalleSolicitudCompra)resultadoDetalle.Contenido));
                            }
                            else
                            {
                                transaccion.Rollback();
                                respuesta.Clear();
                                var error = (ErrorDTO)resultadoSolicitud.Contenido;
                                respuesta.Add(error);
                                return respuesta;
                            }
                        }

                        transaccion.Commit();
                        return respuesta;
                    }
                    else
                    {
                        transaccion.Dispose();
                        var error = (ErrorDTO)resultadoSolicitud.Contenido;
                        respuesta.Add(error);
                        return respuesta;
                    }
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

        public List<BaseDTO> RegistrarSolicitudCompraCompletaConTransaccionEnDatos(SolicitudCompraDTO solicitud, List<DetalleSolicitudCompraDTO> detalles)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                SolicitudCompraDatos intermedio = new SolicitudCompraDatos(contexto);

                var solicitudDatos = ConvertirDTOSolicitudCompraAEntidad(solicitud);

                List<DetalleSolicitudCompra> detallesDatos = new List<DetalleSolicitudCompra>();

                foreach (var item in detalles)
                {
                    detallesDatos.Add(DetalleSolicitudCompraLogica.ConvertirDTODetalleSolicitudCompraAEntidad(item));
                }

                var resultado = intermedio.AgregarSolicitudCompraCompleta(solicitudDatos, detallesDatos);

                if (resultado.Codigo > 0)
                {
                    var solicitudRespuesta = (SolicitudCompra)resultado.Contenido;

                    respuesta.Add(ConvertirEntidadSolicitudCompraDTO(solicitudRespuesta));

                    foreach (var item in solicitudRespuesta.DetalleSolicitudCompras)
                    {
                        respuesta.Add(DetalleSolicitudCompraLogica.ConvertirEntidadDetalleSolicitudCompraDTO(item));
                    }

                    return respuesta;
                }
                else
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

        #region Busquedas

        public List<BaseDTO> ListaSolicitudCompraSinDetalle()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                SolicitudCompraDatos intermedio = new SolicitudCompraDatos(contexto);

                var resultado = intermedio.ListarSolicitudes();

                if (resultado.Codigo > 0)
                {
                    //Respuesta positiva
                    var solicitudesDeCompras = (List<SolicitudCompra>)resultado.Contenido;

                    foreach (var item in solicitudesDeCompras)
                    {
                        var itemConvertido = ConvertirEntidadSolicitudCompraDTO(item);
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

        public List<BaseDTO> FiltrarSolicitudCompraConDetalles(List<DateTime> fechaEntrega, List<DateTime> fechaSolicitud, int tipoEntrega,
    int codigoProducto, int codigoCliente, int codigoVendedor)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                SolicitudCompraDatos intermedio = new SolicitudCompraDatos(contexto);

                var resultado = intermedio.FiltrarSolicitudCompraConDetalles(fechaEntrega, fechaSolicitud, tipoEntrega, codigoProducto, codigoCliente, codigoVendedor);

                if (resultado.Codigo > 0)
                {
                    //Resultado positivo
                    var solicitudesCompra = (List<SolicitudCompra>)resultado.Contenido;

                    foreach (var item in solicitudesCompra)
                    {
                        var itemConvertido = ConvertirEntidadSolicitudCompraDTO(item);
                        respuesta.Add(itemConvertido);
                    }

                    return respuesta;
                }
            }
            catch (System.Exception error)
            {
                respuesta.Clear();
                var errorCatch = new ErrorDTO { CodigoError = -1, MensajeError = error.Message };
                respuesta.Add(errorCatch);
                return respuesta;
            }
            return respuesta;
        }

        #endregion


        #endregion
    }
}
