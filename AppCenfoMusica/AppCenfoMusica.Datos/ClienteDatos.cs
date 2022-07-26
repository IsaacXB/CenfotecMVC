using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos.CenfomusicaModel;
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

        #endregion
    }
}
