using AppCenfoMusica.DTO;
using AppCenfoMusica.Logica;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppCenfoMusica.Web.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult BuscarProductoPorId(int id)
        {
            GestionProductosVM model = new GestionProductosVM();

            var resultado = new ProductoLogica().BuscarProductoPorId(id);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                //Respuesta positiva
                model.Producto = (ProductoDTO)resultado;
            }
            else
            {
                //Respuesta negativa
                model.Error = (ErrorDTO)resultado;
            }
            return View(model);
        }

        public ActionResult ListarProductos()
        {
            GestionProductosVM model = new GestionProductosVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/ListaProductos";

            // Consulta al servicio
            var webrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url); 

            string datos = "";

            //Se almacena la respuesta a la consulta
            using (var respuesta = webrequest.GetResponse()) 
            {
                //Se determina un lector general
                using (var reader = new StreamReader(respuesta.GetResponseStream())) 
                {
                    var resultadoLecutra = reader.ReadToEnd();
                    datos = resultadoLecutra.ToString();
                }
            }

            var resultado = JsonConvert.DeserializeObject<List<BaseDTO>>(datos);

            if (resultado.ElementAt(0).IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoProducto = JsonConvert.DeserializeObject<List<ProductoDTO>>(datos);

                model.ListaProductos = resultadoProducto;

                var valorTotalProductos = model.ListaProductos.Sum(P => P.PrecioUnitario);

                var totalProductosBodega = model.ListaProductos.Sum(P => P.CantidadBodega);

                ViewBag.PreciosTotales = valorTotalProductos;

                ViewData["TotalProductos"] = totalProductosBodega;
            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
