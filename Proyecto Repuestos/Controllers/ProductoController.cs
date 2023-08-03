using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class ProductoController : Controller
    {
        ProductoModel modelProductos = new ProductoModel();
        
        [HttpGet]
        public ActionResult RepUsados()
        {
            var datos = modelProductos.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult RepGenericos()
        {
            var datos = modelProductos.ConsultarProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult RepOriginales()
        {
            var datos = modelProductos.ConsultarProductos();
            return View(datos);
        }

        public ActionResult CargarCategorias()
        {
            var datos = modelProductos.CargarCategorias();
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarEstados()
        {
            var datos2 = modelProductos.CargarEstados();
            return Json(datos2, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditarProductoAPI(ProductoEnt entidad)
        {
            var datos = modelProductos.EditarProductoAPI(entidad); 
            if (datos > 0)
                return RedirectToAction("Productos", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del producto";
                return View("Productos");
            }
        }

        [HttpPost]
        public ActionResult EliminaProducto(int producto_id)
        {
            var resultado = modelProductos.EliminaProducto(producto_id);

            if (resultado == 1)
            {
                return RedirectToAction("Productos", "Admin");
            }
            else if (resultado == 0)
            {
                return View("Productos");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del producto";
                return View("Productos");
            }
        }


        [HttpPost]
        public ActionResult CrearProducto(ProductoEnt entidad)
        {
            try
            {
               

                var resp = modelProductos.RegistrarProducto(entidad);

                if (resp > 0)
                    return RedirectToAction("Productos", "Admin");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Productos");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}