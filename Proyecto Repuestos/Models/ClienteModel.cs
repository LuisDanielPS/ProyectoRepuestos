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

        public int RegistrarCliente(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegistrarCliente";
                JsonContent body = JsonContent.Create(entidad); //Serializar
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

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
        public int EditarClienteAPI(ClienteEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EditarCliente";
                JsonContent body = JsonContent.Create(entidad); // Serializar
                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int EliminaCliente(int cliente_id)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + $"api/EliminaCliente?cliente_id={cliente_id}";

                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}