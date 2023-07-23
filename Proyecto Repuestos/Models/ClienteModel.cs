using Newtonsoft.Json;
using Proyecto_Repuestos.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_Repuestos.Models
{
    public class ClienteModel
    {

        public List<ClienteEnt> ConsultarClientes()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarClientes";
                    HttpResponseMessage resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<ClienteEnt>>().Result;
                    }

                    return new List<ClienteEnt>();
                }
            }
            catch (Exception ex)
            {

                return new List<ClienteEnt>();
            }
        }

    }
}