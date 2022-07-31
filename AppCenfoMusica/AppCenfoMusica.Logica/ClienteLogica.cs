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

        #endregion
    }
}
