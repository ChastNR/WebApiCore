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
        //public byte[] SerializeObject<T>(T t) where T : class
        //    => ObjectToByteArray(t);

        //public T DeserializeObject<T>(byte[] byteArray) where T : class
        //    => (T) ByteArrayToObject(byteArray);

        //public object DeserializeId(byte[] byteArray)
        //    => ByteArrayToObject(byteArray);

        public byte[] ToByteArray<T>(T obj) where T : class
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public T FromByteArray<T>(byte[] data) where T : class
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
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