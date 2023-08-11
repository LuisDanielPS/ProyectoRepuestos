using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Repuestos.Entities
{
    public class ProveedoresEnt
    {
        public long proveedor_id { get; set; }
        public string proveedor_cedula { get; set; }
        public string proveedor_nombre { get; set; }
        public string proveedor_apellido { get; set; }
        public string proveedor_correo { get; set; }
        public string proveedor_telefono { get; set; }
        public string proveedor_direccion { get; set; }

    }
}