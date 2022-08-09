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
    public class ClienteLogica
    {
        #region Variables

        CenfomusicaContext contexto;

        #endregion

        #region Constructor

        public ClienteLogica()
        {
            contexto = new CenfomusicaContext();
        }

        #endregion

        #region Métodos

        #region Traductores

        //private => Son visibles dentro de la misma clase
        //public => Son visibles para todo el mundo, entre clases, entre capas, entre proyectos.
        //protected => Permite acceder a elementos base, pero permite acceder a elementos hijos.
        //internal => Son visibles, para todo el mundo, DENTRO DE LA MISMA CAPA
        internal static ClienteDTO ConvertirEntidadClienteADTO(Cliente cliente)
        {
            return new ClienteDTO
            {
                IdEntidad = cliente.Pkcliente,
                Nombre = cliente.NomCliente,
                Cedula = cliente.IdCedula,
                NombreUsuario = cliente.NomUsuario,
                Contrasena = cliente.IndContrasena,
                Estado = (int)cliente.IndEstado,
                FechaNacimiento = (DateTime)cliente.FecNacimiento,
                Sexo = (int)cliente.IndSexo,
                Telefono = cliente.TelCliente
            };
        }

        internal static Cliente ConvertirDTOClienteAEntidad(ClienteDTO cliente)
        {
            return new Cliente
            {
                Pkcliente = cliente.IdEntidad,
                NomCliente = cliente.Nombre,
                NomUsuario = cliente.NombreUsuario,
                IdCedula = cliente.Cedula,
                IndContrasena = cliente.Contrasena,
                IndEstado = cliente.Estado,
                FecNacimiento = cliente.FechaNacimiento,
                IndSexo = cliente.Sexo,
                TelCliente = cliente.Telefono
            };
        }

        #endregion

        #region Consultas

        public BaseDTO BuscarClientePorID(int id)
        {
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.BuscarClientePorIdDTOValidacion(id);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var cliente = (Cliente)resultado.Contenido;
                    var respuesta = ConvertirEntidadClienteADTO(cliente);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
            }
        }

        public List<BaseDTO> ListaClientes()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.ListarClientes();

                if (resultado.Codigo > 0)
                {
                    //Respuesta positiva
                    var clientes = (List<Cliente>)resultado.Contenido;

                    foreach (var item in clientes)
                    {
                        var itemConvertido = ConvertirEntidadClienteADTO(item);
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

        #region Almacenamiento

        public BaseDTO AgregarCliente(string nombre, string email, string nomUsuario, string contrasena, 
            string cedula, int estado, int sexo, DateTime fechaNacimiento, string telefono)
        {
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.AgregarCliente(nombre, email, nomUsuario, contrasena, cedula, estado,sexo,fechaNacimiento, telefono);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var cliente = (Cliente)resultado.Contenido;
                    var respuesta = ConvertirEntidadClienteADTO(cliente);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
            }
        }

        public BaseDTO AgregarCliente(ClienteDTO cliente)
        {
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var clienteParametro = new RespuestaDTO
                {
                    Contenido = ConvertirDTOClienteAEntidad(cliente)
                };

                var resultado = intermedio.AgregarCliente(clienteParametro);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var clienteResultado = (Cliente)resultado.Contenido;
                    var respuesta = ConvertirEntidadClienteADTO(clienteResultado);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
            }
        }

        #endregion

        #region Actualizaciones 

        public BaseDTO ActualizarCliente(int codigoCliente,string nombre, string email, string nomUsuario, string contrasena,
            string cedula, int estado, int sexo, DateTime fechaNacimiento, string telefono)
        {
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.ActualizarCliente(codigoCliente, nombre, email, nomUsuario, contrasena, cedula, estado, sexo, fechaNacimiento, telefono);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var cliente = (Cliente)resultado.Contenido;
                    var respuesta = ConvertirEntidadClienteADTO(cliente);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
            }
        }

        public BaseDTO ActualizarCliente(ClienteDTO clienteDTO)
        {
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.ActualizarCliente(clienteDTO.IdEntidad, clienteDTO.Nombre, clienteDTO.Email, clienteDTO.NombreUsuario, clienteDTO.Contrasena, clienteDTO.Cedula, clienteDTO.Estado, clienteDTO.Sexo, clienteDTO.FechaNacimiento, clienteDTO.Telefono);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var cliente = (Cliente)resultado.Contenido;
                    var respuesta = ConvertirEntidadClienteADTO(cliente);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
            }
        }

        #endregion

        #region Eliminación

        public BaseDTO EliminarCliente(ClienteDTO cliente)
        {
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.EliminarCliente(cliente.IdEntidad);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo
                    return (BaseDTO)resultado.Contenido;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
            }
        }

        #endregion

        #region Filtrado

        public List<BaseDTO> FiltradoClientesParametrosSolidos(string nombre, string email, string nomUsuario,
            string cedula, int estado, int sexo, DateTime fechaNacimiento, string telefono)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var resultado = intermedio.FiltradoClientesParametrosSolidos(nombre, email, nomUsuario, cedula, estado, sexo, fechaNacimiento, telefono);

                if (resultado.Codigo > 0)
                {
                    //Resultado positivo
                    var clientes = (List<Cliente>)resultado.Contenido;

                    foreach (var item in clientes)
                    {
                        var itemConvertido = ConvertirEntidadClienteADTO(item);
                        respuesta.Add(itemConvertido);
                    }

                    return respuesta;

                }
                else
                {
                    //Resultado negativo
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

        public List<BaseDTO> FiltradoClientesParametrosAnonimos(string nombre, string email, string nomUsuario, string cedula, string estado, string sexo, DateTime fechaNacimiento, string telefono)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                ClienteDatos intermedio = new ClienteDatos(contexto);

                var datosPrevios = new List<Cliente>();

                if (nombre != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "nombre", nombre);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }

                if (email != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "email", email);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }
                if (nomUsuario != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "nomUsuario", nomUsuario);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }
                if (cedula != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "cedula", cedula);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }
                if (estado != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "estado", estado);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }
                if (sexo != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "sexo", sexo);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }
                if (telefono != null)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "telefono", telefono);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }
                if (fechaNacimiento.Minute > 0)
                {
                    var resultado = intermedio.FiltradoClientesParametrosAnonimos(datosPrevios, "fechaNacimiento", fechaNacimiento);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Cliente>)resultado.Contenido;

                        if (datosPrevios.Count < 1)
                        {
                            throw new Exception("No se encontraron datos relacionados a uno o más parámetros de la búsqueda");
                        }
                    }
                    else
                    {
                        return ErrorFiltradoDatos(respuesta, resultado);
                    }
                }


                foreach (var item in datosPrevios)
                {
                    var itemConvertido = ConvertirEntidadClienteADTO(item);
                    respuesta.Add(itemConvertido);
                }

                return respuesta;

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

        private static List<BaseDTO> ErrorFiltradoDatos(List<BaseDTO> respuesta, RespuestaDTO resultado)
        {
            var error = (ErrorDTO)resultado.Contenido;
            respuesta.Add(error);
            return respuesta;
        }
        #endregion

        #endregion
    }
}
