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
