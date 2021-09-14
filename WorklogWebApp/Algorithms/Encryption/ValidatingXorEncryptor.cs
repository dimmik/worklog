using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Encryption
{
    public class ValidatingXorEncryptor : ISymmetricEncryptor
    {
        private readonly Random r = new Random((int)(DateTimeOffset.Now.Ticks % int.MaxValue));
        public int saltSize { get; set; } = 4;
        public string DecryptFromB64(string key, string encryptedB64Content)
        {
            if (string.IsNullOrWhiteSpace(encryptedB64Content)) throw new WrongEncryptedContentException();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var parts = encryptedB64Content.Split('#');
            if (parts.Length != 3) throw new WrongEncryptedContentException();
            var salt = parts[0];
            var saltedKeyFromMsgMd5 = parts[1];
            var saltedKeyMd5 = GetSaltedKeyMd5(key, salt);
            if (saltedKeyFromMsgMd5 != saltedKeyMd5) throw new WrongKeyException();
            var b64 = parts[2];
            byte[] encrytedBytes = Convert.FromBase64String(b64);
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
            string salt = GenerateSalt();
            string saltedKeyMd5 = GetSaltedKeyMd5(key, salt);
            return $"{salt}#{saltedKeyMd5}#{b64}";
        }

        private static string GetSaltedKeyMd5(string key, string salt)
        {
            string saltedKey = $"{key}-{salt}";
            string saltedKeyMd5 = saltedKey.CreateMD5();
            return saltedKeyMd5;
        }

        private string GenerateSalt()
        {
            byte[] saltBuff = new byte[saltSize];
            r.NextBytes(saltBuff);
            string salt = Convert.ToBase64String(saltBuff);
            return salt;
        }
    }
}
