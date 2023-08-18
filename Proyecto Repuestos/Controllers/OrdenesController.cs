using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class OrdenesController : Controller
    {
        OrdenModel modelOrdenes = new OrdenModel();
        ProveedorModel modelProveedores = new ProveedorModel();
        ProductoModel modelProductos = new ProductoModel();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrearOrden()
        {
            var clientes = modelProveedores.ConsultarProveedores();
            var productos = modelProductos.ConsultarProductos();

            var ComboProveedores = new List<SelectListItem>();
            foreach (var item in clientes)
            {
                ComboProveedores.Add(new SelectListItem
                {
                    Text = item.proveedor_nombre,
                    Value = item.proveedor_id.ToString()
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

            ViewBag.Combo = ComboProveedores;
            ViewBag.ComboP = ComboProductos;
            return View();
        }

        [HttpPost]
        public ActionResult EditarOrden(OrdenEncabezadoEnt entidad)
        {
            var resp = modelOrdenes.EditarOrden(entidad);

            if (resp > 0)
                return RedirectToAction("Ordenes", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No fue posible actualizar la información de la orden de compra";
                return View("Editar");
            }
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = modelOrdenes.ConsultarOrden(q);
            var proveedores = modelProveedores.ConsultarProveedores();
            var productos = modelProductos.ConsultarProductos();
            var ComboProveedores = new List<SelectListItem>();
            foreach (var item in proveedores)
            {
                ComboProveedores.Add(new SelectListItem
                {
                    Text = item.proveedor_nombre,
                    Value = item.proveedor_id.ToString()
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

            ViewBag.Combo = ComboProveedores;
            ViewBag.ComboP = ComboProductos;
            return View(datos);
        }

        [HttpGet]
        public ActionResult EliminarOrden(int q)
        {
            var resp = modelOrdenes.EliminarOrden(q);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Orden de compra eliminada con éxito";
                return RedirectToAction("Ordenes", "Admin");
            }
            else
            {
                ViewBag.MsjPantalla = "No se pudo eliminar la orden de compra";
                return RedirectToAction("Ordenes", "Admin");
            }
        }

    }
}
