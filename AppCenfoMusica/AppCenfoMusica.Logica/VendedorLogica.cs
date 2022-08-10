using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.Datos.Helpers;
using AppCenfoMusica.DTO;
using AppCenfoMusica.Logica.Helpers;

namespace AppCenfoMusica.Logica
{
    public class VendedorLogica
    {
        #region Variables

        CenfomusicaContext contexto;

        #endregion

        #region Constructor

        public VendedorLogica()
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
        internal static VendedorDTO ConvertirEntidadVendedorADTO(Vendedor vendedor)
        {
            return new VendedorDTO
            {
                IdEntidad = vendedor.Pkvendedor,
                Nombre = vendedor.NomVendedor,
                Cedula = vendedor.IndCedula,
                NombreUsuario = vendedor.NomUsuario,
                Contrasena = vendedor.IndContrasena,
                Estado = (int)vendedor.IndEstado,
                Puesto = (int)vendedor.IndPuesto
            };
        }

        internal static Vendedor ConvertirDTOVendedorAEntidad(VendedorDTO vendedor)
        {
            return new Vendedor
            {
                Pkvendedor = vendedor.IdEntidad,
                NomVendedor = vendedor.Nombre,
                NomUsuario = vendedor.NombreUsuario,
                IndCedula = vendedor.Cedula,
                IndContrasena = vendedor.Contrasena,
                IndEstado = vendedor.Estado,
                IndPuesto = vendedor.Puesto
            };
        }

        #endregion

        #region Consultas

        public BaseDTO BuscarVendedorPorID(int id)
        {
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.BuscarVendedorPorIdDTOValidacion(id);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var vendedor = (Vendedor)resultado.Contenido;
                    var respuesta = ConvertirEntidadVendedorADTO(vendedor);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return ControladorRetornosLogica.ControladorErrores(error);

                //return new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
            }
        }

        public List<BaseDTO> ListaVendedores()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.ListarVendedores();

                if (resultado.Codigo > 0)
                {
                    //Respuesta positiva
                    var vendedores = (List<Vendedor>)resultado.Contenido;

                    foreach (var item in vendedores)
                    {
                        var itemConvertido = ConvertirEntidadVendedorADTO(item);
                        respuesta.Add(itemConvertido);
                        //respuesta.Add(ConvertirEntidadProductoADTO(item));
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
                return ControladorRetornosLogica.ControladorErroresLista(error);

                //respuesta.Clear();
                //var errorCatch = new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
                //respuesta.Add(errorCatch);

                //return respuesta;
            }
        }

        #endregion

        #region Almacenamiento

        public BaseDTO AgregarVendedor(string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.AgregarVendedor(nombre, puesto, nomUsuario, contrasena, cedula, estado);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var vendedor = (Vendedor)resultado.Contenido;
                    var respuesta = ConvertirEntidadVendedorADTO(vendedor);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return ControladorRetornosLogica.ControladorErrores(error);

                //return new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
            }
        }

        public BaseDTO AgregarVendedor(VendedorDTO vendedor)
        {
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var vendedorParametro = new RespuestaDTO
                {
                    Contenido = ConvertirDTOVendedorAEntidad(vendedor)
                };

                var resultado = intermedio.AgregarVendedor(vendedorParametro);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var vendedorResultado = (Vendedor)resultado.Contenido;
                    var respuesta = ConvertirEntidadVendedorADTO(vendedorResultado);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return ControladorRetornosLogica.ControladorErrores(error);

                //return new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
            }
        }

        #endregion

        #region Actualizaciones 

        public BaseDTO ActualizarVendedor(int codigoVendedor, string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.ActualizarVendedor(codigoVendedor, nombre, puesto, nomUsuario, contrasena, cedula, estado);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var vendedor = (Vendedor)resultado.Contenido;
                    var respuesta = ConvertirEntidadVendedorADTO(vendedor);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return ControladorRetornosLogica.ControladorErrores(error);

                //return new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
            }
        }

        public BaseDTO ActualizarVendedor(VendedorDTO vendedorDTO)
        {
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.ActualizarVendedor(vendedorDTO.IdEntidad, vendedorDTO.Nombre, vendedorDTO.Puesto, vendedorDTO.NombreUsuario, vendedorDTO.Contrasena, vendedorDTO.Cedula, vendedorDTO.Estado);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var vendedor = (Vendedor)resultado.Contenido;
                    var respuesta = ConvertirEntidadVendedorADTO(vendedor);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (Exception error)
            {
                return ControladorRetornosLogica.ControladorErrores(error);

                //return new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
            }
        }

        #endregion

        #region Eliminación

        public BaseDTO EliminarVendedor(VendedorDTO vendedorDTO)
        {
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.EliminarVendedor(vendedorDTO.IdEntidad);

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
                return ControladorRetornosLogica.ControladorErrores(error);

                //return new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
            }
        }

        #endregion

        #region Filtrado

        public List<BaseDTO> FiltradoVendedoresParametrosSolidos( string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var resultado = intermedio.FiltradoVendedoresParametrosSolidos(nombre, puesto, nomUsuario, cedula, estado);

                if (resultado.Codigo > 0)
                {
                    //Resultado positivo
                    var vendedores = (List<Vendedor>)resultado.Contenido;

                    foreach (var item in vendedores)
                    {
                        var itemConvertido = ConvertirEntidadVendedorADTO(item);
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
                return ControladorRetornosLogica.ControladorErroresLista(error);

                //respuesta.Clear();
                //var errorCatch = new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
                //respuesta.Add(errorCatch);

                //return respuesta;
            }
        }

        public List<BaseDTO> FiltradoVendedoresParametrosAnonimos(string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                VendedorDatos intermedio = new VendedorDatos(contexto);

                var datosPrevios = new List<Vendedor>();

                if (nombre != null)
                {
                    var resultado = intermedio.FiltradoVendedoresParametrosAnonimos(datosPrevios, "nombre", nombre);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Vendedor>)resultado.Contenido;

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

                if (puesto != null)
                {
                    var resultado = intermedio.FiltradoVendedoresParametrosAnonimos(datosPrevios, "puesto", puesto);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Vendedor>)resultado.Contenido;

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
                    var resultado = intermedio.FiltradoVendedoresParametrosAnonimos(datosPrevios, "nomUsuario", nomUsuario);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Vendedor>)resultado.Contenido;

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
                    var resultado = intermedio.FiltradoVendedoresParametrosAnonimos(datosPrevios, "cedula", cedula);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Vendedor>)resultado.Contenido;

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
                    var resultado = intermedio.FiltradoVendedoresParametrosAnonimos(datosPrevios, "estado", estado);

                    if (resultado.Codigo > 0)
                    {
                        datosPrevios = (List<Vendedor>)resultado.Contenido;

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
                    var itemConvertido = ConvertirEntidadVendedorADTO(item);
                    respuesta.Add(itemConvertido);
                }

                return respuesta;

            }
            catch (Exception error)
            {
                return ControladorRetornosLogica.ControladorErroresLista (error);

                //respuesta.Clear();
                //var errorCatch = new ErrorDTO
                //{
                //    CodigoError = -1,
                //    MensajeError = error.Message
                //};
                //respuesta.Add(errorCatch);

                //return respuesta;
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
