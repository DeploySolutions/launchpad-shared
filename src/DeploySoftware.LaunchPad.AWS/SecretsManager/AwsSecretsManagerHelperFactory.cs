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
            TryGetRegionEndpoint(regionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = localAwsProfileName;
            ShouldUseLocalAwsProfile = shouldUseLocalAwsProfile;
            
            // attempt to load the registered instance, if any
            //if (IocManager.Instance != null)
            //{
            //    try
            //    {
            //        if(IocManager.Instance.IsRegistered<AwsSecretsManagerHelper>())
            //        {
            //            helper = IocManager.Instance.Resolve<AwsSecretsManagerHelper>();
            //            logger.Debug("AwsSecretsManagerHelper was registered; returning the resolved instance.");
            //        }
            //    }
            //    catch (ComponentNotFoundException)
            //    {
            //        logger.Debug("AwsSecretsManagerHelper claims it was registered but could not be resolved; returning a new instance.");
            //    }
            //}
            if(helper == null)
            {
                var secretClient = GetSecretClient(Region, AwsProfileName, ShouldUseLocalAwsProfile);
                helper = new AwsSecretsManagerHelper(logger, regionEndpointName, secretClient, AwsProfileName, ShouldUseLocalAwsProfile);
            }
            
            return helper;
        }


        protected virtual AmazonSecretsManagerClient GetSecretClient(RegionEndpoint region, string awsProfileName = "", bool shouldUseLocalAwsProfile = false)
        {

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, awsProfileName));
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, awsProfileName));

            AmazonSecretsManagerClient client = null;
            try
            {
                if(shouldUseLocalAwsProfile)
                {
                    if(string.IsNullOrEmpty(awsProfileName))
                    {
                        awsProfileName = DefaultLocalAwsProfileName;
                    }
                    var credentials = GetAwsCredentialsFromNamedLocalProfile(awsProfileName);
                    if (credentials != null)
                    {
                        client = new AmazonSecretsManagerClient(credentials, region);
                    }
                }
                else    
                {
                    client = new AmazonSecretsManagerClient(region);
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
