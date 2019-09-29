namespace UniversalWebApi.Helpers.Serializer
{
    public interface ISerializeHelper
    {
        byte[] SerializeObject<T>(T t) where T : class;

        T DeserializeObject<T>(byte[] byteArray) where T : class;

        object DeserializeId(byte[] byteArray);
    }
}