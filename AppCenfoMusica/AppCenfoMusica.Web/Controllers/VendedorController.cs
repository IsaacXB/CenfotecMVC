using AppCenfoMusica.DTO;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppCenfoMusica.Web.Controllers
{
    public class VendedorController : Controller
    {
        public ActionResult BuscarVendedorPorId(int id)
        {
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

            var resultado = JsonConvert.DeserializeObject<List<BaseDTO>>(datos);

            if (resultado.ElementAt(0).IdEntidad > 0)
            {
                //respuesta positiva
                var resultadoVendedor = JsonConvert.DeserializeObject<List<VendedorDTO>>(datos);

                model.ListarVendedores = resultadoVendedor;
            }
            else
            {
                //respuesta negativa
                var resultadoError = JsonConvert.DeserializeObject<List<ErrorDTO>>(datos);

                model.Error = (ErrorDTO)resultadoError.ElementAt(0);
            }
            return View(model);

        }

        public ActionResult ListarVendedores()
        {
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

            var resultado = JsonConvert.DeserializeObject<List<BaseDTO>>(datos);

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
            }
            return View(model);
        }

        //GET: Únicamente nos va a mostrar el View (a menos que necesitemos configurar alguna cuestión adicional)
        public ActionResult AgregarVendedor()
        {
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
    }
}
