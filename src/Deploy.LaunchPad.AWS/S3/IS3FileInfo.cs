using Deploy.LaunchPad.Files;
using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.Core.Metadata;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial interface IS3FileInfo : ILaunchPadMinimalProperties
    {
        IFileContent<object> Content { get; set; }
        long? FileSize { get; set; }
    }
}