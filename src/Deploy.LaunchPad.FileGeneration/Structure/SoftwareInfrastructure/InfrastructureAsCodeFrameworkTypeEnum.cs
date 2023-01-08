using System.ComponentModel;

namespace Deploy.LaunchPad.FileGeneration.Structure
{

    public enum InfrastructureAsCodeFrameworkTypeEnum
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
