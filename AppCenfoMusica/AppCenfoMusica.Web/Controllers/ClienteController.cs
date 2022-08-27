using AppCenfoMusica.DTO;
using AppCenfoMusica.Logica;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace AppCenfoMusica.Web.Controllers
{
    //[Authorize]
    public class ClienteController : Controller
    {
        public IActionResult BuscarClientePorId(int id, string? accion)
        {
            GestionClientesVM model = new GestionClientesVM();

            var resultado = new ClienteLogica().BuscarClientePorID(id);

            if (resultado.GetType() != typeof(ErrorDTO))
            {
                //Respuesta positiva
                model.Cliente = (ClienteDTO)resultado;

                if (accion == "guardar")
                {
                    ViewBag.Accion = "El producto se almacenó correctamente";
                }

                ViewBag.TipoCliente = "Estado Cliente: " + SetEstadoCliente(model.Cliente.Estado);
            }
            else
            {
                //Respuesta negativa
                model.Error = (ErrorDTO)resultado;
            }

            return View(model);

        }

        public ActionResult ListarClientes()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var logingVM = new LoginVM() { ReturnUrl = "Cliente/ListarClientes" };
                return RedirectToAction("Login", "Account", logingVM);
            }
            GestionClientesVM model = new GestionClientesVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/ListarClientes";

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
                var resultadoClientes = JsonConvert.DeserializeObject<List<ClienteDTO>>(datos);

                model.ListaClientes = resultadoClientes;
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

        public ActionResult AgregarCliente()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarCliente(ClienteDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                        var tarea = cliente.PostAsJsonAsync<ClienteDTO>("AgregarCliente", model);

                        tarea.Wait();

                        var resultado = tarea.Result;

                        var datos = resultado.Content.ReadAsStringAsync().Result;

                        if (!datos.Contains("Error"))
                        {
                            var respuesta = JsonConvert.DeserializeObject<ClienteDTO>(datos);

                            return RedirectToAction("BuscarClientePorID", new { id = respuesta.IdEntidad, accion = "guardar" });
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
                else
                {
                    ModelState.AddModelError("Error: ", "Datos inválidos");
                    throw new Exception("ErrorProgramacion");
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
        public ActionResult EditarEstado(int id)
        {
            GestionClientesVM model = new GestionClientesVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/BuscarClientePorId/" + id.ToString();

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
                var resultadoVendedor = JsonConvert.DeserializeObject<ClienteDTO>(datos);

                model.Cliente = resultadoVendedor;


                ViewBag.TipoCliente = "Estado Cliente: " + SetEstadoCliente(model.Cliente.Estado);

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
        public ActionResult EditarEstado(ClienteDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                        var tarea = cliente.PostAsJsonAsync<ClienteDTO>("ActualizarEstadoCliente", model);

                        tarea.Wait();

                        var resultado = tarea.Result;

                        var datos = resultado.Content.ReadAsStringAsync().Result;

                        if (!datos.Contains("Error"))
                        {
                            JsonSerializerSettings configuracion = new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            };
                            var respuesta = JsonConvert.DeserializeObject<ClienteDTO>(datos, configuracion);

                            return RedirectToAction("BuscarClientePorID", new { id = respuesta.IdEntidad, accion = "guardar" });
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
                    return View();
                }
                else
                {
                    ModelState.AddModelError("ErrorSistema", "Ocurrió un error inesperado, favor ponerse en contacto con el personal autorizado");
                    //almacenamiento en bitácroa que guarde efectivamente el texto del error
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditarDatos(int id)
        {
            GestionClientesVM model = new GestionClientesVM();

            // Define la conexión con nuestros servicios
            var url = "https://localhost:7257/api/Service/BuscarClientePorId/" + id.ToString();

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
                var resultadoVendedor = JsonConvert.DeserializeObject<ClienteDTO>(datos);

                model.Cliente = resultadoVendedor;


                ViewBag.TipoCliente = "Estado Cliente: " + SetEstadoCliente(model.Cliente.Estado);

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
        public ActionResult EditarDatos(ClienteDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri("https://localhost:7257/api/Service/");

                        var tarea = cliente.PostAsJsonAsync<ClienteDTO>("ActualizarEstadoCliente", model);

                        tarea.Wait();

                        var resultado = tarea.Result;

                        var datos = resultado.Content.ReadAsStringAsync().Result;

                        if (!datos.Contains("Error"))
                        {
                            JsonSerializerSettings configuracion = new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            };
                            var respuesta = JsonConvert.DeserializeObject<ClienteDTO>(datos, configuracion);

                            return RedirectToAction("BuscarClientePorID", new { id = respuesta.IdEntidad, accion = "guardar" });
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

        public string SetEstadoCliente(int estado)
        {
            string estadoCliente = string.Empty;
            switch (estado)
            {
                case 1:
                    estadoCliente = "Activo";
                    break;
                case 2:
                    estadoCliente = "Inactivo";
                    break;
                default:
                    break;
            }
            return estadoCliente;
        }

    }
}
