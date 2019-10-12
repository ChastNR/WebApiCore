using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tools.Serializer
{
    public class SerializeHelper : ISerializeHelper
    {
        public byte[] ToByteArray<T>(T obj) where T : class
        {
            using var ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray();
        }

        public T FromByteArray<T>(byte[] data) where T : class
        {
            using var ms = new MemoryStream(data);
            return (T) new BinaryFormatter().Deserialize(ms);
        }
    }
}