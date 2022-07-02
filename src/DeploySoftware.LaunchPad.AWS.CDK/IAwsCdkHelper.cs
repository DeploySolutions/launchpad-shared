using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.AWS.CDK
{
    public interface IAwsCdkHelper
    {

        public IVpc GetVpc(string vpcId = "");

        public string[] GetUserDataCommandsFromFile(string key);

        public string GetAmiImageId(string key);

        public string GetDeploymentUsername();

        public string GetDeploymentPrincipalArn();

        public string GetAwsAccountRootPrincipalArn();

        public string GetDeploymentAccount();

        public string CdkOutput(string id, string name, string value, string description);

        public ISecurityGroup GetSecurityGroupByCdkOutputName(string cdkOutputName);

    }
}
