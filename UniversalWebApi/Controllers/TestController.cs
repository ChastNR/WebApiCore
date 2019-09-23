using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniversalWebApi.Helpers.Serializer;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        protected readonly ISerializeHelper _serializeHelper;

        public TestController(ISerializeHelper serializeHelper)
        {
            _serializeHelper = serializeHelper;
        }

        [HttpPost("test")]
        public async Task<string> Send([FromBody] string data)
        {
            using( var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "/api/test/serializeData"))
                {
                    using (var httpContent = CreateHttpContent(data))
                    {
                        request.Content = httpContent;

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            var result = response.EnsureSuccessStatusCode();

                            return result.ToString();
                        }
                    }
                }

            } 
        }

        [HttpPost("serializeData")]
        public byte[] EncryptData(string data)
        {
            var serializedData = _serializeHelper.ObjectToByteArray(data);

            return serializedData;
        }

        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }
    }
}