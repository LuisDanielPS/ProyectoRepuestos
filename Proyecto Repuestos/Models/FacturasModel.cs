using Proyecto_Repuestos.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace Proyecto_Repuestos.Models
{
    public class FacturasModel
    {
        public List<FacturaEncabezadoEnt> ConsultarFacturas()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarFacturas";
                    HttpResponseMessage resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<FacturaEncabezadoEnt>>().Result;
                    }

                    return new List<FacturaEncabezadoEnt>();
                }
            }
            catch (Exception ex)
            {
                return new List<FacturaEncabezadoEnt>();
            }
        }

        public bool RegistrarFactura(FacturaEncabezadoEnt factura)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegistrarFactura";

                    HttpResponseMessage resp = client.PostAsJsonAsync(url, factura).Result;

                    return resp.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int EliminarFactura(int factura_id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EliminarFactura?factura_id=" + factura_id;

                    HttpResponseMessage resp = client.DeleteAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<int>().Result;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}