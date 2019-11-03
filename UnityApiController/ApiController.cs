using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityApiController
{
    public class ApiController : IApiController
    {
        private readonly Uri _baseAddress = new Uri("http://localhost:5000");
        public ApiController() { }
        public ApiController(string uri) => _baseAddress = new Uri(uri);

        public async Task<IEnumerable<T>> Get<T>() where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var response = await client.GetAsync($"api/{typeof(T).Name}");
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
                return result;
            }
        }

        public async Task<T> Get<T>(object id) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var response = await client.GetAsync($"api/{typeof(T).Name}/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(content);
                return result;
            }
        }

        public async Task<bool> Post<T>(T t) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"api/{typeof(T).Name}", content);
                return result.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Put<T>(T t) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                var result = await client.PutAsync($"api/{typeof(T).Name}", content);
                return result.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Delete<T>(object id) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var result = await client.DeleteAsync($"api/{typeof(T).Name}/{id}");
                return result.IsSuccessStatusCode;
            }
        }
    }
}