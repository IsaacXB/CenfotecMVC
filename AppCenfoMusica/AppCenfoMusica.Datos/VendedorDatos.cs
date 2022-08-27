using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.Datos.Helpers;
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

        public RespuestaDTO ValidarVendedor(string userName, string password)
        {
            try
            {
                var vendedor = contexto.Vendedors.FirstOrDefault(x => x.NomUsuario == userName && x.IndContrasena == password);

                if (vendedor != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedor
                    };
                }
                else
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = new ErrorDTO() {CodigoError = -1, MensajeError ="Usuario o Contraseña incorrecta." }
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

        //public RespuestaDTO AgregarVendedor(VendedorDTO vendedorDTO)
        //{
        //    try
        //    {
        //        contexto.Vendedors.Add(new Vendedor
        //        {
        //            NomVendedor = vendedorDTO.Nombre,
        //            IndPuesto = vendedorDTO.Puesto,
        //            NomUsuario = vendedorDTO.NombreUsuario,
        //            IndContrasena = vendedorDTO.Contrasena,
        //            IndCedula = vendedorDTO.Cedula,
        //            IndEstado = vendedorDTO.Estado
        //        });

        //        if (contexto.SaveChanges() > 0)
        //        {
        //            return new RespuestaDTO
        //            {
        //                Codigo = 1,
        //                Contenido = vendedorDTO
        //            };
        //        }
        //        else
        //        {
        //            throw new Exception("No se logró realizar el guardado de datos solicitado.");
        //        }

        //        return new RespuestaDTO
        //        {
        //            Codigo = 1,
        //            Contenido = vendedorDTO
        //        };
        //    }
        //    catch (Exception error)
        //    {
        //        return new RespuestaDTO
        //        {
        //            Codigo = -1,
        //            Contenido = new ErrorDTO
        //            {
        //                MensajeError = error.Message
        //            }
        //        };
        //    }
        //}

        public RespuestaDTO AgregarVendedor(RespuestaDTO vendedor)
        {
            try
            {
                if (vendedor.Contenido == null) throw new Exception("No se logró realizar el guardado de datos solicitado.");

                contexto.Vendedors.Add((Vendedor)vendedor.Contenido);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO { Codigo = 1, Contenido = (Vendedor) vendedor.Contenido };
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

                    if (vendedor.IndEstado == estado)
                    {
                        return new RespuestaDTO { Codigo = 1, Contenido = new ErrorDTO() {CodigoError = -1, MensajeError = "No se han detectado cambios." } };
                    }

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

        public RespuestaDTO ActualizarVendedor(int idvendedor, string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            try
            {
                var vendedor = contexto.Vendedors.FirstOrDefault(x => x.Pkvendedor == idvendedor);

                if (vendedor != null)
                {
                    vendedor.NomVendedor = !string.IsNullOrEmpty(nombre) ? nombre : vendedor.NomVendedor;
                    vendedor.IndPuesto = puesto > 0 ? puesto : vendedor.IndPuesto;
                    vendedor.NomUsuario = !string.IsNullOrEmpty(nomUsuario) ? nomUsuario : vendedor.NomUsuario;
                    vendedor.IndContrasena = !string.IsNullOrEmpty(contrasena) ? contrasena : vendedor.IndContrasena;
                    vendedor.IndCedula = !string.IsNullOrEmpty(cedula) ? cedula : vendedor.IndCedula;
                    vendedor.IndEstado = estado > 0 ? estado : vendedor.IndEstado;
                }

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = vendedor
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

        public RespuestaDTO EliminarVendedor(int idVendedor)
        {
            try
            {
                var respuesta = BuscarVendedorPorIdDTOValidacion(idVendedor);

                if (respuesta == null || respuesta.Contenido == null) throw new Exception("No se pudo actualizar el vendedor especificado.");


                if (respuesta.Contenido.GetType() != typeof(ErrorDTO))
                {
                    contexto.Vendedors.Remove(((Vendedor)respuesta.Contenido));

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            Codigo = 1,
                            Contenido = new BaseDTO { Mensaje = "El cliente se elimino satisfacoriamente" }
                        };
                    }
                }

                throw new Exception("No se pudo actualizar el cliente especificado");
            }
            catch (Exception error)
            {
                return ControladorRetornos.ControladorErrores(error);
            }
        }

        #endregion

        #region Filtrado

        // Filtro por parámetros solidos
        public RespuestaDTO FiltradoVendedoresParametrosSolidos( string nombre, int puesto, string nomUsuario, string cedula, int estado)
        {
            try
            {
                List<Vendedor> datosEncontrados = new List<Vendedor>();
                if (nombre != null)
                {
                    datosEncontrados = contexto.Vendedors.Where(p => p.NomVendedor.Contains(nombre)).ToList();
                }
                if (puesto > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IndPuesto == puesto).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Vendedors.Where(p => p.IndPuesto == puesto).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(nomUsuario))
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.NomUsuario == nomUsuario).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Vendedors.Where(p => p.NomUsuario == nomUsuario).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(cedula))
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IndCedula == cedula).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Vendedors.Where(p => p.IndCedula == cedula).ToList();
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
                        datosEncontrados = contexto.Vendedors.Where(p => p.IndEstado == estado).ToList();
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

        public RespuestaDTO FiltradoVendedoresParametrosAnonimos(List<Vendedor> datosEncontrados, string nombreParametro, object valorParametro)
        {
            try
            {
                if (datosEncontrados.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "nombre":
                            datosEncontrados = datosEncontrados.Where(p => p.NomVendedor.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "puesto":
                            datosEncontrados = datosEncontrados.Where(p => p.IndPuesto.Equals(valorParametro)).ToList();
                            break;
                        case "nomUsuario":
                            datosEncontrados = datosEncontrados.Where(p => p.NomUsuario.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "cedula":
                            datosEncontrados = datosEncontrados.Where(p => p.IndCedula.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "estado":
                            datosEncontrados = datosEncontrados.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        default:
                            throw new Exception("Parámetro no establecido.");
                    }
                }
                else
                {
                    switch (nombreParametro)
                    {
                        case "nombre":
                            datosEncontrados = contexto.Vendedors.Where(p => p.NomVendedor.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "puesto":
                            datosEncontrados = contexto.Vendedors.Where(p => p.IndPuesto.Equals(valorParametro)).ToList();
                            break;
                        case "nomUsuario":
                            datosEncontrados = contexto.Vendedors.Where(p => p.NomUsuario.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "cedula":
                            datosEncontrados = contexto.Vendedors.Where(p => p.IndCedula.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "estado":
                            datosEncontrados = contexto.Vendedors.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
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
