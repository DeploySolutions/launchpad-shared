using Deploy.LaunchPad.Util.Files;
using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial interface IS3FileInfo : ILaunchPadMinimalProperties, ILaunchPadObject
    {
        IFileContent<object> Content { get; set; }
        long? FileSize { get; set; }
    }
}