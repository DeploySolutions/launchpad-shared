using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public  interface IAwsSecretsManagerHelper: ISecretHelper, IAwsHelper
    {
    }
}
