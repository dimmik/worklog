using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogDomain;

namespace WorklogWebAssembly.Server.Auth
{
    public static class AuthProvider
    {
        public static AuthData GetAuthData(this HttpRequest request, string admPasswd)
        {
            var authCookie = request.Cookies["Namespace"];
            if (authCookie == null) return new AuthData() { IsAuthorized = false };
            if (authCookie == $"adm:{admPasswd}") return new AuthData { IsAuthorized = true, IsAdmin = true };
            return new AuthData()
            {
                IsAdmin = false,
                IsAuthorized = true,
                NamespaceMd5 = authCookie.CreateMD5()
            };
        }
        public static string CreateMD5(this string input)
        {
            if (input == null) input = "";
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string CreateMD5(Stream input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(input);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }


        }

    }
}
