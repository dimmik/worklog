using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Encryption
{
    public interface ISymmetricEncryptor
    {
        string EncryptAndReturnB64(string key, string plaintextContent);
        string DecryptFromB64(string key, string encryptedB64Content);
    }
}
