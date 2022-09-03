using AppCenfoMusica.DTO;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace AppCenfoMusica.Web.Controllers
{
    //[Authorize]
    public class VendedorController : Controller
    {
        public ActionResult BuscarVendedorPorId(int id, string? accion)
        {
            if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            GestionVendedoresVM model = new GestionVendedoresVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/BuscarVendedorPorId/" + id.ToString();

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

            var resultado = JsonConvert.DeserializeObject<BaseDTO>(datos, configuracion);

            if (resultado.IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoVendedor = JsonConvert.DeserializeObject<VendedorDTO>(datos);

                model.Vendedor = resultadoVendedor;

                ViewBag.TipoVendedor = "Tipo Vendedor: " + SetTipoVendedor(model.Vendedor.Puesto);

                ViewData["EstadoVendedor"] = "Estado Vendedor:"+ SetEstadoVendedor(model.Vendedor.Estado);

                if (accion == "guardar")
                {
                    ViewBag.Accion = "El vendedor se almacenó correctamente";
                }
                else if (accion == "eliminar")
                {
                    ViewBag.Accion = "El vendedor se elimino correctamente";
                }

            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
            }
            return View(model);

        }

        public string SetTipoVendedor(int puesto)
        {
            string tipoVendedor = string.Empty;

            switch (puesto)
            {
                case 1:
                    tipoVendedor = "Vendedor de Planta";
                    break;
                case 2:
                    tipoVendedor = "Proveedor";
                    break;
                case 3:
                    tipoVendedor = "Gerente de Tienda";
                    break;
                default:
                    break;
            }
            return tipoVendedor;
        }

        public string SetEstadoVendedor(int estado)
        {
            string estadoVendedor = string.Empty;
            switch (estado)
            {
                case 1:
                    estadoVendedor = "Activo";
                    break;
                case 2:
                    estadoVendedor = "Inactivo";
                    break;
                case 3:
                    estadoVendedor = "Vacaciones";
                    break;
                default:
                    break;
            }
            return estadoVendedor;
        }

        [HttpGet]
        public ActionResult EditarEstado(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            GestionVendedoresVM model = new GestionVendedoresVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/BuscarVendedorPorId/" + id.ToString();

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

            var resultado = JsonConvert.DeserializeObject<BaseDTO>(datos, configuracion);

            if (resultado.IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoVendedor = JsonConvert.DeserializeObject<VendedorDTO>(datos);

                model.Vendedor = resultadoVendedor;


                ViewBag.TipoVendedor = "Tipo Vendedor: " + SetTipoVendedor(model.Vendedor.Puesto);

                ViewData["EstadoVendedor"] = "Estado Vendedor:" + SetEstadoVendedor(model.Vendedor.Estado);
                return View(resultadoVendedor);
            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
            }
            return View(model);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEstado(VendedorDTO model)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
                {
                    var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                    return RedirectToAction("Login", "Account", logingVM);
                }

                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewData["UserType"] = HttpContext.Session.GetString("UserType");

                if (ModelState.IsValid)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                        var tarea = cliente.PostAsJsonAsync<VendedorDTO>("ActualizarEstadoVendedor", model);

                        tarea.Wait();

                        var resultado = tarea.Result;

                        var datos = resultado.Content.ReadAsStringAsync().Result;

                        if (!datos.Contains("Error"))
                        {
                            JsonSerializerSettings configuracion = new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            };
                            var respuesta = JsonConvert.DeserializeObject<VendedorDTO>(datos, configuracion);

                            return RedirectToAction("BuscarVendedorPorID", new { id = respuesta.IdEntidad, accion = "guardar" });
                            //return Content("<div><h4>¡Operación exitosa!</h4><br /><div>Se insertó el nuevo producto con el nombre" + respuesta.Nombre + "</div></div>","text/html");
                        }
                        else
                        {
                            var respuesta = JsonConvert.DeserializeObject<ErrorDTO>(datos);
                            ModelState.AddModelError("ErrorProgramacion", respuesta.MensajeError);
                            throw new Exception();
                        }
                    }
                }
                
            }
            catch (Exception error)
            {
                if (error.Message == "ErrorProgramacion")
                {
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("ErrorSistema", "Ocurrió un error inesperado, favor ponerse en contacto con el personal autorizado");
                    //almacenamiento en bitácroa que guarde efectivamente el texto del error
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult ListarVendedores(string? accion)
        {
            if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            if (accion == "guardar")
            {
                ViewBag.Accion = "El vendedor se almacenó correctamente";
            }
            else if (accion == "eliminar")
            {
                ViewBag.Accion = "El vendedor se elimino correctamente";
            }

            GestionVendedoresVM model = new GestionVendedoresVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/ListarVendedores";

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

            if (resultado.ElementAt(0).IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoVendedores = JsonConvert.DeserializeObject<List<VendedorDTO>>(datos);

                model.ListarVendedores = resultadoVendedores;
            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
                return View(model);
            }
               
            
            return View(model);

        }

        public ActionResult BienvenidoVendedor(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/BienvenidoVendedor" };
                return RedirectToAction("Login", "Account", logingVM);
            }

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            GestionVendedoresVM model = new GestionVendedoresVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/BuscarVendedorPorId/" + id.ToString();

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

            var resultado = JsonConvert.DeserializeObject<BaseDTO>(datos, configuracion);

            if (resultado.IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoVendedor = JsonConvert.DeserializeObject<VendedorDTO>(datos);

                model.Vendedor = resultadoVendedor;

                ViewBag.TipoVendedor = "Tipo Vendedor: " + SetTipoVendedor(model.Vendedor.Puesto);

                ViewData["EstadoVendedor"] = "Estado Vendedor:" + SetEstadoVendedor(model.Vendedor.Estado);

            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
            }
            return View(model);

        }

        //GET: Únicamente nos va a mostrar el View (a menos que necesitemos configurar alguna cuestión adicional)
        public ActionResult AgregarVendedor()
        {
            //if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
            //{
            //    var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
            //    return RedirectToAction("Login", "Account", logingVM);
            //}

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarVendedor(VendedorDTO model)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                    var tarea = cliente.PostAsJsonAsync<VendedorDTO>("AgregarVendedor", model);

                    tarea.Wait();

                    var resultado = tarea.Result;

                    var datos = resultado.Content.ReadAsStringAsync().Result;

                    if (!datos.Contains("Error"))
                    {
                        JsonSerializerSettings configuracion = new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        };
                        var respuesta = JsonConvert.DeserializeObject<VendedorDTO>(datos, configuracion);

                        return RedirectToAction("BuscarVendedorPorID", new { id = respuesta.IdEntidad, accion = "guardar" });
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
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("ErrorSistema", "Ocurrió un error inesperado, favor ponerse en contacto con el personal autorizado");
                    //almacenamiento en bitácroa que guarde efectivamente el texto del error
                    return View(model);
                }
            }
        }

        [HttpGet]
        public ActionResult EliminarVendedor(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null || HttpContext.Session.GetString("UserType") != "Vendedor")
            {
                var logingVM = new LoginVM() { ReturnUrl = "Vendedor/ListarVendedores" };
                return RedirectToAction("Login", "Account", logingVM);
            }

            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserType"] = HttpContext.Session.GetString("UserType");

            GestionVendedoresVM model = new GestionVendedoresVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/BuscarVendedorPorId/" + id.ToString();

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

            var resultado = JsonConvert.DeserializeObject<BaseDTO>(datos, configuracion);

            if (resultado.IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoVendedor = JsonConvert.DeserializeObject<VendedorDTO>(datos);

                model.Vendedor = resultadoVendedor;


                ViewBag.TipoVendedor = "Tipo Vendedor: " + SetTipoVendedor(model.Vendedor.Puesto);

                ViewData["EstadoVendedor"] = "Estado Vendedor:" + SetEstadoVendedor(model.Vendedor.Estado);
                return View(resultadoVendedor);
            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
            }
            return View(model);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarVendedor(VendedorDTO vendedorDTO)
        {
            GestionVendedoresVM model = new GestionVendedoresVM();

            try
            {
                if (ModelState.IsValid)
                {              
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                        var tarea = cliente.PostAsJsonAsync<VendedorDTO>("EliminarVendedor", vendedorDTO);

                        tarea.Wait();

                        var resultado2 = tarea.Result;

                        var datos = resultado2.Content.ReadAsStringAsync().Result;

                        if (!datos.Contains("Error"))
                        {
                            JsonSerializerSettings configuracion = new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            };
                            var respuesta = JsonConvert.DeserializeObject<VendedorDTO>(datos, configuracion);

                            return RedirectToAction("ListarVendedores", new { accion = "eliminar" });
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

            }
            catch (Exception error)
            {
                if (error.Message == "ErrorProgramacion")
                {
                    return View(vendedorDTO);
                }
                else
                {
                    ModelState.AddModelError("ErrorSistema", "Ocurrió un error inesperado, favor ponerse en contacto con el personal autorizado");
                    //almacenamiento en bitácroa que guarde efectivamente el texto del error
                    return View(vendedorDTO);
                }
            }
            return View(vendedorDTO);
        }

    }
}
