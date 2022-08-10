using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.Datos
{
    public class DetalleSolicitudCompraDatos
    {
        #region Variables

        CenfomusicaContext contexto = new CenfomusicaContext();

        #endregion

        #region Constructor

        public DetalleSolicitudCompraDatos(CenfomusicaContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }

        #endregion

        #region Metodos

        #region Inserciones
        public RespuestaDTO AgregarDetalleSolicitudCompra(DetalleSolicitudCompra detalle)
        {
            try
            {
                //  1        2     3
                contexto.DetalleSolicitudCompras.Add(detalle);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = detalle // Dentro de este objeto, que se retorna posterior al SaveChanges, ya se tiene actualizado el valor del PK
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado");
                }
            }
            catch (Exception error)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO { MensajeError = error.Message }
                };
            }
        }
        #endregion

        #region Consultas
        public object BuscarSolicitudCompraDetallePorID(int id)
        {
            var solicitudCompra = contexto.DetalleSolicitudCompras.FirstOrDefault(P => P.PkdetalleSolicitudCompra == id);

            return solicitudCompra;

        }

        public RespuestaDTO BuscarSolicitudCompraDetallePorID_DTO_Validacion(int id)
        {
            try
            {
                var producto = contexto.DetalleSolicitudCompras.FirstOrDefault(P => P.PkdetalleSolicitudCompra == id);

                if (producto != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = producto
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningúna solicitud de compra detalle con el código especificado");
                }
            }
            catch (Exception errorClaseException)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO { MensajeError = errorClaseException.Message }
                };
            }
        }

        public RespuestaDTO ListarDetalleSolicitudSinEncabezado()
        {
            try
            {
                var detalleSolicitud = contexto.DetalleSolicitudCompras.ToList();
                if (detalleSolicitud.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = detalleSolicitud
                    };
                }
                else
                {
                    throw new Exception("No se encontraron clientes en este momento con ese estado.");
                }
            }
            catch (System.Exception error)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO
                    {
                        MensajeError = error.Message
                    }
                };
            }
        }
        #endregion

        #region Eliminaciones
        public RespuestaDTO EliminarDetalleSolicitudCompra(int idDetalleSolicitudCompra)
        {
            try
            {
                using (var transaccion = contexto.Database.BeginTransaction())
                {
                    var respuesta = BuscarSolicitudCompraDetallePorID_DTO_Validacion(idDetalleSolicitudCompra);

                    if (respuesta.Contenido.GetType() != typeof(ErrorDTO))
                    {
                        contexto.DetalleSolicitudCompras.Remove(((DetalleSolicitudCompra)respuesta.Contenido));                     
                        
                        if (contexto.SaveChanges() > 0)
                        {
                            transaccion.Commit();
                            return new RespuestaDTO
                            {
                                Codigo = 1,
                                Contenido = new BaseDTO { Mensaje = "El detalle de solicitud de compra se eliminó satisfactoriamente" }
                            };
                        }
                        else
                        {
                            transaccion.Rollback();

                        }
                    }
                    throw new Exception("No se pudo actualizar el detalle de la solicitud de compra especificada");


                }
            }
            catch (Exception error)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO { MensajeError = error.Message }
                };
            }
        }

        #endregion

        #endregion
    }
}
