using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel modelUsuarios = new UsuarioModel();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CargarRoles()
        {
            var datos2 = modelUsuarios.CargaRoles();
            return Json(datos2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditarUsuarioAPI(UsuarioEnt entidad)
        {
            var datos = modelUsuarios.EditaUsuarioAPI(entidad);
            if (datos > 0)
                return RedirectToAction("Usuarios", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("Usuarios");
            }
        }

        [HttpPost]
        public ActionResult CrearUsuario(UsuarioEnt entidad)
        {
            try
            {


                var resp = modelUsuarios.RegistrarUsuario(entidad);

                if (resp > 0)
                    return RedirectToAction("Usuarios", "Admin");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Usuarios");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult EliminaUsuario(int usuario_id)
        {
            var resultado = modelUsuarios.EliminaUsuario(usuario_id);

            if (resultado == 1)
            {
                return RedirectToAction("Usuarios", "Admin");
            }
            else if (resultado == 0)
            {
                return View("Usuarios");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido eliminar la información del usuario";
                return View("Usuarios");
            }
        }


    }
}