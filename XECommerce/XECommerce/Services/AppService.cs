using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XECommerce.Models;

namespace XECommerce.Services
{
    public class AppService
    {
        public async Task<Response> Login(string email, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password,
                };
                var request = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://tzecommerce.diskcode.info");
                var url = "/api/Users/Login";
                var response = await client.PostAsync(url, content);

                if(!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o contraseña incorrectos",
                    };
                }
                // se deserializa la respuesta json
                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(result);

                //repuesta desializada
                return new Response
                {
                    IsSuccess = true,
                    Message = "Login Ok",
                    Result = user,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,                
                };
            }
        }

        //public async Task<List<Product>> GetProducts()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("http://tzecommerce.diskcode.info");
        //        var url = "/api/Products";
        //        var response = await client.GetAsync(url);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }
        //        // se deserializa la respuesta json
        //        var result = await response.Content.ReadAsStringAsync();
        //        var products = JsonConvert.DeserializeObject<List<Product>>(result);
        //        return products.OrderBy(p => p.Description).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public async Task<List<T>> Get<T>(string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://tzecommerce.diskcode.info");
                var url = string.Format( "/api/{0}", controller);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                // se deserializa la respuesta json
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
