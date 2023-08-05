using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Repuestos.Entities
{
    public class FacturasDetalleEnt
    {
        public int facturaD_id { get; set; }
        public int factura_id { get; set; }
        public int producto_id { get; set; }
        public int facturaD_cantidad { get; set; }
        public double facturaD_precio { get; set; }
        public double facturaD_descuento { get; set; }
        public double facturaD_total { get; set; }
    }
}