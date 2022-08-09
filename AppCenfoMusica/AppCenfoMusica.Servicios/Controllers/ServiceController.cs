using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AppCenfoMusica.DTO;
using AppCenfoMusica.Logica;

namespace AppCenfoMusica.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        //Get: api/Service/ListaProductos
        [HttpGet("ListaProductos")]
        public IEnumerable<BaseDTO> ListaProductos()
        {
            var resultado = new ProductoLogica().ListaProductos();

            if (resultado.ElementAt(0).GetType() != typeof(ErrorDTO))
            {
                return resultado.OfType<ProductoDTO>();
            }
            else
            {
                return resultado.OfType<ErrorDTO>();
            }
        }

        //Get: api/Service/BuscarProductoPorId/{id}
        [HttpGet("BuscarProductoPorId/{id}")]
        public BaseDTO BuscarProductoPorId(int id)
        {
            var resultado = new ProductoLogica().BuscarProductoPorId(id);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ProductoDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }

        //Get: api/Service/AgregarProducto/{id}
        [HttpPost("AgregarProducto")]
        public BaseDTO AgregarProducto(ProductoDTO producto)
        {
            var resultado = new ProductoLogica().AgregarProducto(producto);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ProductoDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
        //Get: api/Service/AgregarProducto/{id}
        [HttpPost("AgregarProductoParametros")]
        public BaseDTO AgregarProductoParametros(string nombre, int tipo, int cantidad, decimal precio)
        {
            var resultado = new ProductoLogica().AgregarProducto(nombre, tipo, cantidad, precio);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ProductoDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
    }
}
