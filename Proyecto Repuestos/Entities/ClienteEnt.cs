using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Repuestos.Entities
{
    public class ClienteEnt
    {
        public int cliente_id { get; set; }
        public string cliente_cedula { get; set; }
        public string cliente_nombre { get; set; }
        public string cliente_apellido { get; set; }
        public string cliente_correo { get; set; }
        public string cliente_telefono { get; set; }
        public string cliente_direccion { get; set; }


    }
}