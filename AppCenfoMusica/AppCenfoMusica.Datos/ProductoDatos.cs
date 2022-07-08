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


        public Producto? BuscarProductoPorId(int id)
        {
            var producto = contexto.Productos.FirstOrDefault(x => x.Pkproducto == id);

            return producto;

        }
        public RespuestaDTO BuscarProductoPorIdDTO(int id)
        {
            var producto = contexto.Productos.FirstOrDefault(x => x.Pkproducto == id);
            return new RespuestaDTO
            {
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
                    return new RespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = producto
                    };
                }

            }
            catch (System.Exception error)
            {
                return new RespuestaDTO { Contenido = error.Message, Codigo = -1};  
            }
            return null;

        }

        #endregion

    }
}
