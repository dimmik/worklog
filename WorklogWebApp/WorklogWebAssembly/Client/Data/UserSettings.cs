using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Client.Data
{
    public class UserSettings
    {
        public int DefaultRecordsToShow { get; set; } = 50;
        public ShowEncryptedOptions DefaultShowEncryptedOptions { get; set; } = new ShowEncryptedOptions(decrypt: false, showEncrypted: false, key: "");
        public bool ShowCompleted { get; set; } = true;
        public CollapseOptions Collapse { get; set; } = new CollapseOptions();
    }
    public class CollapseOptions
    {
        public bool Collapse { get; set; } = false;
        public int Length { get; set; } = 120;
    }
}
