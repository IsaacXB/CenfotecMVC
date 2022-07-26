using AppCenfoMusica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.Datos.Helpers
{
    internal class ControladorRetornos
    {
        internal static RespuestaDTO ControladorErrores (Exception error)
        {
            return new RespuestaDTO()
            {
                Codigo = error.InnerException != null ? -(error.InnerException.HResult) : -1,
                Contenido = new ErrorDTO { MensajeError = error.InnerException != null ? error.Message : error.Message, }
            };
        }
    }
}
