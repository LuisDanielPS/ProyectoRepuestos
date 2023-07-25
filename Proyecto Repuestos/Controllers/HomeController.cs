using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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
                    Session["IdUsuario"] = resp.usuario_id.ToString();
                    Session["NombreUsuario"] = resp.usu_nombre;
                    Session["RolUsuario"] = resp.rol_descripcion;
                    Session["CorreoUsuario"] = resp.usu_correo;
                    Session["idRolUsuario"] = resp.rol_id;
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


        [HttpGet]
        public ActionResult Cambiar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CambiarContrasenna(UsuarioEnt entidad)
        {
            entidad.usuario_id = long.Parse(Session["IdUsuario"].ToString());
            entidad.usu_correo = Session["CorreoUsuario"].ToString();
            entidad.usu_clave = model.Encrypt(entidad.usu_clave);
            entidad.ContrasennaNueva = model.Encrypt(entidad.ContrasennaNueva);
            entidad.ConfirmarContrasenna = model.Encrypt(entidad.ConfirmarContrasenna);

            var respValidar = model.IniciarSesion(entidad);

            if (respValidar == null)
            {
                ViewBag.MsjPantalla = "Su contraseña no es correcta";
                return View("Cambiar");
            }

            if (entidad.ContrasennaNueva != entidad.ConfirmarContrasenna)
            {
                ViewBag.MsjPantalla = "Las contraseñas no coinciden";
                return View("Cambiar");
            }

            if (entidad.ContrasennaNueva == entidad.usu_clave)
            {
                ViewBag.MsjPantalla = "Debe ingresar una contraseña diferente a la utilizada actualmente";
                return View("Cambiar");
            }

            var respCambiar = model.CambiarContrasenna(entidad);

            if (respCambiar > 0)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido cambiar su contraseña";
                return View("Cambiar");
            }
        }

        [HttpGet]
        public ActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioEnt entidad)
        {
            try
            {
                var resp = model.RecuperarContrasenna(entidad);

                if (resp)
                    return RedirectToAction("Login", "Home");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido recuperar su información";
                    return View("Recuperar");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


    }
}