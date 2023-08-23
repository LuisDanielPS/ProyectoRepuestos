using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class ProveedorController : Controller
    {
        ProveedorModel modelProveedor = new ProveedorModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditarProductoAPI(ProveedoresEnt entidad)
        {
            var datos = modelProveedor.EditarProveedorAPI(entidad);
            if (datos > 0)
                return RedirectToAction("Proveedores", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del proveedor";
                return View("Proveedores");
            }
        }

        [HttpPost]
        public ActionResult CrearProveedor(ProveedoresEnt entidad)
        {
            try
            {


                var resp = modelProveedor.RegistrarProveedor(entidad);

                if (resp > 0)
                    return RedirectToAction("Proveedores", "Admin");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Proveedores");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EliminaProveedor(int proveedor_id)
        {
            var resultado = modelProveedor.EliminarProveedor(proveedor_id);

            if (resultado == 1)
            {
                return RedirectToAction("Proveedores", "Admin");
            }
            else if (resultado == 0)
            {
                return View("Proveedores");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido eliminar la información del proveedor";
                return View("Proveedores");
            }
        }
    }
}