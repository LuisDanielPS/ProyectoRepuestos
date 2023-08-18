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
    public class OrdenModel
    {

        public List<OrdenEncabezadoEnt> ConsultarOrdenes()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarOrdenes";
                    HttpResponseMessage resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<OrdenEncabezadoEnt>>().Result;
                    }

                    return new List<OrdenEncabezadoEnt>();
                }
            }
            catch (Exception ex)
            {
                return new List<OrdenEncabezadoEnt>();
            }
        }

        public int EditarOrden(OrdenEncabezadoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EditarOrden";
                JsonContent body = JsonContent.Create(entidad);

                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public OrdenEncabezadoEnt ConsultarOrden(long q)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarOrden?q=" + q;
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<OrdenEncabezadoEnt>().Result;
                }

                return null;
            }
        }

        public int EliminarOrden(int orden_id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EliminarOrden?orden_id=" + orden_id;

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