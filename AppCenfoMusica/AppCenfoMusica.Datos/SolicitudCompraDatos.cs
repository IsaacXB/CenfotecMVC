using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;

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

        #endregion
    }
}
