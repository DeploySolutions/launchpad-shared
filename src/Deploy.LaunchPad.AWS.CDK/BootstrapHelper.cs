using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Deploy.LaunchPad.AWS.CDK
{


    public class BootstrapHelper : IAwsConfigurationHelper
    {
        protected Stack _stack;
        protected IVpc _vpc;

        public BootstrapHelper(Stack s)
        {
            _stack = s;
        }

        public IVpc GetVpc()
        {
            if (_vpc == null)
            {
                var vpcId = (string)_stack.Node.TryGetContext("vpc-id");

                _vpc = Vpc.FromLookup(_stack, "egs-vpc", new VpcLookupOptions
                {
                    VpcId = vpcId
                });
            }
            return _vpc;
        }

        public void CdkOutput(string id, string name, string value, string description)
        {
            new CfnOutput(_stack, id, new CfnOutputProps
            {
                ExportName = name,
                Value = value,
                Description = description
            });
        }

        /// <summary>
        /// Returns a string array of userdata commands from a given text file path
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] GetUserDataCommandsFromFile(string key) 
        {
            string commandsTextFilePath;
            if (key.StartsWith("ec2-userdata"))
            {
                commandsTextFilePath = (string)_stack.Node.TryGetContext(key);
            } else
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
        public string GetAmiImageId(string key)
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
        /// Returns the File System ID for the TFS file system
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTfsFileSystemId()
        {
            string tfsPrefix = (string)_stack.Node.TryGetContext("tfs-prefix");
            return Fn.ImportValue(string.Concat(tfsPrefix,"-tfs-filesystemid"));
        }
        public ISubnet GetPrivateSubnetInAz1()
        {
            var privateSubnetSelection = new SubnetSelection();
            privateSubnetSelection.Subnets = GetVpc().PrivateSubnets;
            var az1SubnetAttributes = new PrivateSubnetAttributes();
            az1SubnetAttributes.AvailabilityZone = "cac1-az1";
            az1SubnetAttributes.SubnetId = (string)_stack.Node.TryGetContext("EgsImgPipeVpc/egs_imgpipe_vpc/egs_private_subnetSubnet1"); // TODO: make this dynamic lookup

            var az1PrivateSubnet = Subnet.FromSubnetAttributes(_stack, "az1-private-subnet", az1SubnetAttributes);
            return az1PrivateSubnet;
        }

        public string GetDeploymentUsername()
        {
            
            string userName = System.Environment.GetEnvironmentVariable("profile") ??
                   System.Environment.GetEnvironmentVariable("CDK_DEPLOY_USERNAME") ??
                   (string)_stack.Node.TryGetContext("CDK_DEPLOY_USERNAME") ??
                   (string)_stack.Node.TryGetContext("deployUserName")
            ;
            return userName;
        }

        public string GetAwsAccountRootPrincipalArn()
        {
            return "arn:aws:iam::" + Aws.ACCOUNT_ID + ":root";           
        }

        public string GetDeploymentPrincipalArn()
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
            else { 
                kmsPrincipal = "arn:aws:iam::" +
                                    GetDeploymentAccount() +
                                    ":role/" +
                                    role;
            }
            return kmsPrincipal;
        }

        public string GetDeploymentAccount()
        {
            string account = System.Environment.GetEnvironmentVariable("CDK_DEPLOY_ACCOUNT") ??
                    System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT") ??
                    (string)_stack.Node.TryGetContext("CDK_DEPLOY_ACCOUNT");
            return account;
        }

        public ISecurityGroup GetInternalServersSecurityGroup()
        {
            var sgId = Fn.ImportValue("cdk-out-internal-servers-sgId");
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, "sg-internal-servers", sgId);
            }
            return null;
        }

        public ISecurityGroup GetTfsSecurityGroup()
        {
            var sgId = Fn.ImportValue("cdk-out-tfs-sgId");
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, "sg-tfs", sgId);
            }
            return null;
        }

        public ISecurityGroup GetLoadBalancerSecurityGroup()
        {
            var sgId = Fn.ImportValue("cdk-out-loadbalancer-sgId");
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, "sg-loadbalancer", sgId);
            }
            return null;
        }
        public ISecurityGroup GetBastionSecurityGroup()
        {
            var sgId = Fn.ImportValue("cdk-out-bastion-sgId");
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, "sg-bastion", sgId);
            }
            return null;
        }

        public ISecurityGroup GetQCSecurityGroup()
        {
            var sgId = Fn.ImportValue("cdk-out-qc-sgId");
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, "sg-qc", sgId);
            }
            return null;
        }

        public ISecurityGroup GetDatabaseSecurityGroup()
        {
            var sgId = Fn.ImportValue("cdk-out-internal-servers-sgId");
            if (!string.IsNullOrEmpty(sgId))
            {
                return SecurityGroup.FromSecurityGroupId(_stack, "cdk-out-database-sgId", sgId);
            }
            return null;
        }

        public ISecurityGroup GetSecurityGroupByCdkOutputName(string name)
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
