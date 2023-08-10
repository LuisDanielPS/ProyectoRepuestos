using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Repuestos.Entities
{
    public class FacturaEncabezadoEnt
    {
        public int factura_id { get; set; }
        public int cliente_id { get; set; }
        public string factura_tipo { get; set; }
        public DateTime factura_fecha { get; set; }
        public string factura_descripcion { get; set; }
        public decimal factura_total { get; set; }
        public List<FacturasDetalleEnt> factura_detalle { get; set; }
    }
}