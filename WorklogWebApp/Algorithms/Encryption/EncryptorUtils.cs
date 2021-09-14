using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Encryption
{
    static class EncryptorUtils
    {
        public static byte[] Xor(byte[] keyBytes, byte[] textBytes)
        {
            var encryptedBytes = new byte[textBytes.Length];
            for (int i = 0; i < textBytes.Length; i++)
            {
                var keyByte = keyBytes[i % keyBytes.Length];
                var textByte = textBytes[i];
                encryptedBytes[i] = (byte)(textByte ^ keyByte);
            }

            return encryptedBytes;
        }

    }
}
