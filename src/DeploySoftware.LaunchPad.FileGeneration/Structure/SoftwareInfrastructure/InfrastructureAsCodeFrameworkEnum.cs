using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    
    public enum InfrastructureAsCodeFrameworkEnum
    {
        [Description("Terraform")]
        Terraform = 0,
        [Description("AWS Cloud Development Kit")]
        AWS_CDK = 1,
        [Description("AWS Cloud Formation")]
        AWS_CloudFormation = 2,
        [Description("Azure Resource Manager (ARM) templates")]
        Azure_ARM = 2
    }
}
