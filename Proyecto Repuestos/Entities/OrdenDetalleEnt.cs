using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Repuestos.Entities
{
    public class OrdenDetalleEnt
    {
        public int ordenD_id { get; set; }
        public int orden_id { get; set; }
        public int producto_id { get; set; }
        public int producto_cantidad { get; set; }
    }
}