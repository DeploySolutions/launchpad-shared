using Abp.Dependency;
using Amazon;
using Amazon.SecretsManager;
using Castle.Core.Logging;
using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public partial class AwsSecretsManagerHelperFactory : AwsHelperBase, ISingletonDependency
    {

        public virtual AwsSecretsManagerHelper Create(
            ILogger logger,
            string regionEndpointName = DefaultRegionEndpointName, 
            string localAwsProfileName = DefaultLocalAwsProfileName, 
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            AwsSecretsManagerHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            Region = GetRegionEndpoint(regionEndpointName);
            AwsProfileName = localAwsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                var secretClient = GetSecretClient(Region, AwsProfileName);
                helper = new AwsSecretsManagerHelper(logger, regionEndpointName, secretClient, AwsProfileName);
            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        helper = IocManager.Instance.Resolve<AwsSecretsManagerHelper>();
                        logger.Debug("AwsSecretsManagerHelper was registered; returning the resolved instance.");
                    }
                    catch (ComponentNotFoundException)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        var secretClient = GetSecretClient(Region);
                        helper = new AwsSecretsManagerHelper(logger, regionEndpointName, secretClient);
                        logger.Debug("AwsSecretsManagerHelper was not registered; returning a new instance.");
                    }
                }
                else
                {

                }
                // after all that, load the helper
                if (helper == null)
                {
                    // create the helper using the AWS credentials resolution pattern.
                    // Since we are not using local profile here, we presumably load from EC2 role or environment
                    var secretClient = GetSecretClient(Region);
                    helper = new AwsSecretsManagerHelper(logger, regionEndpointName, secretClient);
                    logger.Debug("AwsSecretsManagerHelper was null; returning a new instance.");
                }
            }
            
            return helper;
        }


        protected virtual AmazonSecretsManagerClient GetSecretClient(RegionEndpoint region, string profileName = "")
        {

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, profileName));
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, profileName));

            AmazonSecretsManagerClient client = null;
            try
            {
                if(!string.IsNullOrEmpty(profileName))
                {
                    var credentials = GetAwsCredentialsFromNamedLocalProfile(profileName);
                    if (credentials != null)
                    {
                        client = new AmazonSecretsManagerClient(credentials, region);
                    }
                }
            }
            catch (AmazonSecretsManagerException smEx)
            {
                Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials, smEx.Message));
            }
            if (client == null) // try to load using local environment or EC2 information
            {
                client = new AmazonSecretsManagerClient(region);
                Logger.Info(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_SecretClient_IsNull);
            }
            return client;

        }
    }
}
