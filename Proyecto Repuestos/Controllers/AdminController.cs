using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class AdminController : Controller
    {
        UsuarioModel modelUsuarios = new UsuarioModel();
        [HttpGet]
        public ActionResult PanelAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Clientes()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Usuarios()
        {
            var datos = modelUsuarios.ConsultarUsuarios();
            return View(datos);
        }


        [HttpGet]
        public ActionResult Productos()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Proveedores()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Estados()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Roles()
        {
            return View();
        }
    }
}