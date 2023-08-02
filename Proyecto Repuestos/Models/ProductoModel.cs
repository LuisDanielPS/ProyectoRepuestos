using Newtonsoft.Json;
using Proyecto_Repuestos.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Web;
using System.Web.Security;
using static System.Net.WebRequestMethods;

namespace Proyecto_Repuestos.Models
{
    public class ProductoModel
    {
        public List<ProductoEnt> ConsultarProductos()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarProductos";
                    HttpResponseMessage resp = client.GetAsync(url).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
                    }
                    else
                    {
                        return new List<ProductoEnt>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<ProductoEnt>();
            }
        }

        public List<CategoriaEnt> CargarCategorias()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarCategorias";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CategoriaEnt>>().Result;
                }

                return new List<CategoriaEnt>();
            }
        }
        public List<EstadoEnt> CargarEstados()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarEstados";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<EstadoEnt>>().Result;
                }

                return new List<EstadoEnt>();
            }
        }
        public int EnviarSolicitudEditarProductoAPI(ProductoEnt entidad)
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EditarProducto";
                    JsonContent body = JsonContent.Create(entidad); // Serializar
                    HttpResponseMessage resp = client.PutAsync(url, body).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        return resp.Content.ReadFromJsonAsync<int>().Result;
                    }

                    return 0;
                }
            }
        


    }
}


