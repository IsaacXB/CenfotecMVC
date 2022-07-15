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

        public RespuestaDTO AgregarProducto(RespuestaDTO producto)
        {
            try
            {
                if (producto.Contenido == null) throw new Exception("No se logró realizar el guardado de datos solicitado.");

                contexto.Productos.Add((Producto)producto.Contenido);

                if (contexto.SaveChanges() > 0)
                {
                    return new RespuestaDTO { Codigo = 1, Contenido = producto };
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

        public RespuestaDTO ActualizarPrecioProducto(int codigoProducto, decimal precioProducto)
        {
            try
            {
                var producto = contexto.Productos.FirstOrDefault(x => x.Pkproducto ==  codigoProducto);

                if (producto != null)
                {
                    producto.MtoPrecio = precioProducto;

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO { Codigo = 1, Contenido = producto };
                    }
                }
                throw new Exception("No se pudo actualizar el producto especificado.");
           
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

        public RespuestaDTO ActualizarPrecioProductoAlterno(int codigoProducto, decimal precioProducto)
        {
            try
            {
                var respuesta = BuscarProductoPorIdDTOValidacion(codigoProducto);

                if (respuesta == null || respuesta.Contenido == null) throw new Exception("No se pudo actualizar el producto especificado.");

                if (respuesta.Codigo > 0 && respuesta.Contenido.GetType() != typeof(ErrorDTO))
                {
                    ((Producto)respuesta.Contenido).MtoPrecio = precioProducto;

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO { Codigo = 1, Contenido = producto };
                    }
                }
                throw new Exception("No se pudo actualizar el producto especificado.");

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

        public RespuestaDTO ActualizarCantidadProductoSegunRangoPreciosTipo(decimal precioInicial, decimal precioFinal, int tipo)
        {
            try
            {
                var productos = contexto.Productos.Where(P => (P.MtoPrecio >= precioInicial && P.MtoPrecio <= precioFinal) && P.TipProducto == tipo).ToList();

                if (productos.Count > 0)
                {
                    //contexto.RemoveRange(productos);

                    foreach (var item in productos)
                    {
                        item.CantProducto = 100;
                        //contexto.Productos.Remove(item);
                    }

                    if (contexto.SaveChanges() == productos.Count())
                    {
                        return new RespuestaDTO
                        {
                            Codigo = 1,
                            Contenido = productos // Dentro de este objeto, que se retorna posterior al SaveChanges, ya se tiene actualizado el valor del PK
                        };
                    }
                }

                throw new Exception("No se encontraron productos con los parámetros establecidos");
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

        #region Eliminacion

        public RespuestaDTO EliminarProducto(int idProducto)
        {
            try
            {
                var respuesta = BuscarProductoPorIdDTOValidacion(idProducto);

                if (respuesta == null || respuesta.Contenido == null) throw new Exception("No se pudo actualizar el producto especificado.");


                if (respuesta.Contenido.GetType() != typeof(ErrorDTO))
                {
                    contexto.Productos.Remove(((Producto)respuesta.Contenido));
                    //producto.MtoPrecio = precioProducto;

                    if (contexto.SaveChanges() > 0)
                    {
                        return new RespuestaDTO
                        {
                            Codigo = 1,
                            Contenido = ((Producto)respuesta.Contenido) // Dentro de este objeto, que se retorna posterior al SaveChanges, ya se tiene actualizado el valor del PK
                        };
                    }
                }

                throw new Exception("No se pudo actualizar el producto especificado");
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
