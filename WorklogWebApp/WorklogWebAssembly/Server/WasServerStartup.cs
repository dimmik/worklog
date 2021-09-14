using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorklogDomain;
using WorklogStorage;
using WorklogStorage.InMemoryStorage;
using WorklogStorage.MongoDb;
using WorklogWebApp.Exceptions;

namespace WorklogWebAssembly.Server
{
    public class WasServerStartup
    {
        public WasServerStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SetUpStorage(services);
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        private void SetUpStorage(IServiceCollection services)
        {
            IWorklogStorage mongoDbStorage() => new MongoDbStorage(
                Configuration.GetValue<string>("MongoDbUrl"),
                Configuration.GetValue<string>("MongoDbUsername"),
                Configuration.GetValue<string>("MongoDbPassword")
                );
            IWorklogStorage inMemStorage() => new PersistentLocalStorage("./test-inmem-nbs.json");
            var storages = new Dictionary<string, Func<IWorklogStorage>>()
            {
                {"local", inMemStorage},
                {"mongo", mongoDbStorage}
            };
            var storageType = Configuration.GetValue("StorageType", "local");
            services.AddSingleton(storages.ContainsKey(storageType) ? storages[storageType]() : storages["local"]());        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<HttpExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
