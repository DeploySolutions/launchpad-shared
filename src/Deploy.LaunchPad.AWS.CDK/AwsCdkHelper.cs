// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.CDK
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsCdkHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.IO;

namespace Deploy.LaunchPad.AWS.CDK
{


    /// <summary>
    /// Class AwsCdkHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.CDK.IAwsCdkHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.CDK.IAwsCdkHelper" />
    public partial class AwsCdkHelper : IAwsCdkHelper
    {
        /// <summary>
        /// The stack
        /// </summary>
        protected Stack _stack;
        /// <summary>
        /// The stack props
        /// </summary>
        protected IStackProps _stackProps;
        /// <summary>
        /// The VPC
        /// </summary>
        protected IVpc _vpc;
        /// <summary>
        /// The region
        /// </summary>
        protected string _region;
        /// <summary>
        /// The deployment account identifier
        /// </summary>
        protected string _deploymentAccountId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsCdkHelper"/> class.
        /// </summary>
        protected AwsCdkHelper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsCdkHelper"/> class.
        /// </summary>
        /// <param name="stack">The stack.</param>
        /// <param name="stackProps">The stack props.</param>
        /// <param name="vpc">The VPC.</param>
        public AwsCdkHelper(Stack stack, IStackProps stackProps, IVpc? vpc = null)
        {
            _stack = stack;
            _stackProps = stackProps;
        }

        /// <summary>
        /// Initializes the specified stack.
        /// </summary>
        /// <param name="stack">The stack.</param>
        /// <param name="stackProps">The stack props.</param>
        public virtual void Initialize(Stack stack, IStackProps stackProps)
        {
            _stack = stack;
            _stackProps = stackProps;
        }

        /// <summary>
        /// Gets the VPC.
        /// </summary>
        /// <param name="vpcId">The VPC identifier.</param>
        /// <returns>IVpc.</returns>
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
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="description">The description.</param>
        /// <returns>System.String.</returns>
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
        /// <param name="key">The key.</param>
        /// <returns>System.String[].</returns>
        /// <exception cref="System.ArgumentException">Key must start with 'ec2-userdata'</exception>
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
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentException">Key must start with 'ec2-ami'</exception>
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


        /// <summary>
        /// Gets the deployment username.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetDeploymentUsername()
        {

            string userName = System.Environment.GetEnvironmentVariable("profile") ??
                   System.Environment.GetEnvironmentVariable("CDK_DEPLOY_USERNAME") ??
                   (string)_stack.Node.TryGetContext("CDK_DEPLOY_USERNAME") ??
                   (string)_stack.Node.TryGetContext("deployUserName")
            ;
            return userName;
        }

        /// <summary>
        /// Gets the aws account root principal arn.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetAwsAccountRootPrincipalArn()
        {
            return "arn:aws:iam::" + Aws.ACCOUNT_ID + ":root";
        }

        /// <summary>
        /// Gets the deployment principal arn.
        /// </summary>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Gets the deployment account.
        /// </summary>
        /// <returns>System.String.</returns>
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
