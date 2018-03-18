using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models.Token;

namespace VaultTest
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddVault(this IConfigurationBuilder builder)
        {
            var buildConfig = builder.Build();

            IVaultClient vaultClient = VaultClientFactory.CreateVaultClient(
                new Uri(buildConfig["vault:address"]),
                new TokenAuthenticationInfo(buildConfig["vault:token"])
            );

            return builder.AddInMemoryCollection(
                vaultClient
                    .ReadSecretAsync(buildConfig["vault:path"])
                    .Result.Data.Select(
                        x => KeyValuePair.Create(x.Key, x.Value.ToString())
                    )
            );
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder => builder.AddVault())
                .Build();
    }
}
