using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppCenfoMusica.Web.ViewModels
{
    public class LoginVM
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserType { get; set; }

        public bool IsAuthenticated { get; set; }

        //[Display(Name = "Remember Me")]
        //public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
