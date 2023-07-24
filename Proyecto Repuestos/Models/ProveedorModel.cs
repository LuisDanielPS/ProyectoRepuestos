using Proyecto_Repuestos.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace Proyecto_Repuestos.Models
{
    public class ProveedorModel
    {

        public List<ProveedoresEnt> ConsultarProveedores()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarProveedores";
                    HttpResponseMessage resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<ProveedoresEnt>>().Result;
                    }
                    else
                    {

                        return new List<ProveedoresEnt>();
                    }
                }
            }
            catch (Exception ex)
            {

                return new List<ProveedoresEnt>();
            }
        }
    }
}