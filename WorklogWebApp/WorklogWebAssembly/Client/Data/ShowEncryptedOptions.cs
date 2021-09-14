using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Client.Data
{
    public class ShowEncryptedOptions
    {
        public bool decrypt { get; set; } = false;
        public bool showEncrypted { get; set; } = false;
        public string key { get; set; } = "";

        public ShowEncryptedOptions(bool decrypt, bool showEncrypted, string key)
        {
            this.decrypt = decrypt;
            this.showEncrypted = showEncrypted;
            this.key = key;
        }
        public ShowEncryptedOptions Clone()
        {
            return new ShowEncryptedOptions(decrypt, showEncrypted, key);
        }
    }
}
