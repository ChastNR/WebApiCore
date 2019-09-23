namespace UniversalWebApi.Helpers.EncrytionHelper
{
    public interface IEncryptionHelper
    {
        string ComputeSha256Hash(string rawData);
        byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes);
        byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes);
    }
}