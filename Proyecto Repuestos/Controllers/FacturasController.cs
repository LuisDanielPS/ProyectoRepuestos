using Newtonsoft.Json.Linq;
using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class FacturasController : Controller
    {
        FacturasModel modelFacturas = new FacturasModel();
        ClienteModel modelClientes = new ClienteModel();
        ProductoModel modelProductos = new ProductoModel();

        // GET: Facturas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrearFactura()
        {
            FacturasModel modelFacturas = new FacturasModel();
            var clientes = modelClientes.ConsultarClientes();
            var productos = modelProductos.ConsultarProductos();

            var ComboClientes = new List<SelectListItem>();
            foreach (var item in clientes)
            {
                ComboClientes.Add(new SelectListItem
                {
                    Text = item.cliente_nombre,
                    Value = item.cliente_id.ToString()
                });
            }

            var ComboProductos = new List<SelectListItem>();
            foreach (var item in productos)
            {
                ComboProductos.Add(new SelectListItem
                {
                    Text = item.producto_descripcion,
                    Value = item.producto_id.ToString()
                });
            }

            ViewBag.Combo = ComboClientes;
            ViewBag.ComboP = ComboProductos;
            return View();
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = modelFacturas.ConsultarFactura(q);
            var clientes = modelClientes.ConsultarClientes();
            var productos = modelProductos.ConsultarProductos();
            var ComboClientes = new List<SelectListItem>();
            foreach (var item in clientes)
            {
                ComboClientes.Add(new SelectListItem
                {
                    Text = item.cliente_nombre,
                    Value = item.cliente_id.ToString()
                });
            }

            var ComboProductos = new List<SelectListItem>();
            foreach (var item in productos)
            {
                ComboProductos.Add(new SelectListItem
                {
                    Text = item.producto_descripcion,
                    Value = item.producto_id.ToString()
                });
            }

            ViewBag.Combo = ComboClientes;
            ViewBag.ComboP = ComboProductos;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarFactura(FacturaEncabezadoEnt entidad)
        {
            var resp = modelFacturas.EditarFactura(entidad);

            if (resp > 0)
                return RedirectToAction("Facturas", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No fue posible actualizar la información de la factura";
                return View("Editar");
            }
        }

        [HttpGet]
        public ActionResult EliminarFactura(int q)
        {
            var resp = modelFacturas.EliminarFactura(q);

            if (resp > 0) {
                ViewBag.MsjPantalla = "Factura eliminada con éxito";
                return RedirectToAction("Facturas", "Admin");
            } else {
                ViewBag.MsjPantalla = "No se pudo eliminar la factura";
                return RedirectToAction("Facturas", "Admin");
            }
        }

    }
}