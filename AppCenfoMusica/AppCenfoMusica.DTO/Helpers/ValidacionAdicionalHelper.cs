using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenfoMusica.DTO.Helpers
{
    public class ValidacionHora: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            try
            {
                if (value != null)
                {
                    int hora = Convert.ToInt32(value);
                    if (hora >= 0 && hora <= 23)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            return false;
        }
    }
    public class ValidacionMinuto : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            try
            {
                if (value != null)
                {
                    int hora = Convert.ToInt32(value);
                    if (hora >= 0 && hora <= 59)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
    }

    public class ValidacionContraseña : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            try
            {
                if (value == null) return false;
                if (value != null)
                {
                    string? contraseña = value.ToString();
                    bool tieneDigito = contraseña == null ? false : contraseña.Any(char.IsDigit);
                    bool tieneCaracterEspecial = contraseña == null ? false : TieneCaracterEspecial(contraseña);
                    bool tieneCaracterNoPermitido = contraseña == null ? false : TieneCaracterNoPermitido(contraseña);
                    if (tieneDigito && tieneCaracterEspecial && !tieneCaracterNoPermitido)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
        public static bool TieneCaracterEspecial(string input)
        {
            string specialChar = @"#_$!";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public static bool TieneCaracterNoPermitido(string input)
        {
            string specialChar = @" .,;-*";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
    }

    public class ValidacionFechaEntrega : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            try
            {
                if (value == null) return false;
                var fecha = value.ToString() == null ? string.Empty : value.ToString() ;
                if (fecha == null) return false;

                DateTime fechaEntrega =  DateTime.Parse(fecha);
                DateTime fechaHoy = DateTime.Now;
                TimeSpan timeSpan = fechaEntrega - fechaHoy;

                if (timeSpan.Days > 30)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (System.Exception ex)
            {
                return false;
            }

            return false;
        }
    }

   public class ValidacionEmailCenfotec : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            try
            {
                if (value != null)
                {
                    string? email = value.ToString();
                    if (email != null && email.Contains("@ucenfotec.ac.cr"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
    }
}
