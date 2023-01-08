using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.IO;

namespace Deploy.LaunchPad.AWS.CDK
{


    public partial class AwsCdkHelper : IAwsCdkHelper
    {
        protected Stack _stack;
        protected IStackProps _stackProps;
        protected IVpc _vpc;
        protected string _region;
        protected string _deploymentAccountId;

        protected AwsCdkHelper()
        {
        }

        public AwsCdkHelper(Stack stack, IStackProps stackProps, IVpc? vpc = null)
        {
            _stack = stack;
            _stackProps = stackProps;
        }

        public virtual void Initialize(Stack stack, IStackProps stackProps)
        {
            _stack = stack;
            _stackProps = stackProps;
        }

        public virtual IVpc GetVpc(string vpcId = "")
        {
            Console.WriteLine("AwsCdkHelper.GetVpc() => vpcId " + vpcId);
            if (_vpc == null)
            {
                // try to load the VPC via its id
                //
                if (string.IsNullOrEmpty(vpcId))
                {
                    vpcId = (string)_stack.Node.TryGetContext("vpc-id");
                    Console.WriteLine("AwsCdkHelper.GetVpc() => _vpc was null, provided vpc id was empty, attempting to load from cdk.context.json, vpc-id is:" + vpcId);
                }
                if (!string.IsNullOrEmpty(vpcId))
                {
                    Console.WriteLine("AwsCdkHelper.GetVpc() => vpc id is no longer empty, attempting to load _vpc Vpc.FromLookup with id:" + vpcId);
                    _vpc = Vpc.FromLookup(_stack, vpcId, new VpcLookupOptions
                    {
                        VpcId = vpcId
                    });
                }

                if (_vpc != null)
                {

                    Console.WriteLine("AwsCdkHelper.GetVpc() => _vpc is not null any longer, its id is " + _vpc.VpcId);
                }
            }
            return _vpc;
        }

        /// <summary>
        /// Utility method to generate CDK Output
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public virtual string CdkOutput(string id, string name, string value, string description)
        {
            var output = new CfnOutput(_stack, id, new CfnOutputProps
            {
                ExportName = name,
                Value = value,
                Description = description
            });
            return output.ToString();
        }

        /// <summary>
        /// Returns a string array of userdata commands from a given text file path
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string[] GetUserDataCommandsFromFile(string key)
        {
            string commandsTextFilePath;
            if (key.StartsWith("ec2-userdata"))
            {
                commandsTextFilePath = (string)_stack.Node.TryGetContext(key);
            }
            else
            {
                throw new ArgumentException("Key must start with 'ec2-userdata'");
            }
            return File.ReadAllLines(commandsTextFilePath);
        }

        /// <summary>
        /// Returns the AMI image ID for a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string GetAmiImageId(string key)
        {
            string amiImageId = String.Empty;
            if (key.StartsWith("ec2-ami"))
            {
                amiImageId = (string)_stack.Node.TryGetContext(key);
            }
            else
            {
                throw new ArgumentException("Key must start with 'ec2-ami'");
            }
            return amiImageId;
        }


        public virtual string GetDeploymentUsername()
        {

            string userName = System.Environment.GetEnvironmentVariable("profile") ??
                   System.Environment.GetEnvironmentVariable("CDK_DEPLOY_USERNAME") ??
                   (string)_stack.Node.TryGetContext("CDK_DEPLOY_USERNAME") ??
                   (string)_stack.Node.TryGetContext("deployUserName")
            ;
            return userName;
        }

        public virtual string GetAwsAccountRootPrincipalArn()
        {
            return "arn:aws:iam::" + Aws.ACCOUNT_ID + ":root";
        }

        public virtual string GetDeploymentPrincipalArn()
        {
            var kmsPrincipal = string.Empty;
            var role = (string)_stack.Node.TryGetContext("deploy-components-role-name");
            if (string.IsNullOrEmpty(role))
            {
                kmsPrincipal = "arn:aws:iam::" +
                                    GetDeploymentAccount() +
                                    ":user/" +
                                    GetDeploymentUsername();
            }
            else
            {
                kmsPrincipal = "arn:aws:iam::" +
                                    GetDeploymentAccount() +
                                    ":role/" +
                                    role;
            }
            return kmsPrincipal;
        }

        public virtual string GetDeploymentAccount()
        {
            string account = System.Environment.GetEnvironmentVariable("CDK_DEPLOY_ACCOUNT") ??
                    System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT") ??
                    (string)_stack.Node.TryGetContext("CDK_DEPLOY_ACCOUNT");
            return account;
        }

        /// <summary>
        /// Gets a EC2 Security Group from the given CDK Output value. 
        /// </summary>
        /// <param name="name">The CDK Output security group id</param>
        /// <returns>An object reference to the EC2 security group; null if group doesn't exist or the CDK Output value doesn't reference an existing group.</returns>
        public virtual ISecurityGroup GetSecurityGroupByCdkOutputName(string name)
        {
            var sgId = Fn.ImportValue(name);
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, name, sgId);
            }
            return null;
        }
    }
}
