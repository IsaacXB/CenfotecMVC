using AppCenfoMusica.DTO;
using AppCenfoMusica.Logica;
using AppCenfoMusica.Web.Models;
using AppCenfoMusica.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace AppCenfoMusica.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = "Home/Index")
        {
            string userType = returnUrl.Contains("Cliente") ? "Cliente" : "Vendedor";

            var model = new LoginVM { ReturnUrl = returnUrl, UserType = userType };
   
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    //await _signManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            string userType = model.ReturnUrl.Contains("Cliente") ? "Cliente" : "Vendedor";
            model.UserType = userType;

            if (model.UserType == "Vendedor")
            {
                var resultado = new VendedorLogica().ValidarVendedor(model.Username, model.Password);

                if (resultado.GetType() != typeof(ErrorDTO))
                {
                    //Respuesta positiva
                    model.IsAuthenticated = true;

                    ViewBag.IsAuthenticated = true;
                    ViewBag.UserName = model.Username;
                    HttpContext.Session.SetString("UserName", model.Username);
                    RedirectToAction("ListarVendedores", "Vendedor");
                }
                else
                {
                    //Respuesta negativa
                    model.IsAuthenticated = false;
                    ViewBag.UserName = string.Empty;
                    HttpContext.Session.Remove("UserName");
                }

                return View(model);
            }
            else if (model.UserType == "Cliente")
            {
                var resultado = new ClienteLogica().ValidarCliente(model.Username, model.Password);

                if (resultado.GetType() != typeof(ErrorDTO))
                {
                    //Respuesta positiva
                    model.IsAuthenticated = true;

                    ViewBag.IsAuthenticated = true;
                    ViewBag.UserName = model.Username;
                    HttpContext.Session.SetString("UserName", model.Username);
                    RedirectToAction("ListarVendedores", "Vendedor");
                }
                else
                {
                    //Respuesta negativa
                    model.IsAuthenticated = false;
                    ViewBag.UserName = string.Empty;
                    HttpContext.Session.Remove("UserName");
                }

                return View(model);

            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
    }
}
