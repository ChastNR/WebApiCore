using System.Text;
using UniversalWebApi.Helpers.EncrytionHelper;
using UniversalWebApi.Helpers.Serializer;
using Microsoft.AspNetCore.Mvc;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class EncryptController : Controller
    {
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly ISerializeHelper _serializeHelper;
        private readonly byte[] passwordBytes = Encoding.UTF8.GetBytes("Password");

        public EncryptController(IEncryptionHelper encryptionHelper, ISerializeHelper serializeHelper)
        {
            _encryptionHelper = encryptionHelper;
            _serializeHelper = serializeHelper;
        }

        [HttpGet("EncryptData")]
        public byte[] EncryptData(string data)
        {
            var serializedData = _serializeHelper.ObjectToByteArray(data);

            var encryptedData = _encryptionHelper.AES_Encrypt(serializedData, passwordBytes);

            return encryptedData;
        }

        [HttpGet("DecryptData")]
        public string DecryptData(byte[] data)
        {
            var decryptedData = _encryptionHelper.AES_Decrypt(data, passwordBytes);

            var deserializedData = (string)_serializeHelper.ByteArrayToObject(decryptedData);

            return deserializedData;
        }

        [HttpGet("test")]
        public string Test(string data)
        {
            var serializedData = _serializeHelper.ObjectToByteArray(data);

            var encryptedData = _encryptionHelper.AES_Encrypt(serializedData, passwordBytes);

            var decryptedData = _encryptionHelper.AES_Decrypt(encryptedData, passwordBytes);

            var deserializedData = $"{(string)_serializeHelper.ByteArrayToObject(decryptedData)} + bla";

            return deserializedData;
        }
    }
}