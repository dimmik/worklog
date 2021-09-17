using Algorithms.Encryption;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Client
{
    public class WasClientMain
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<AppState>();

//            var enc = new ValidatingXorEncryptor();
            var enc = new AesBasedEncryptor();
            builder.Services.AddSingleton<ISymmetricEncryptor>(enc);

            await builder.Build().RunAsync();
        }
    }
}
