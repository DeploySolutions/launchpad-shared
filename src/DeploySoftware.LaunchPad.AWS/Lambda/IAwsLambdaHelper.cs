using Amazon.Lambda;

namespace DeploySoftware.LaunchPad.AWS.Lambda
{
    public interface IAwsLambdaHelper : IAwsHelper<AmazonLambdaConfig>
    {
    }
}
