using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Domain.SoftwareApplications
{
    public partial interface ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
    {
        public string HangfireDatabaseConnectionString { get; set; }

        public string DefaultDatabaseConnectionString { get; set; }


        public TSecretProvider SecretProvider { get; }

    }
}
