using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace UnityApiController
{
    public class ApiController : IApiController
    {
        private readonly Uri _baseAddress = new Uri("http://localhost:5000");
        public ApiController() { }
        public ApiController(string uri) => _baseAddress = new Uri(uri);

        public IEnumerable<T> Get<T>() where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var response = client.GetAsync($"api/{typeof(T).Name}").Result;
                var result = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }

        public T Get<T>(object id) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var response = client.GetAsync($"api/{typeof(T).Name}/{id}").Result;
                var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }

        public bool Post<T>(T t) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                var result = client.PostAsync($"api/{typeof(T).Name}", content).Result;
                return result.IsSuccessStatusCode;
            }
        }

        public bool Put<T>(T t) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                var result = client.PutAsync($"api/{typeof(T).Name}", content).Result;
                return result.IsSuccessStatusCode;
            }
        }

        public bool Delete<T>(object id) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var result = client.DeleteAsync($"api/{typeof(T).Name}/{id}").Result;
                return result.IsSuccessStatusCode;
            }
        }
    }
}