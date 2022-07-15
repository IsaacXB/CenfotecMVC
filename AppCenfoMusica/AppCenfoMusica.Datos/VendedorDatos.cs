using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.Datos
{
    public class VendedorDatos
    {
        #region Constructor     
        public VendedorDatos(CenfomusicaContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        #endregion

        #region Variables  

        CenfomusicaContext contexto = new CenfomusicaContext();

        #endregion

        #region Metodos

        #region Consultas
        public Vendedor? BuscarVendedorPorId(int id)
        {
            var vendedor = contexto.Vendedors.FirstOrDefault(x => x.Pkvendedor == id);

            return vendedor;

        }
        public RespuestaDTO BuscarVendedorPorIdDTO(int id)
        {
            var vendedor = contexto.Vendedors.FirstOrDefault(x => x.Pkvendedor == id);
            return new RespuestaDTO
            {
                Codigo = 1,
                Contenido = vendedor
            };

        }
        public RespuestaDTO? BuscarVendedorPorIdDTOValidacion(int id)
        {
            try
            {
                var vendedor = contexto.Vendedors.FirstOrDefault(x => x.Pkvendedor == id);

                if (vendedor != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedor
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
        public RespuestaDTO? BuscarVendedoresPorPuesto(int id)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(x => x.IndPuesto == id).ToList();

                if (vendedores.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedores
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningún vendedor con el código especificado");
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
        public RespuestaDTO ListarVendedores()
        {
            try
            {
                var vendedores = contexto.Vendedors.ToList();
                if (vendedores.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedores
                    };
                }
                else
                {
                    throw new Exception("No se encontraron vendedores en este momento.");
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
        public RespuestaDTO AgregarVendedor(string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            try
            {
                var vendedor = new Vendedor
                {
                    NomVendedor = nombre,
                    IndPuesto = puesto,
                    NomUsuario = nomUsuario,
                    IndContrasena = contrasena,
                    IndCedula = cedula,
                    IndEstado = estado
                };

                contexto.Vendedors.Add(vendedor);
                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedor
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }


                return new RespuestaDTO
                {
                    Codigo = 1,
                    Contenido = vendedor
                };
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

        public RespuestaDTO AgregarVendedor(Vendedor vendedor)
        {
            try
            {
                contexto.Vendedors.Add(vendedor);
                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedor
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }


                return new RespuestaDTO
                {
                    Codigo = 1,
                    Contenido = vendedor
                };
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

        public RespuestaDTO AgregarVendedor(VendedorDTO vendedorDTO)
        {
            try
            {
                contexto.Vendedors.Add(new Vendedor
                {
                    NomVendedor = vendedorDTO.Nombre,
                    IndPuesto = vendedorDTO.Puesto,
                    NomUsuario = vendedorDTO.NombreUsuario,
                    IndContrasena = vendedorDTO.Contrasena,
                    IndCedula = vendedorDTO.Cedula,
                    IndEstado = vendedorDTO.Estado
                });

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedorDTO
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }

                return new RespuestaDTO
                {
                    Codigo = 1,
                    Contenido = vendedorDTO
                };
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

        public RespuestaDTO AgregarVendedor(RespuestaDTO vendedor)
        {
            try
            {
                if (vendedor.Contenido == null) throw new Exception("No se logró realizar el guardado de datos solicitado.");

                contexto.Vendedors.Add((Vendedor)vendedor.Contenido);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO { Codigo = 1, Contenido = vendedor };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
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

        #region Actualizaciones 

        public RespuestaDTO ActualizarEstadoVendedor(int codigoVendedor, int estado)
        {
            try
            {
                var vendedor = contexto.Vendedors.FirstOrDefault(x => x.Pkvendedor == codigoVendedor);

                if (vendedor != null)
                {
                    vendedor.IndEstado = estado;

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO { Codigo = 1, Contenido = vendedor };
                    }
                }
                throw new Exception("No se pudo actualizar el vendedor especificado.");

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

        public RespuestaDTO ActualizarVendedorPorPuesto(int codigoVendedor, string puesto)
        {
            try
            {
                var vendedores = contexto.Vendedors.Where(x => x.Pkvendedor == codigoVendedor);

                if (vendedores != null)
                {
                    foreach (var item in vendedores)
                    {

                        if (contexto.SaveChanges() > 0)
                        {
                            return new RespuestaDTO { Codigo = 1, Contenido = vendedores };
                        }
                    }

                }
                throw new Exception("No se pudo actualizar el vendedor especificado.");

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

        #region Eliminacion

        public RespuestaDTO EliminarVendedoresInactivos()
        {
            try
            {

                var vendedores = contexto.Vendedors.Remove((Vendedor)contexto.Vendedors.Where(x => x.IndEstado == 3));

                throw new Exception("No se pudo actualizar el vendedor especificado");
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
