using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}