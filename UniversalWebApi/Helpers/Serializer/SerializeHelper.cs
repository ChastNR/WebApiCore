using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UniversalWebApi.Helpers.Serializer
{
    public class SerializeHelper : ISerializeHelper
    {
        public byte[] SerializeObject<T>(T t) where T : class
            => ObjectToByteArray(t);

        public T DeserializeObject<T>(byte[] byteArray) where T : class
            => (T)ByteArrayToObject(byteArray);

        public object DeserializeId(byte[] byteArray)
            => ByteArrayToObject(byteArray);

        public byte[] ObjectToByteArray(object obj)
        {
            if (obj == null) return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);

                var test = ms.ToArray();

                return ms.ToArray();
            }
        }

        public object ByteArrayToObject(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(arrBytes, 0, arrBytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = (object)binForm.Deserialize(ms);

                return obj;
            }
        }
    }
}