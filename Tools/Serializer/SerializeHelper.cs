using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tools.Serializer
{
    public class SerializeHelper : ISerializeHelper
    {
        public byte[] ToByteArray<T>(T obj) where T : class
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public T FromByteArray<T>(byte[] data) where T : class
        {
            if (data == null)
                return default;
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream(data);
            var obj = bf.Deserialize(ms);
            return (T)obj;
        }
    }
}