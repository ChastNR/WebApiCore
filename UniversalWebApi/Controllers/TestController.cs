using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlRepository.Interfaces;
using UniversalWebApi.Helpers.Serializer;
using UniversalWebApi.Models;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly string _rootUrl;
        private readonly IDataRepository _dataRepository;
        private readonly ISerializeHelper _serializeHelper;

        public TestController(ISerializeHelper serializeHelper, IDataRepository dataRepository)
        {
            _rootUrl = "http://localhost:5000";
            _serializeHelper = serializeHelper;
            _dataRepository = dataRepository;
        }

        [HttpGet("tt")]
        public async Task<IEnumerable<User>> Get()
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetByteArrayAsync($"{_rootUrl}/api/user/get");
                

                var result = _serializeHelper.DeserializeObject<IEnumerable<User>>(content);
                
                return result;
            }
        }
        
        
        
        
//        [HttpGet("testGet")]
//        public async Task<IEnumerable<User>> Get()
//        {
//            var result = await GetRequest<User>($"{_rootUrl}/api/{typeof(User).Name}/get");
//            return result;
//        }

//        [HttpGet("testGet/{id}")]
//        public async Task<User> Get(int id)
//        {
//            var result = await GetRequest<User>($"{_rootUrl}/api/{typeof(User).Name}/get", id);
//            return result;
//        }
//
//        [HttpPost("testAdd")]
//        public async Task<IActionResult> Add([FromBody] User user)
//        {
//            var result = await PostRequest($"{_rootUrl}/api/test/testAdd2", user);
//
//            return result ? (IActionResult) Ok("Nice") : BadRequest();
//        }
//
//        private async Task<IEnumerable<T>> GetRequest<T>(string url) where T : class
//        {
//            using (var client = new HttpClient())
//            {
//                var content = await client.GetStringAsync(url);
//                return JsonConvert.DeserializeObject<List<T>>(content);
//            }
//        }
//
//        private async Task<T> GetRequest<T>(string url, int id) where T : class
//        {
//            using (var client = new HttpClient())
//            {
//                var content = await client.GetStringAsync($"{url}/{id}");
//                return JsonConvert.DeserializeObject<T>(content);
//            }
//        }

//        private async Task<bool> PostRequest<T>(string url, T t) where T : class
//        {
//            using (var client = new HttpClient())
//            {
//                client.DefaultRequestHeaders.Accept.Add(
//                    new MediaTypeWithQualityHeaderValue("application/octet-stream"));
//
//                var serializedObject = _serializeHelper.SerializeObject(t);
//
//                //var content = new ByteArrayContent(serializedObject);
//                //content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
//
//                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
//                {
//                    request.Content = new ByteArrayContent(serializedObject);
//                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
//
//                    var response = await client.PostAsync(request.RequestUri, request.Content);
//
//                    return response.IsSuccessStatusCode;
//                }
//
//                //var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
//                //var response = await client.PostAsync(url, content);
//
//                //var response = await client.SendAsync(request);
//                //return response.IsSuccessStatusCode;
//
//                //return response.IsSuccessStatusCode;
//            }
//        }

//        [HttpPost("testAdd2")]
//        public async Task<IActionResult> Add([FromBody] byte[] byteArray)
//        {
//            try
//            {
//                var result = _serializeHelper.DeserializeObject<User>(byteArray);
//                await _dataRepository.InsertAsync(result);
//                return Ok("Added successfully!");
//            }
//            catch (Exception e)
//            {
//                return Json(e.Message);
//            }
//        }
    }
}