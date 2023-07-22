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
        }
    }
 





