using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.Datos.Helpers;
using AppCenfoMusica.DTO;

namespace AppCenfoMusica.Datos
{
    public class ClienteDatos
    {
        #region Constructor     
        public ClienteDatos(CenfomusicaContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        #endregion

        #region Variables  

        CenfomusicaContext contexto = new CenfomusicaContext();

        #endregion

        #region Metodos

        #region Consultas
        public Cliente? BuscarVendedorPorCedula(string cedula)
        {
            var vendedor = contexto.Clientes.FirstOrDefault(x => x.IdCedula == cedula);

            return vendedor;

        }
        public RespuestaDTO BuscarClientePorCedulaDTO(string cedula)
        {
            var vendedor = contexto.Clientes.FirstOrDefault(x => x.IdCedula == cedula);
            return new RespuestaDTO
            {
                Codigo = 1,
                Contenido = vendedor
            };

        }
        public RespuestaDTO? BuscarClientePorCedulaDTOValidacion(string cedula)
        {
            try
            {
                var vendedor = contexto.Clientes.FirstOrDefault(x => x.IdCedula == cedula);

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

        public RespuestaDTO? BuscarClientePorIdDTOValidacion(int id)
        {
            try
            {
                var cliente = contexto.Clientes.FirstOrDefault(x => x.Pkcliente == id);

                if (cliente != null)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = cliente
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
        public RespuestaDTO? BuscarClientesPorNombre(string nombre)
        {
            try
            {
                var clientes = contexto.Clientes.Where(x => x.NomUsuario.Contains(nombre)).ToList();

                if (clientes.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = clientes
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningún cliente con el nombre especificado");
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
        public RespuestaDTO ListarClientesPorEstado(int estado)
        {
            try
            {
                var clientes = contexto.Clientes.Where(x => x.IndEstado == estado).ToList();
                if (clientes.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = clientes
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
        public RespuestaDTO ListarClientesPorFechaDeNacimiento(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var clientes = contexto.Clientes.Where(x => x.FecNacimiento >= fechaInicial && x.FecNacimiento <= fechaFinal).ToList();
                if (clientes.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = clientes
                    };
                }
                else
                {
                    throw new Exception("No se encontraron clientes en este momento con ese rango de fechas.");
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
        public RespuestaDTO AgregarCliente(string nombre, string email, string nomUsuario, string contrasena, string cedula, int estado, int sexo, DateTime fechaNacimiento, string telefono)
        {
            try
            {
                var cliente = new Cliente
                {
                    NomCliente = nombre,
                    EmlCorreo = email,
                    NomUsuario = nomUsuario,
                    IndContrasena = contrasena,
                    IdCedula = cedula,
                    IndEstado = estado,
                    IndSexo = sexo,
                    FecNacimiento = fechaNacimiento,
                    TelCliente = telefono
                };

                contexto.Clientes.Add(cliente);
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

        public RespuestaDTO AgregarCliente(Cliente cliente)
        {
            try
            {
                contexto.Clientes.Add(cliente);
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

        public RespuestaDTO AgregarCliente(ClienteDTO clienteDTO)
        {
            try
            {
                contexto.Clientes.Add(new Cliente
                {
                    NomCliente = clienteDTO.Nombre,
                    NomUsuario = clienteDTO.NombreUsuario,
                    IndContrasena = clienteDTO.Contrasena,
                    IdCedula = clienteDTO.Cedula,
                    IndEstado = clienteDTO.Estado,
                    IndSexo = clienteDTO.Sexo,
                    FecNacimiento = clienteDTO.FechaNacimiento,
                    TelCliente = clienteDTO.Telefono
                });

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = clienteDTO
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

        public RespuestaDTO AgregarCliente (RespuestaDTO cliente)
        {
            try
            {
                if (cliente.Contenido == null) throw new Exception("No se logró realizar el guardado de datos solicitado.");

                contexto.Clientes.Add((Cliente)cliente.Contenido);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO { Codigo = 1, Contenido = cliente };
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

        public RespuestaDTO ActualizarCliente(RespuestaDTO clienteActualizar)
        {
            try
            {   var clienteDatos = (Cliente)clienteActualizar.Contenido;

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = clienteDatos
                    };
                }

                throw new Exception("No se pudo actualizar el cliente especificado");
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

        #region Eliminaciones
        public RespuestaDTO EliminarCliente(int idCliente)
        {
            try
            {
                var respuesta = BuscarClientePorIdDTOValidacion(idCliente);

                if (respuesta == null || respuesta.Contenido == null) throw new Exception("No se pudo actualizar el cliente especificado.");


                if (respuesta.Contenido.GetType() != typeof(ErrorDTO))
                {
                    contexto.Clientes.Remove(((Cliente)respuesta.Contenido));

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
        public RespuestaDTO FiltradoClientesParametrosSolidos(string nombre, string email, string nomUsuario, string cedula, int estado, int sexo, DateTime? fechaNacimiento, string telefono)
        {
            try
            {
                List<Cliente> datosEncontrados = new List<Cliente>();
                if (nombre != null)
                {
                    datosEncontrados = contexto.Clientes.Where(p => p.NomCliente.Contains(nombre)).ToList();
                }
                if (!string.IsNullOrEmpty(email))
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.EmlCorreo == email).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Clientes.Where(p => p.EmlCorreo == email).ToList();
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
                        datosEncontrados = contexto.Clientes.Where(p => p.NomUsuario == nomUsuario).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(cedula))
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IdCedula == cedula).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Clientes.Where(p => p.IdCedula == cedula).ToList();
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
                        datosEncontrados = contexto.Clientes.Where(p => p.IndEstado == estado).ToList();
                    }
                }
                if (sexo > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IndSexo == sexo).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Clientes.Where(p => p.IndSexo == sexo).ToList();
                    }
                }
                if (fechaNacimiento != null)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.FecNacimiento >= fechaNacimiento && p.FecNacimiento <= fechaNacimiento).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Clientes.Where(p => p.FecNacimiento >= fechaNacimiento && p.FecNacimiento <= fechaNacimiento).ToList();
                    }
                }
                if (sexo > 0)
                {
                    if (datosEncontrados.Count > 0)
                    {
                        datosEncontrados = datosEncontrados.Where(p => p.IndSexo == sexo).ToList();
                    }
                    else
                    {
                        datosEncontrados = contexto.Clientes.Where(p => p.IndSexo == sexo).ToList();
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

        public RespuestaDTO FiltradoClientesParametrosAnonimos(List<Cliente> datosEncontrados, string nombreParametro, object valorParametro)
        {
            try
            {
                if (datosEncontrados.Count > 0)
                {
                    switch (nombreParametro)
                    {
                        case "nombre":
                            datosEncontrados = datosEncontrados.Where(p => p.NomCliente.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "email":
                            datosEncontrados = datosEncontrados.Where(p => p.EmlCorreo.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "nomUsuario":
                            datosEncontrados = datosEncontrados.Where(p => p.NomUsuario.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "cedula":
                            datosEncontrados = datosEncontrados.Where(p => p.IdCedula.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "estado":
                            datosEncontrados = datosEncontrados.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "sexo":
                            datosEncontrados = datosEncontrados.Where(p => p.IndSexo == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "fechaNacimiento":
                            datosEncontrados = datosEncontrados.Where(p => p.FecNacimiento >= Convert.ToDateTime(valorParametro)).ToList();
                            break; 
                        case "telefono":
                            datosEncontrados = datosEncontrados.Where(p => p.TelCliente.Contains(valorParametro.ToString())).ToList();
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
                            datosEncontrados = contexto.Clientes.Where(p => p.NomCliente.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "email":
                            datosEncontrados = contexto.Clientes.Where(p => p.EmlCorreo.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "nomUsuario":
                            datosEncontrados = contexto.Clientes.Where(p => p.NomUsuario.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "cedula":
                            datosEncontrados = contexto.Clientes.Where(p => p.IdCedula.Contains(valorParametro.ToString())).ToList();
                            break;
                        case "estado":
                            datosEncontrados = contexto.Clientes.Where(p => p.IndEstado == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "sexo":
                            datosEncontrados = contexto.Clientes.Where(p => p.IndSexo == Convert.ToUInt32(valorParametro)).ToList();
                            break;
                        case "fechaNacimiento":
                            datosEncontrados = contexto.Clientes.Where(p => p.FecNacimiento >= Convert.ToDateTime(valorParametro)).ToList();
                            break;
                        case "telefono":
                            datosEncontrados = contexto.Clientes.Where(p => p.TelCliente.Contains(valorParametro.ToString())).ToList();
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
