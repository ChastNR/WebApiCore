using System.IO;
using System.Security.Cryptography;

namespace Tools.EncryptTool
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            var saltBytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

            using var ms = new MemoryStream();
            using var aes = new RijndaelManaged {KeySize = 256, BlockSize = 128};

            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                cs.Close();
            }

            var encryptedBytes = ms.ToArray();

            return encryptedBytes;
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            var saltBytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

            using var ms = new MemoryStream();
            using var aes = new RijndaelManaged {KeySize = 256, BlockSize = 128};

            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;

            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                cs.Close();
            }

            var decryptedBytes = ms.ToArray();

            return decryptedBytes;
        }
    }
}