using AppCenfoMusica.DTO;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppCenfoMusica.Web.Controllers
{
    public class VendedorController : Controller
    {
        public IActionResult BuscarVendedorPorId(int id)
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
