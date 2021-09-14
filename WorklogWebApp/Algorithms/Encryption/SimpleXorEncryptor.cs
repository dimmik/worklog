using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Encryption
{
    public class SimpleXorEncryptor : ISymmetricEncryptor
    {
        public string DecryptFromB64(string key, string encryptedB64Content)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encrytedBytes = Convert.FromBase64String(encryptedB64Content);
            var decryptedBytes = EncryptorUtils.Xor(keyBytes, encrytedBytes);
            var str = Encoding.UTF8.GetString(decryptedBytes);
            return str;
        }

        public string EncryptAndReturnB64(string key, string plaintextContent)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var textBytes = Encoding.UTF8.GetBytes(plaintextContent);
            byte[] encryptedBytes = EncryptorUtils.Xor(keyBytes, textBytes);
            var b64 = Convert.ToBase64String(encryptedBytes);
            return b64;
        }

    }
}
