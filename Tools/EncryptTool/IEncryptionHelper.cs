namespace Tools.EncryptTool
{
    public interface IEncryptionHelper
    {
        byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes);
        
        byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes);
    }
}