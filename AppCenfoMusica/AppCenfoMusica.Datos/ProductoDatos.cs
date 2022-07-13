using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenfoMusica.DTO;
using AppCenfoMusica.Datos.CenfomusicaModel;

namespace AppCenfoMusica.Datos
{
    public class ProductoDatos
    {
        #region Constructor     
        public ProductoDatos(CenfomusicaContext contextoGlobal)
        {
            contexto = contextoGlobal;
        }
        #endregion

        #region Variables  

        CenfomusicaContext contexto = new CenfomusicaContext();

        #endregion

        #region Metodos

        #region Consultas
        public Producto? BuscarProductoPorId(int id)
        {
            var producto = contexto.Productos.FirstOrDefault(x => x.Pkproducto == id);

            return producto;

        }
        public RespuestaDTO BuscarProductoPorIdDTO(int id)
        {
            var producto = contexto.Productos.FirstOrDefault(x => x.Pkproducto == id);
            return new RespuestaDTO {
                Codigo = 1,
                Contenido = producto
            };

        }
        public RespuestaDTO? BuscarProductoPorIdDTOValidacion(int id)
        {
            try
            {
                var producto = contexto.Productos.FirstOrDefault(x => x.Pkproducto == id);

                if (producto != null)
                {
                    return new RespuestaDTO {
                        Codigo = 1,
                        Contenido = producto
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
        public RespuestaDTO? BuscarProductosPorTipo(int id)
        {
            try
            {
                var productos = contexto.Productos.Where(x => x.TipProducto == id).ToList();

                if (productos.Count > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = productos
                    };
                }
                else
                {
                    throw new Exception("No se logró encontrar ningún producto con el código especificado");
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
        public RespuestaDTO ListarProductos()
        {
            try
            {
                var productos = contexto.Productos.ToList();
                if (productos.Count > 0)
                {
                    return new RespuestaDTO {
                        Codigo = 1, 
                        Contenido = productos
                    };
                }
                else
                {
                    throw new Exception("No se encontraron productos en este momento.");
                }
            }
            catch (System.Exception error)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO { 
                        MensajeError = error.Message 
                    }
                };
            }
        }
        #endregion

        #region Inserciones
        public RespuestaDTO AgregarProducto(string nombre, int tipo, int cantidad, decimal precio)
        {
            try
            {
                var producto = new Producto
                {
                    NomProducto = nombre,
                    TipProducto = tipo,
                    CantProducto = cantidad,
                    MtoPrecio = precio
                };

                contexto.Productos.Add(producto);
                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = producto
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }
                

                return new RespuestaDTO { 
                    Codigo = 1, 
                    Contenido = producto 
                };
            }
            catch (Exception error)
            {
                return new RespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new ErrorDTO { 
                        MensajeError = error.Message 
                    }
                };
            }
        }

        public RespuestaDTO AgregarProducto(Producto producto)
        {
            try
            {
                contexto.Productos.Add(producto);
                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = producto
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }


                return new RespuestaDTO
                {
                    Codigo = 1,
                    Contenido = producto
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

        public RespuestaDTO AgregarProducto(ProductoDTO producto)
        {
            try
            {
                contexto.Productos.Add(new Producto { 
                    NomProducto = producto.Nombre,
                    CantProducto = producto.CantidadBodega,
                    MtoPrecio = producto.PrecioUnitaro,
                    TipProducto = producto.TipoProducto
                });

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = producto
                    };
                }
                else
                {
                    throw new Exception("No se logró realizar el guardado de datos solicitado.");
                }

                return new RespuestaDTO
                {
                    Codigo = 1,
                    Contenido = producto
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
        #endregion

        #endregion

    }
}
