using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models.AppRole;

namespace VaultTest
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddVault(this IConfigurationBuilder builder)
        {
            var buildConfig = builder.Build();

            IVaultClient vaultClient = VaultClientFactory.CreateVaultClient(
                new Uri(buildConfig["vault:address"]),
                new AppRoleAuthenticationInfo(
                    buildConfig["vault:roleid"], 
                    buildConfig["vault:secretid"]
                )
            );

            var vaultSecrets = vaultClient
                    .ReadSecretAsync(buildConfig["vault:path"])
                    .Result.Data.Select(x => KeyValuePair.Create($"vault:{x.Key}", x.Value.ToString()));
         
            return builder.AddInMemoryCollection(vaultSecrets);
        }
    }
}
