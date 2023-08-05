using Proyecto_Repuestos.Entities;
using Proyecto_Repuestos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Repuestos.Controllers
{
    public class ClienteController : Controller
    {
        ClienteModel modelClientes = new ClienteModel();

        [HttpGet]
        public ActionResult ConsultarClientes(ClienteEnt entidad)
        {
            var datos = modelClientes.ConsultarClientes();
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreaCliente(ClienteEnt entidad)
        {
            try
            {


                var resp = modelClientes.RegistrarCliente(entidad);

                if (resp > 0)
                    return RedirectToAction("Clientes", "Admin");
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido registrar su información";
                    return View("Clientes");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EditarClienteAPI(ClienteEnt entidad)
        {
            var datos = modelClientes.EditarClienteAPI(entidad);
            if (datos > 0)
                return RedirectToAction("Clientes", "Admin");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del cliente";
                return View("Clientes");
            }
        }


        [HttpPost]
        public ActionResult EliminaCliente(int cliente_id)
        {
            var resultado = modelClientes.EliminaCliente(cliente_id);

            if (resultado == 1)
            {
                return RedirectToAction("Clientes", "Admin");
            }
            else if (resultado == 0)
            {
                return View("Clientes");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del cliente";
                return View("Clientes");
            }
        }
    }
}