using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public class AwsSecretVault : SecretVaultBase
    {
        

        public AwsSecretVault() : base()
        {
            
        }

        public AwsSecretVault(string secretIdentifier) : base(secretIdentifier)
        {

        }

        public AwsSecretVault(string secretIdentifier, string name) : base(secretIdentifier, name)
        {

        }

        public AwsSecretVault(string secretIdentifier, string name, string fullName) : base(secretIdentifier,name, fullName)
        {

        }

    }
}
