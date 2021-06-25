using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.AWS
{
    public abstract partial class AwsHelperBase
    {
        public AWSCredentials GetAwsCredentials(string awsProfileName)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials creds;
            if (chain.TryGetAWSCredentials(awsProfileName, out creds))
            {
                Console.WriteLine("AWS credentials created");
            }
            return creds;
        }
    }
}
