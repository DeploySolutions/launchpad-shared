using Amazon.Lambda;

namespace Deploy.LaunchPad.AWS.Lambda
{
    public interface IAwsLambdaHelper : IAwsHelper<AmazonLambdaConfig>
    {
    }
}
