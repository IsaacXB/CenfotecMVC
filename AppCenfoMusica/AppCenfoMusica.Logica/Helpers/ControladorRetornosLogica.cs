using AppCenfoMusica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.Logica.Helpers
{
    internal class ControladorRetornosLogica
    {
        internal static BaseDTO ControladorErrores(Exception error)
        {
            return new ErrorDTO()
            {
                
                CodigoError = error.InnerException != null ? -(error.InnerException.HResult) : -1,
                MensajeError = error.InnerException != null ? error.Message : error.Message
            };
        }

        internal static List<BaseDTO> ControladorErroresLista(Exception error)
        {
            return new List<BaseDTO>()
            {
                new ErrorDTO{
                CodigoError = error.InnerException != null ? -(error.InnerException.HResult) : -1,
                Mensaje = error.InnerException != null ? error.Message : error.Message
                }
            };
        }


    }
}
