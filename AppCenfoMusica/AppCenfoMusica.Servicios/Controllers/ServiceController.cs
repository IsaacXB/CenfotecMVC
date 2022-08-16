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
        #region Productos
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

        //Get: api/Service/AgregarProducto/
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

        [HttpPost("ActualizarPrecioProducto")]
        public BaseDTO ActualizarPrecioProducto(ProductoDTO producto)
        {
            var resultado = new ProductoLogica().ActualizarPrecioProducto(producto.IdEntidad, producto.PrecioUnitario);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ProductoDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
        #endregion

        #region Clientes

        //Get: api/Service/ListarClientes
        [HttpGet("ListarClientes")]
        public IEnumerable<BaseDTO> ListarClientes()
        {
            var resultado = new ClienteLogica().ListaClientes();

            if (resultado.ElementAt(0).GetType() != typeof(ErrorDTO))
            {
                return resultado.OfType<ClienteDTO>();
            }
            else
            {
                return resultado.OfType<ErrorDTO>();
            }
        }

        //Get: api/Service/BuscarClientePorId/{id}
        [HttpGet("BuscarClientePorId/{id}")]
        public BaseDTO BuscarClientePorId(int id)
        {
            var resultado = new ClienteLogica().BuscarClientePorID(id);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ClienteDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }

        //Get: api/Service/AgregarCliente/
        [HttpPost("AgregarCliente")]
        public BaseDTO AgregarCliente(ClienteDTO cliente)
        {
            var resultado = new ClienteLogica().AgregarCliente(cliente);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ClienteDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
        //Get: api/Service/AgregarClienteParametros/
        [HttpPost("AgregarClienteParametros")]
        public BaseDTO AgregarClienteParametros(string nombre, string email, string nomUsuario, string contrasena,
            string cedula, int estado, int sexo, DateTime fechaNacimiento, string telefono)
        {
            var resultado = new ClienteLogica().AgregarCliente(nombre, email, nomUsuario, contrasena, cedula, estado, sexo, fechaNacimiento, telefono);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ClienteDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }

        [HttpPost("ActualizarCliente")]
        public BaseDTO ActualizarPrecioProducto(int codigoCliente,string nombre, string email, string nomUsuario, string contrasena,
            string cedula, int estado, int sexo, DateTime fechaNacimiento, string telefono)
        {
            var resultado = new ClienteLogica().ActualizarCliente(codigoCliente,nombre, email, nomUsuario, contrasena, cedula, estado, sexo, fechaNacimiento, telefono);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (ClienteDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
        #endregion

        #region Vendedores

        //Get: api/Service/ListarVendedores
        [HttpGet("ListarVendedores")]
        public IEnumerable<BaseDTO> ListarVendedores()
        {
            var resultado = new VendedorLogica().ListaVendedores();

            if (resultado.ElementAt(0).GetType() != typeof(ErrorDTO))
            {
                return resultado.OfType<VendedorDTO>();
            }
            else
            {
                return resultado.OfType<ErrorDTO>();
            }
        }

        //Get: api/Service/BuscarVendedorPorId/{id}
        [HttpGet("BuscarVendedorPorId/{id}")]
        public BaseDTO BuscarVendedorPorId(int id)
        {
            var resultado = new VendedorLogica().BuscarVendedorPorID(id);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (VendedorDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }

        //Get: api/Service/AgregarVendedor/
        [HttpPost("AgregarVendedor")]
        public BaseDTO AgregarVendedor(VendedorDTO vendedor)
        {
            var resultado = new VendedorLogica().AgregarVendedor(vendedor);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (VendedorDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
        //Get: api/Service/AgregarVendedorParametros/
        [HttpPost("AgregarVendedorParametros")]
        public BaseDTO AgregarVendedorParametros(string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            var resultado = new VendedorLogica().AgregarVendedor(nombre, puesto, nomUsuario, contrasena, cedula, estado);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (VendedorDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }

        [HttpPost("ActualizarVendedor")]
        public BaseDTO ActualizarVendedor(int codigoVendedor, string nombre, int puesto, string nomUsuario, string contrasena, string cedula, int estado)
        {
            var resultado = new VendedorLogica().ActualizarVendedor(codigoVendedor, nombre, puesto, nomUsuario, contrasena, cedula, estado);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                return (VendedorDTO)resultado;
            }
            else
            {
                return (ErrorDTO)resultado;
            }
        }
        #endregion

    }
}
