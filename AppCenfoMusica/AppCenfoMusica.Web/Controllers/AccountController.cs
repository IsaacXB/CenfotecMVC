﻿using AppCenfoMusica.DTO;
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
            string userType = string.Empty;

            if (returnUrl.Contains("Cliente"))
            {
                userType = "Cliente";
            }
            else if (returnUrl.Contains("Vendedor"))
            {
                userType = "Vendedor";
            }
            else if (returnUrl.Contains("Home") || returnUrl.Contains("Producto"))
            {
                userType = "Home";
            }

            var model = new LoginVM { ReturnUrl = returnUrl, UserType = userType };
   
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            ViewBag.UserName = string.Empty;
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserType");
            return RedirectToAction("Index", "Home", "Home");
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
            string userType = model.UserType;
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
                    ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                    ViewData["UserType"] = HttpContext.Session.GetString("UserType");

                    HttpContext.Session.SetString("UserName", model.Username);
                    HttpContext.Session.SetString("UserType", model.UserType);
                    return RedirectToAction("BienvenidoVendedor", "Vendedor", new { id = resultado.IdEntidad });
                }
                else
                {
                    //Respuesta negativa
                    model.IsAuthenticated = false;
                    ViewBag.UserName = string.Empty;
                    HttpContext.Session.Remove("UserName");
                    HttpContext.Session.Remove("UserType");
                    ModelState.AddModelError("", "Usuario o contraseña invalida.");

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
                    ViewData["UserName"] = model.Username;
                    HttpContext.Session.SetString("UserName", model.Username);
                    HttpContext.Session.SetString("UserType", model.UserType);
                    return RedirectToAction("BienvenidoCliente", "Cliente", new { id = resultado.IdEntidad });
                }
                else
                {
                    //Respuesta negativa
                    model.IsAuthenticated = false;
                    ViewBag.UserName = string.Empty;
                    ViewData["UserName"] = string.Empty;
                    HttpContext.Session.Remove("UserName");
                    HttpContext.Session.Remove("UserType");
                    ModelState.AddModelError("", "Usuario o contraseña invalida.");

                }

                return View(model);

            }
            else if (model.UserType == "Home")
            {
                var resultado = new VendedorLogica().ValidarVendedor(model.Username, model.Password);

                if (resultado.GetType() != typeof(ErrorDTO))
                {
                    //Respuesta positiva
                    model.IsAuthenticated = true;

                    ViewBag.IsAuthenticated = true;
                    ViewBag.UserName = model.Username;
                    ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                    ViewData["UserType"] = HttpContext.Session.GetString("UserType");

                    model.UserType = "Vendedor";
                    HttpContext.Session.SetString("UserName", model.Username);
                    HttpContext.Session.SetString("UserType", model.UserType);
                    return RedirectToAction("BienvenidoVendedor", "Vendedor", new { id = resultado.IdEntidad });
                }
                else
                {
                    resultado = new ClienteLogica().ValidarCliente(model.Username, model.Password);

                    if (resultado.GetType() != typeof(ErrorDTO))
                    {
                        //Respuesta positiva
                        model.IsAuthenticated = true;

                        ViewBag.IsAuthenticated = true;
                        ViewBag.UserName = model.Username;
                        ViewData["UserName"] = model.Username;
                        model.UserType = "Cliente";
                        HttpContext.Session.SetString("UserName", model.Username);
                        HttpContext.Session.SetString("UserType", model.UserType);
                        return RedirectToAction("BienvenidoCliente", "Cliente", new { id = resultado.IdEntidad });

                    }

                    return View(model);
                }

                if (resultado.GetType() == typeof(ErrorDTO))
                {
                    //Respuesta negativa
                    model.IsAuthenticated = false;
                    ViewBag.UserName = string.Empty;
                    ViewData["UserName"] = string.Empty;
                    HttpContext.Session.Remove("UserName");
                    HttpContext.Session.Remove("UserType");
                    ModelState.AddModelError("", "Usuario o contraseña invalida.");

                }
            }
            ModelState.AddModelError("", "Error al intentar hacer login.");
            return View(model);
        }
    }
}
