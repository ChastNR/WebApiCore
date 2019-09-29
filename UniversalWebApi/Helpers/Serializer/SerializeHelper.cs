using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace UniversalWebApi.Helpers.Serializer
{
    public class SerializeHelper : ISerializeHelper
    {
        public byte[] SerializeObject<T>(T t) where T : class
            => ObjectToByteArray(t);

        public T DeserializeObject<T>(byte[] byteArray) where T : class
            => (T) ByteArrayToObject(byteArray);

        public object DeserializeId(byte[] byteArray)
            => ByteArrayToObject(byteArray);

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null) return null;

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private object ByteArrayToObject(byte[] arrBytes)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(arrBytes, 0, arrBytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = (object) new BinaryFormatter().Deserialize(ms);

                return obj;
            }
        }
    }

    public class BinaryInputFormatter : InputFormatter
    {
        public BinaryInputFormatter() =>
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/octet-stream"));

        protected override bool CanReadType(Type type) => type == typeof(byte[]);

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            using (var ms = new MemoryStream(16384))
            {
                context.HttpContext.Request.Body.CopyToAsync(ms);
                return InputFormatterResult.SuccessAsync(ms.ToArray());
            }
        }
    }

    public class BinaryOutputFormatter : OutputFormatter
    {
        public BinaryOutputFormatter() =>
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/octet-stream"));

        protected override bool CanWriteType(Type type) => type == typeof(byte[]);

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var array = (byte[]) context.Object;
            return context.HttpContext.Response.Body.WriteAsync(array, 0, array.Length);
        }
    }
}