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
        FacturasModel modelFacturas = new FacturasModel();
        OrdenModel modelOrdenes = new OrdenModel();
        UsuarioModel modelUsuarios = new UsuarioModel();
        ProductoModel modelProductos = new ProductoModel();
        ClienteModel modelClientes = new ClienteModel();
        ProveedorModel modelProveedores = new ProveedorModel();
        RolesModel modelRoles = new RolesModel();

        [HttpGet]
        public ActionResult PanelAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Facturas()
        {
            var datos = modelFacturas.ConsultarFacturas();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Ordenes()
        {
            var datos = modelOrdenes.ConsultarOrdenes();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Usuarios()
        {
            var datos = modelUsuarios.ConsultarUsuarios();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Clientes()
        {
            var datos = modelClientes.ConsultarClientes();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Productos()
        {
            var datos = modelProductos.ConsultarProductos();
            return View(datos);
        }


        [HttpGet]
        public ActionResult Proveedores()
        {
            var datos = modelProveedores.ConsultarProveedores();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Roles()
        {
            var datos = modelRoles.ConsultarRoles();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Estados()
        {
            return View();
        }
    }
}