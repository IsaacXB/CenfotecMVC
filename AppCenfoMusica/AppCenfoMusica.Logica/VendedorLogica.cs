using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos;
using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.Datos.Helpers;
using AppCenfoMusica.DTO;


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
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
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
                return new ErrorDTO
                {
                    CodigoError = -1,
                    MensajeError = error.Message
                };
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
