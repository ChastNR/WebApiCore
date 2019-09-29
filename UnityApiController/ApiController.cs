using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace UnityApiController
{
    public class ApiController //: IApiController
    {
        private readonly string _uri;

        public ApiController() => _uri = "http://localhost:5000";
        public ApiController(string uri) => _uri = uri;
    }
}