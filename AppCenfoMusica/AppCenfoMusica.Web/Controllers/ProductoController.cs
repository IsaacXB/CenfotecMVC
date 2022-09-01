using AppCenfoMusica.DTO;
using AppCenfoMusica.Logica;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace AppCenfoMusica.Web.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult BuscarProductoPorId(int id, string? accion)
        {
            if (HttpContext.Session.GetString("UserName") == null ||
                (HttpContext.Session.GetString("UserType") != "Vendedor" &&
                HttpContext.Session.GetString("UserType") != "Cliente"))
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }
            GestionProductosVM model = new GestionProductosVM();

            ViewBag.ProductosBodega = HttpContext.Session.GetString("TotalProductos");

            var resultado = new ProductoLogica().BuscarProductoPorId(id);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                //Respuesta positiva
                model.Producto = (ProductoDTO)resultado;
                if (accion == "guardar")
                {
                    ViewBag.Accion = "El vendedor se almacenó correctamente";
                }
            }
            else
            {
                //Respuesta negativa
                model.Error = (ErrorDTO)resultado;
            }
            return View(model);
        }

        public ActionResult ProductosAdicionales()
        {
            GestionProductosVM model = new GestionProductosVM();

            //Tenemos que definir la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/ListaProductos";

            var webrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url); // Nos genera la consulta al servicio

            string datos = "";

            //Para poder consumir esa consulta, necesitamos un espacio en memoria que nos permita almacenar la respuesta a esa consulta

            using (var respuesta = webrequest.GetResponse()) //Aquí estamos almacenando la respuesta a la consulta (simplemente almacenamiento)
            {
                using (var reader = new StreamReader(respuesta.GetResponseStream())) //Aquí lo que estamos haciendo es determinando un lector general que nos va a permitir
                                                                                     // entender cualquier tipo de respuesta, independientemente de la herramienta / entorno
                                                                                     // utilizado para construirla
                {
                    var resultadoLecutra = reader.ReadToEnd();
                    datos = resultadoLecutra.ToString();
                }
            }

            JsonSerializerSettings configuracion = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var resultado = JsonConvert.DeserializeObject<List<ProductoDTO>>(datos, configuracion);

            //if(resultado.ElementAt(0).IdEntidad > 0)
            if (resultado.ElementAt(0).GetType() != null)
            {

                model.ListaProductos = resultado.OfType<ProductoDTO>().ToList();

                ViewBag.PreciosTotales = HttpContext.Session.GetString("PreciosTotales");

                ViewData["TotalProductos"] = HttpContext.Session.GetString("TotalProductos");
            }
            else
            {
               // model.Error = (ErrorDTO)resultado.ElementAt(0);
            }



            return View(model);
        }

        public ActionResult ListarProductos(string? ordenamiento, int? paginaActual)
        {
            if (HttpContext.Session.GetString("UserName") == null ||
                (HttpContext.Session.GetString("UserType") != "Vendedor" &&
                HttpContext.Session.GetString("UserType") != "Cliente"))
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            int pagina = paginaActual ?? 1;

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

            JsonSerializerSettings configuracion = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var resultado = JsonConvert.DeserializeObject<List<BaseDTO>>(datos, configuracion);

            //if (resultado.ElementAt(0).IdEntidad > 0)
            if (resultado.ElementAt(0).GetType() != typeof(ErrorDTO))
            {
                //respuesta positiva
                var resultadoProducto = JsonConvert.DeserializeObject<List<ProductoDTO>>(datos);

                //model.ListaProductos = resultadoProducto;

                model.ListaProductos = resultadoProducto.OfType<ProductoDTO>().ToList();

                if (ordenamiento != null)
                {
                    switch (ordenamiento)
                    {
                        case "Nombre":
                            model.ListaProductos = model.ListaProductos.OrderBy(O => O.Nombre).ToList();
                            break;
                        case "Tipo":
                            model.ListaProductos = model.ListaProductos.OrderBy(O => O.TipoProducto).ToList();
                            break;
                        default:
                            break;
                    }
                }

                ViewBag.TotalProductos = model.ListaProductos.Count;

                if (ViewBag.TotalProductos > 10)
                {
                    ViewBag.TotalPaginas = (int)Math.Ceiling((double)ViewBag.totalProductos / 10);

                    if (pagina < ViewBag.TotalPaginas)
                    {
                        model.ListaProductos = model.ListaProductos.GetRange((pagina - 1) * 10, 10);
                    }
                    else
                    {
                        model.ListaProductos = model.ListaProductos.GetRange((pagina - 1) * 10, ViewBag.TotalProductos - ((pagina - 1) * 10));
                    }
                }

                var valorTotalProductos = model.ListaProductos.Sum(P => P.PrecioUnitario);

                var totalProductosBodega = model.ListaProductos.Sum(P => P.CantidadBodega);

                HttpContext.Session.SetString("PreciosTotales", "La sumatoria total de precios en la tienda es de: " + valorTotalProductos);
                HttpContext.Session.SetString("TotalProductos", "El total de productos en bodega es de: " + totalProductosBodega);

                //HttpContext.Session.SetString("CantidadProductos", "Se encontraron " + totalProductosBodega + " productos en la base de datos");
                //HttpContext.Session.SetString("RangoPrecios", "El total de precios es " + valorTotalProductos );


                ViewBag.PreciosTotales = valorTotalProductos;

                ViewData["TotalProductos"] = totalProductosBodega;

            }
            else
            {
                //respuesta negativa
                //var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultado.ElementAt(0);
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET: Únicamente nos va a mostrar el View (a menos que necesitemos configurar alguna cuestión adicional)
        public ActionResult AgregarProducto()
        {
            if (HttpContext.Session.GetString("UserName") == null ||
                (HttpContext.Session.GetString("UserType") != "Vendedor" &&
                HttpContext.Session.GetString("UserType") != "Cliente"))
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }
            ViewBag.PreciosTotales = HttpContext.Session.GetString("PreciosTotales");

            ViewData["TotalProductos"] = HttpContext.Session.GetString("TotalProductos");
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProducto(ProductoDTO model)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                    var tarea = cliente.PostAsJsonAsync<ProductoDTO>("AgregarProducto", model);

                    tarea.Wait();

                    var resultado = tarea.Result;

                    var datos = resultado.Content.ReadAsStringAsync().Result;

                    if (!datos.Contains("Error"))
                    {
                        var respuesta = JsonConvert.DeserializeObject<ProductoDTO>(datos);

                        return RedirectToAction("BuscarProductoPorID", new { id = respuesta.IdEntidad, accion = "guardar" });
                        //return Content("<div><h4>¡Operación exitosa!</h4><br /><div>Se insertó el nuevo producto con el nombre" + respuesta.Nombre + "</div></div>","text/html");
                    }
                    else
                    {
                        var respuesta = JsonConvert.DeserializeObject<ErrorDTO>(datos);
                        ModelState.AddModelError("ErrorProgramacion", respuesta.MensajeError);
                        throw new Exception("ErrorProgramacion");
                    }
                }
            }
            catch (Exception error)
            {
                if (error.Message == "ErrorProgramacion")
                {
                    return View();
                }
                else
                {
                    ModelState.AddModelError("ErrorSistema", "Ocurrió un error inesperado, favor ponerse en contacto con el personal autorizado");
                    //almacenamiento en bitácroa que guarde efectivamente el texto del error
                    return View();
                }
            }
        }

        private void IncluirProductoCarrito(GestionProductosVM model, int contadorProductos)
        {
            HttpContext.Session.SetInt32("CantidadProductos", Convert.ToInt32(contadorProductos));
            HttpContext.Session.SetInt32("Producto" + contadorProductos, model.Producto.IdEntidad);
            HttpContext.Session.SetInt32("CantidadProducto" + contadorProductos, model.CantidadXProducto);
            HttpContext.Session.SetString("CantidadProductosCarrito", "Hay " + HttpContext.Session.GetInt32("CantidadProductos") + " productos en el carrito");
        }

        public ActionResult EliminarProductoCarrito(int id)
        {
            int? contadorProductos = HttpContext.Session.GetInt32("CantidadProductos") - 1;
            HttpContext.Session.Remove("Producto" + id);
            HttpContext.Session.Remove("CantidadProducto" + id);
            HttpContext.Session.SetInt32("CantidadProductos", Convert.ToInt32(contadorProductos));
            HttpContext.Session.SetString("CantidadProductosCarrito", "Hay " + HttpContext.Session.GetInt32("CantidadProductos") + " productos en el carrito");

            return RedirectToAction("ContenidoCarritoCompras");
        }

        public ActionResult LimpiarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("ListarProductos");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarProducto(IFormCollection form)
        {
            var idProducto = form["idProducto"];
            return View();
        }
    }
}
