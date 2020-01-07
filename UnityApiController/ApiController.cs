using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace UnityApiController
{
    public class ApiController : IApiController
    {
        public async Task<IEnumerable<T>> GetAsync<T>(string path) where T : class
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(path);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
                return result;
            }
        }

        public async Task<T> GetAsync<T>(string path, object id) where T : class
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{path}/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(content);
                return result;
            }
        }

        public async Task<bool> PostAsync<T>(string path, T t) where T : class
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(path, content);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PutAsync<T>(string path, T t) where T : class
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(path, content);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteAsync<T>(string path, object id) where T : class
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{path}/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}