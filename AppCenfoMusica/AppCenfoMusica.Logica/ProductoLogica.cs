using AppCenfoMusica.Datos.CenfomusicaModel;
using AppCenfoMusica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.Datos;


namespace AppCenfoMusica.Logica
{
    public class ProductoLogica
    {
        #region Variables
        CenfomusicaContext contexto;
        #endregion

        #region Constructor 
        public ProductoLogica()
        {
            this.contexto = new CenfomusicaContext();
        }
        #endregion

        #region Metodos 
        
        #region Traductores

        internal static ProductoDTO ConvertirEntidadProductoADTO(Producto producto)
        {
            return new ProductoDTO
            {
                CantidadBodega = Convert.ToInt32(producto.CantProducto),
                IdEntidad = producto.Pkproducto,
                Nombre = producto.NomProducto,
                PrecioUnitario = Convert.ToDecimal(producto.MtoPrecio),
                TipoProducto = Convert.ToInt32(producto.TipProducto)
            };
        }

        internal static Producto ConvertirProductoDTOAEntidad(Producto producto)
        {
            return new Producto
            {
                CantProducto = producto.CantProducto,
                MtoPrecio = producto.MtoPrecio,
                NomProducto = producto.NomProducto,
                TipProducto = producto.TipProducto
            };
        }
        #endregion

        #region Consultas

        public BaseDTO BuscarProductoPorId(int id)
        {
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var resultado = intermedio.BuscarProductoPorIdDTOValidacion(id);

                if (resultado != null && resultado.Codigo > 0 && resultado.Contenido != null)
                {
                    var producto =  (Producto)resultado.Contenido;
                    var respuesta = ConvertirEntidadProductoADTO(producto);

                    return respuesta;
                }
                else
                {
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (System.Exception error)
            {
                return new ErrorDTO { CodigoError = -1, MensajeError = error.Message };
            }
        }

        public List<BaseDTO> ListaProductos()
        {
            List<BaseDTO> respuesta = new List<BaseDTO>();
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var resultado = intermedio.ListarProductos();

                if (resultado.Codigo > 0)
                {
                    var productos = (List<Producto>)resultado.Contenido;
                    foreach (var item in productos)
                    {
                        var itemConvertido = ConvertirEntidadProductoADTO(item);
                        respuesta.Add(itemConvertido);
                    }
                }
                else
                {
                    var error = (ErrorDTO)resultado.Contenido;
                    respuesta.Add(error);
                    return respuesta;
                }
            }
            catch (System.Exception error)
            {
                respuesta.Clear();
                var errorCatch = new ErrorDTO { CodigoError = -1, MensajeError = error.Message };
                respuesta.Add(errorCatch);
                return respuesta;

            }
            return respuesta;
        }
        #endregion

        #region Almacenamiento

        public BaseDTO AgregarProducto(string nombre, int tipo, int cantidad, decimal precio)
        {
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var resultado = intermedio.AgregarProducto(nombre, tipo, cantidad, precio);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var producto = (Producto)resultado.Contenido;
                    var respuesta = ConvertirEntidadProductoADTO(producto);
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

        public BaseDTO AgregarProducto(ProductoDTO producto)
        {
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var productoParametro = new RespuestaDTO { Contenido = ConvertirProductoDTOAEntidad(producto) };

                var resultado = intermedio.AgregarProducto(productoParametro);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo
                    var productoResultado = (Producto)resultado.Contenido;
                    var respuesta = ConvertirEntidadProductoADTO(productoResultado);
                    return respuesta;
                }
                {
                    //Escritura con respuesta negativa
                    return (ErrorDTO)resultado.Contenido;
                }
            }
            catch (System.Exception error)
            {
                return new ErrorDTO { CodigoError = -1, MensajeError = error.Message };
            }
        }

        private object ConvertirProductoDTOAEntidad(ProductoDTO producto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Actualizaciones 

        public BaseDTO ActualizarPrecioProducto(int codigoProducto, decimal precioProducto)
        {
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var resultado = intermedio.ActualizarPrecioProducto(codigoProducto, precioProducto);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var producto = (Producto)resultado.Contenido;
                    var respuesta = ConvertirEntidadProductoADTO(producto);
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

        public BaseDTO ActualizarPrecioProducto(ProductoDTO productoCliente)
        {
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var resultado = intermedio.ActualizarPrecioProducto(productoCliente.IdEntidad, productoCliente.PrecioUnitario);

                if (resultado.Codigo > 0)
                {
                    //Escritura con respuesta positivo

                    var producto = (Producto)resultado.Contenido;
                    var respuesta = ConvertirEntidadProductoADTO(producto);
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

        public BaseDTO EliminarProducto(ProductoDTO productoCliente)
        {
            try
            {
                ProductoDatos intermedio = new ProductoDatos(contexto);

                var resultado = intermedio.EliminarProducto(productoCliente.IdEntidad);

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

        #endregion
    }
}
