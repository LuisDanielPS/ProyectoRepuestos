using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel model = new UsuarioModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            try
            {
                entidad.usu_clave = model.Encrypt(entidad.usu_clave);
                var resp = model.IniciarSesion(entidad);

                if (resp != null)
                {
                    Session["NombreUsuario"] = resp.usu_nombre;
                    Session["RolUsuario"] = resp.rol_descripcion;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MsjPantalla = "Error al iniciar sesión por favor verifique su correo electrónico y contraseña";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public ActionResult Registrarse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                entidad.usu_clave = model.Encrypt(entidad.usu_clave);
                entidad.rol_id = 2;

                var resp = model.RegistrarUsuario(entidad);

                if (resp > 0)
                    return RedirectToAction("Login", "Home");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su usuario";
                    return View("Registrarse");
                }
            }
            catch (Exception ex)
            {
              
                return View("Error" , ex);
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}