using Algorithms.MD5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class StringUtils
    {
        public static string CreateMD5(this string input)
        {
            if (input == null) input = "";
            var md5 = new MD5.MD5();
            md5.Value = input;
            return md5.FingerPrint;
            
        }
    }
}
