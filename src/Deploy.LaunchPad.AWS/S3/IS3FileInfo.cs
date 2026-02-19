using Deploy.LaunchPad.Files;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial interface IS3FileInfo : ILaunchPadMinimalProperties, ILaunchPadObject
    {
        IFileContent<object> Content { get; set; }
        long? FileSize { get; set; }
    }
}