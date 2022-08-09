using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.Datos.Helpers;
using AppCenfoMusica.DTO;
using Microsoft.EntityFrameworkCore;

namespace AppCenfoMusica.Datos
{
    public class SolicitudCompraDatos
    {
        #region Constructor     
        public SolicitudCompraDatos(CenfomusicaContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        #endregion

        #region Variables  

        CenfomusicaContext contexto = new CenfomusicaContext();

        #endregion

        #region Metodos

        #region Consultas
        public SolicitudCompra? BuscarSolicitudCompraPorPK(int solicitudCompraPK)
        {
            var solicitudCompra = contexto.SolicitudCompras.FirstOrDefault(x => x.PksolicitudCompra == solicitudCompraPK);

            return solicitudCompra;

        }
        public RespuestaDTO BuscarSolicitudCompraPorPKDTO(int solicitudCompraPK)
        {
            var solicitudCompra = contexto.SolicitudCompras.FirstOrDefault(x => x.PksolicitudCompra == solicitudCompraPK);
            return new RespuestaDTO
            {
                Codigo = 1,
                Contenido = solicitudCompra
            };

        }
        public RespuestaDTO? BuscarSolicitudCompraPorPKDTOValidacion(int solicitudCompraPK)
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.FirstOrDefault(x => x.PksolicitudCompra == solicitudCompraPK);

                if (solicitudCompra != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
                    };
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
            return null;

        }
        public RespuestaDTO ListarSolicitudesDeCompraPorEstado(int estado)
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.Where(x => x.IndEstado == estado).ToList();
                if (solicitudCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
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
        public RespuestaDTO ListarSolicitudesDeCompraCliente(int cliente)
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.Where(x => x.Fkcliente == cliente).ToList();
                if (solicitudCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
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
        public RespuestaDTO ListarSolicitudesDeCompraPorVendedor(int vendedor)
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.Where(x => x.Fkvendedor == vendedor).ToList();
                if (solicitudCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
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
        public RespuestaDTO ListarSolicitudesDeCompraPorTipoDeEntrega(int tipoEntrega)
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.Where(x => x.IndTipoEntrega == tipoEntrega).ToList();
                if (solicitudCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
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
        public RespuestaDTO FiltradoProductosParametrosAnonimos(List<SolicitudCompra> datosEncontrados, string nombreParametro, object valorParametro)
        {
            try
            {
                if (datosEncontrados.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "PrimaryKey":
                            datosEncontrados = datosEncontrados.Where(p => p.PksolicitudCompra == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "Estado":
                            datosEncontrados = datosEncontrados.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "Cliente":
                            datosEncontrados = datosEncontrados.Where(p => p.Fkcliente == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "Vendedor":
                            datosEncontrados = datosEncontrados.Where(p => p.Fkvendedor == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "TipoDeEntrega":
                            datosEncontrados = datosEncontrados.Where(p => p.IndTipoEntrega == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        default:
                            throw new Exception("Parámetro no establecido.");
                    }
                }
                else
                {
                    switch (nombreParametro)
                    {
                        case "PrimaryKey":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.PksolicitudCompra == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "Estado":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "Cliente":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.Fkcliente == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "Vendedor":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.Fkvendedor == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "TipoDeEntrega":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.IndTipoEntrega == Convert.ToUInt32(valorParametro)).ToList();
                            break;

                        default:
                            throw new Exception("Parámetro no establecido.");
                    }
                }
                if (datosEncontrados.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = datosEncontrados
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
                    Contenido = new ErrorDTO { MensajeError = error.Message }
                };
            }

        }
        public RespuestaDTO ListarSolicitudes()
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.ToList();
                if (solicitudCompra.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
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
        public RespuestaDTO BuscarSolicitudCompraConDetalle(int codigoSolicitud)
        {
            try
            {
                var solicitud = contexto.SolicitudCompras.Include(S => S.DetalleSolicitudCompras).FirstOrDefault(S => S.PksolicitudCompra == codigoSolicitud);

                if (solicitud != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitud
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningún producto con el código especificado");
                }
            }
            catch (Exception error)
            {
                return ControladorRetornos.ControladorErrores(error);
            }
        }
        public RespuestaDTO BuscarSolicitudCompraSinDetalle(int codigoSolicitud)
        {
            try
            {
                var solicitud = contexto.SolicitudCompras.FirstOrDefault(S => S.PksolicitudCompra == codigoSolicitud);

                if (solicitud != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitud
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningún producto con el código especificado");
                }
            }
            catch (Exception error)
            {
                return ControladorRetornos.ControladorErrores(error);
            }
        }
        public RespuestaDTO ComprasMayoresACiertaCantidad(decimal montoLimite)
        {
            try
            {
                //               1             2        3    4           4.2              4.3   4.4        
                var ventas = contexto.SolicitudCompras.Where(S => S.DetalleSolicitudCompras.Sum(D => D.MtoLinea) >= montoLimite);

                if (ventas.Count() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = ventas
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningún producto con el código especificado");
                }
            }
            catch (Exception error)
            {
                return ControladorRetornos.ControladorErrores(error);
            }
        }
        public object BuscarSolicitudCompraPorID(int id)
        {
            var solicitudCompra = contexto.SolicitudCompras.FirstOrDefault(P => P.PksolicitudCompra == id);

            return solicitudCompra;

        }
        #endregion

        #region Inserciones
        public RespuestaDTO AgregarSolicitudCompra(DateTime fechaEntrega, DateTime fechaSolicitud, int vendedor, int cliente, int estado, int tipoEntrega)
        {
            try
            {
                var solicitudCompra = new SolicitudCompra
                {
                    FecEntrega = fechaEntrega,
                    FecSolicitud = fechaSolicitud,
                    Fkvendedor = vendedor,
                    Fkcliente = cliente,
                    IndEstado = estado,
                    IndTipoEntrega = tipoEntrega
                };

                contexto.SolicitudCompras.Add(solicitudCompra);
                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = cliente
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }
            }
            catch (Exception error)
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

        public RespuestaDTO AgregarSolicitudCompra(SolicitudCompra solicitudCompra)
        {
            try
            {
                contexto.SolicitudCompras.Add(solicitudCompra);
                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }
            }
            catch (Exception error)
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

        #region Actualizaciones

        public RespuestaDTO ActualizarSolicitudCompra(RespuestaDTO solicicitudCompraActualizar)
        {
            try
            {
                var solicitudCompra = (SolicitudCompra)solicicitudCompraActualizar.Contenido;

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
                    };
                }

                throw new Exception("No se pudo actualizar la solicitud de compra especificada.");
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

        #region Actualizaciones
        public RespuestaDTO ActualizarSolicitudCompra(int idSolicitudCompra, DateTime? fechaEntrega, DateTime? fechaSolicitud, 
            int vendedor, int cliente, int estado, int tipoEntrega)
        {
            try
            {
                var solicitudCompra = contexto.SolicitudCompras.FirstOrDefault(x => x.PksolicitudCompra == idSolicitudCompra);

                if (solicitudCompra != null)
                {
                    solicitudCompra.IndEstado = estado > 0 ? estado : solicitudCompra.IndEstado;
                    solicitudCompra.Fkvendedor = vendedor > 0 ? vendedor : solicitudCompra.Fkvendedor;
                    solicitudCompra.Fkcliente = cliente > 0 ? cliente : solicitudCompra.Fkcliente;
                    solicitudCompra.IndTipoEntrega = tipoEntrega > 0 ? tipoEntrega : solicitudCompra.IndTipoEntrega;
                    solicitudCompra.FecEntrega = fechaEntrega != null ? fechaEntrega : solicitudCompra.FecEntrega;
                    solicitudCompra.FecSolicitud = fechaSolicitud != null ? fechaSolicitud : solicitudCompra.FecSolicitud;
                }

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = solicitudCompra
                    };
                }

                throw new Exception("No se pudo actualizar el cliente especificado");
            }
            catch (Exception error)
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

        #endregion

        #region Eliminaciones

        public RespuestaDTO EliminarSolicicitudCompra(int idSolicitudCompra)
        {
            try
            {
                var respuesta = BuscarSolicitudCompraPorPKDTOValidacion(idSolicitudCompra);

                if (respuesta == null || respuesta.Contenido == null) throw new Exception("No se pudo actualizar la solicitud de compra especificada.");


                if (respuesta.Contenido.GetType() != typeof(ErrorDTO))
                {
                    contexto.SolicitudCompras.Remove(((SolicitudCompra)respuesta.Contenido));

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            Codigo = 1,
                            Contenido = new BaseDTO { Mensaje = "La solicitud de compra se elimino satisfacoriamente" }
                        };
                    }
                }

                throw new Exception("No se pudo actualizar la solicitud de compra especificada");
            }
            catch (Exception error)
            {
                return ControladorRetornos.ControladorErrores(error);
            }
        }
        #endregion

        #region Filtrado

        // Filtro por parámetros solidos
        public RespuestaDTO FiltradoSolicicitudComprasParametrosSolidos(DateTime? fechaEntregaInicial, DateTime? fechaEntregaFinal, 
            DateTime? fechaSolicitudInicial, DateTime? fechaSolicitudFinal, int vendedor, int cliente, int estado, int tipoEntrega)
        {
            try
            {
                List<SolicitudCompra> datosEncontrados = new List<SolicitudCompra>();
                if (fechaEntregaInicial != null)
                {
                    datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecEntrega >= fechaEntregaInicial).ToList();
                }
                if (fechaEntregaFinal != null)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.FecEntrega <= fechaEntregaFinal).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecEntrega <= fechaEntregaFinal).ToList();
                    }
                }
                if (fechaSolicitudInicial != null)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.FecSolicitud >= fechaSolicitudInicial).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecSolicitud >= fechaSolicitudInicial).ToList();
                    }
                }
                if (fechaSolicitudFinal != null)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.FecSolicitud <= fechaSolicitudFinal).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecSolicitud <= fechaSolicitudFinal).ToList();
                    }
                }
                if (vendedor > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.Fkvendedor == vendedor).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.Fkvendedor == vendedor).ToList();
                    }
                }
                if (cliente > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.Fkcliente == cliente).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.Fkcliente == cliente).ToList();
                    }
                }
                if (estado > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IndEstado == estado).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.IndEstado == estado).ToList();
                    }
                }
                if (tipoEntrega > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IndTipoEntrega == tipoEntrega).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.SolicitudCompras.Where(p => p.IndTipoEntrega == tipoEntrega).ToList();
                    }
                }

                if (datosEncontrados.Count > 1)
                {
                    return new RespuestaDTO { Codigo = 1, Contenido = datosEncontrados };
                }
                else
                {
                    throw new Exception("No se encontron productos para los parametros establecidos.");
                }

            }
            catch (System.Exception error)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO { MensajeError = error.Message }
                };
            }
        }

        //Filtro por parámetros anónimos

        public RespuestaDTO FiltradoSolicitudComprasParametrosAnonimos(List<SolicitudCompra> datosEncontrados, string nombreParametro, object valorParametro)
        {
            try
            {
                if (datosEncontrados.Count > 0)
                {
                    switch (nombreParametro)
                    {

                        case "fechaEntregaInicial":
                            datosEncontrados = datosEncontrados.Where(p => p.FecEntrega >= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "fechaEntregaFinal":
                            datosEncontrados = datosEncontrados.Where(p => p.FecEntrega <= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "fechaSolicitudInicial":
                            datosEncontrados = datosEncontrados.Where(p => p.FecSolicitud >= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "fechaSolicitudFinal":
                            datosEncontrados = datosEncontrados.Where(p => p.FecSolicitud <= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "vendedor":
                            datosEncontrados = datosEncontrados.Where(p => p.Fkvendedor == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "cliente":
                            datosEncontrados = datosEncontrados.Where(p => p.Fkcliente == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "estado":
                            datosEncontrados = datosEncontrados.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "tipoEntrega":
                            datosEncontrados = datosEncontrados.Where(p => p.IndTipoEntrega == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        default:
                            throw new Exception("Parámetro no establecido.");
                    }
                }
                else
                {
                    switch (nombreParametro)
                    {
                        case "fechaEntregaInicial":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecEntrega >= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "fechaEntregaFinal":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecEntrega <= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "fechaSolicitudInicial":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecSolicitud >= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "fechaSolicitudFinal":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.FecSolicitud <= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "vendedor":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.Fkvendedor == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "cliente":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.Fkcliente == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "estado":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "tipoEntrega":
                            datosEncontrados = contexto.SolicitudCompras.Where(p => p.IndTipoEntrega == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        default:
                            throw new Exception("Parámetro no establecido.");
                    }
                }
                return new RespuestaDTO
                {
                    Codigo = 1,
                    Contenido = datosEncontrados
                };
            }
            catch (System.Exception error)
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
