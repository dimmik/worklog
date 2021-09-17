using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Encryption
{
    public class AesBasedEncryptor : ISymmetricEncryptor
    {
        Random r = new Random();
        public string DecryptFromB64(string key, string encryptedB64Content)
        {
            if (string.IsNullOrWhiteSpace(encryptedB64Content)) return "";
            var parts = encryptedB64Content.Split('#');
            if (parts.Length != 2) throw new WrongEncryptedContentException();
            var (IV, b64enc) = (Convert.FromBase64String(parts[0]), parts[1]);
            var md5 = key.CreateMD5();
            var md5B = StringToByteArray(md5);
            try
            {
                string ret = AesDecrypt(md5B, b64enc, IV);
                return ret;
            } catch
            {
                throw new WrongKeyException();
            }
        }

        public string EncryptAndReturnB64(string key, string plaintextContent)
        {
            byte[] IV = new byte[16];
            r.NextBytes(IV);
            var md5 = key.CreateMD5();
            var md5B = StringToByteArray(md5);
            var aesB64 = AesEncrypt(md5B, plaintextContent, IV);
            return $"{Convert.ToBase64String(IV)}#{aesB64}";
        }

       

        private string AesDecrypt(byte[] key, string b64Encrypted, byte[] IV)
        {
            byte[] inputBytes = Convert.FromBase64String(b64Encrypted);

            AesEngine engine = new AesEngine();
            CbcBlockCipher blockCipher = new CbcBlockCipher(engine);
            PaddedBufferedBlockCipher cipher1 = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());

            KeyParameter keyParam = new KeyParameter(key);
            ParametersWithIV keyParamWithIv = new ParametersWithIV(keyParam, IV);


            cipher1.Init(false, keyParamWithIv); //Error Message thrown
            byte[] outputBytes = new byte[cipher1.GetOutputSize(inputBytes.Length)]; //cip

            int length = cipher1.ProcessBytes(inputBytes, outputBytes, 0);
            cipher1.DoFinal(outputBytes, length); //Do the final block

            string decrypted = Encoding.UTF8.GetString(outputBytes).Trim('\0');
            return decrypted;
        }

        private string AesEncrypt(byte[] key, string plaintext, byte[] IV)
        {

            byte[] inputBytes = Encoding.UTF8.GetBytes(plaintext);
            
            


            AesEngine engine = new AesEngine();
            CbcBlockCipher blockCipher = new CbcBlockCipher(engine);
            PaddedBufferedBlockCipher cipher1 = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());

            KeyParameter keyParam = new KeyParameter(key);
            ParametersWithIV keyParamWithIv = new ParametersWithIV(keyParam, IV);


            cipher1.Init(true, keyParamWithIv);
            byte[] outputBytes = new byte[cipher1.GetOutputSize(inputBytes.Length)]; //cip
            int length = cipher1.ProcessBytes(inputBytes, outputBytes, 0);
            cipher1.DoFinal(outputBytes, length); //Do the final block
            string encryptedInput = Convert.ToBase64String(outputBytes);
            return encryptedInput;

        }




        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }


}
