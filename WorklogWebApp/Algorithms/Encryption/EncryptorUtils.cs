using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Encryption
{
    public static class EncryptorUtils
    {
        public static byte[] Xor(byte[] keyBytes, byte[] textBytes)
        {
            if (keyBytes == null || keyBytes.Length == 0) return textBytes;
            if (textBytes == null || textBytes.Length == 0) return new byte[0];

            var encryptedBytes = new byte[textBytes.Length];
            for (int i = 0; i < textBytes.Length; i++)
            {
                var keyByte = keyBytes[i % keyBytes.Length];
                var textByte = textBytes[i];
                encryptedBytes[i] = (byte)(textByte ^ keyByte);
            }

            return encryptedBytes;
        }
        public static byte[] Xor(string key, string text)
        {
            if (text == null || text == "") return new byte[0];
            if (key == null) key = "";

            return Xor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(text));
        }

    }
}
