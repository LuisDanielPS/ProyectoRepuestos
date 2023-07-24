using Proyecto_Repuestos.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace Proyecto_Repuestos.Models
{
    public class RolesModel
    {

        public List<RolesEnt> ConsultarRoles()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarRoles";
                    HttpResponseMessage resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<RolesEnt>>().Result;
                    }
                    else
                    {

                        return new List<RolesEnt>();
                    }
                }
            }
            catch (Exception ex)
            {

                return new List<RolesEnt>();
            }
        }
    }
}