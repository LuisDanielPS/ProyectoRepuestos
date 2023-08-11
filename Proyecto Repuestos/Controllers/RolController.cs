using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class RolController : Controller
    {
        RolesModel modelRoles= new RolesModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditarRolAPI(RolesEnt entidad)
        {
            var datos = modelRoles.EditarRolAPI(entidad);
            if (datos > 0)
                return RedirectToAction("Roles", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del rol";
                return View("Roles");
            }
        }

        [HttpPost]
        public ActionResult EliminarRolAPI(int rol_id)
        {
            var resultado = modelRoles.EliminaRol(rol_id);

            if (resultado == 1)
            {
                return RedirectToAction("Roles", "Admin");
            }
            else if (resultado == 0)
            {
                return View("Roles");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido eliminar la información del rol";
                return View("Roles");
            }
        }

        [HttpPost]
        public ActionResult CrearRol(RolesEnt entidad)
        {
            try
            {


                var resp = modelRoles.RegistrarRol(entidad);

                if (resp > 0)
                    return RedirectToAction("Roles", "Admin");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Roles");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}