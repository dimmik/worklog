using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogDomain;
using WorklogWebAssembly.Shared;

namespace WorklogWebAssembly.Server.Auth
{
    public static class AuthProvider
    {
        public static AuthData GetAuthDataFromCookie(this HttpRequest request, string admPasswd)
        {
            var authCookie = request.Cookies["Namespace"];
            if (string.IsNullOrWhiteSpace(authCookie)) return new AuthData() { IsAuthorized = false };
            if (authCookie == $"adm:{admPasswd}") return new AuthData { IsAuthorized = true, IsAdmin = true };
            return new AuthData()
            {
                IsAdmin = false,
                IsAuthorized = true,
                NamespaceMd5 = authCookie.CreateMD5()
            };
        }


    }
}
