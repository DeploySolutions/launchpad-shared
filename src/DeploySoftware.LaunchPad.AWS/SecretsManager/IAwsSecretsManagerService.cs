using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Domain;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public interface IAwsSecretsManagerService : ILaunchPadDomainService
    {
        public IAwsSecretsManagerHelper Helper { get; set; }
    }
}
