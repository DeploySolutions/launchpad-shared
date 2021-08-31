using Abp.Dependency;
using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class AwsSecretProvider<TSecretVault> : SecretProviderBase<TSecretVault>, ISingletonDependency
        where TSecretVault: SecretVaultBase, new()
    {

        public AwsSecretProvider() :base()
        {       
        }
    }
}
