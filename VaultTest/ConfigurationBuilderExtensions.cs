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

            if (buildConfig.GetSection("settings:useVault").Get<bool>())
            {
                try
                {
                    IVaultClient vaultClient = VaultClientFactory.CreateVaultClient(
                        new Uri(buildConfig["vault:address"]),
                        new AppRoleAuthenticationInfo(
                            buildConfig["vault:roleid"],
                            buildConfig["vault:secretid"]
                        )
                    );

                    var vaultSecrets = vaultClient
                            .ReadSecretAsync(buildConfig["vault:path"])
                            .Result.Data.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString()));

                    return builder.AddInMemoryCollection(vaultSecrets);
                }
                catch (Exception ex)
                {
                    throw new Exception("Vault configuration failed: " + ex.Message);
                }
            }
            else
            {
                return builder;
            }
        }
    }
}
