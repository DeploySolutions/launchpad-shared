using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.CDK
{
    public interface IAwsConfigurationHelper
    {
        IVpc GetVpc();
        string[] GetUserDataCommandsFromFile(string key);

        string GetAmiImageId(string key);

        ISubnet GetPrivateSubnetInAz1();
        string GetDeploymentUsername();

        string GetDeploymentPrincipalArn();

        string GetAwsAccountRootPrincipalArn();

        string GetTfsFileSystemId();

        string GetDeploymentAccount();
        void CdkOutput(string id, string name, string value, string description);
        ISecurityGroup GetLoadBalancerSecurityGroup();

        ISecurityGroup GetTfsSecurityGroup();

        ISecurityGroup GetBastionSecurityGroup();
        ISecurityGroup GetQCSecurityGroup();
        
        ISecurityGroup GetInternalServersSecurityGroup();
        ISecurityGroup GetDatabaseSecurityGroup();

        ISecurityGroup GetSecurityGroupByCdkOutputName(string cdkOutputName);

    }
}
