namespace Tools.Serializer
{
    public interface ISerializeHelper
    {
        byte[] ToByteArray<T>(T obj) where T : class;
        
        T FromByteArray<T>(byte[] data) where T : class;
    }
}