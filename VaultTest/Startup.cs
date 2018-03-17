using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.Token;

namespace VaultTest
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        { }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IVaultClient vaultClient = VaultClientFactory.CreateVaultClient(
                new Uri("http://localhost:8200"), 
                new TokenAuthenticationInfo("73045cbb-af51-2dd9-e2aa-031292988586"));


            var secret = vaultClient.ReadSecretAsync("secret/test").Result;

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(secret.Data["test"].ToString());
            });
        }
    }
}
