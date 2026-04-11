using Deploy.LaunchPad.Files;
using Deploy.LaunchPad.Util.Metadata;

namespace Deploy.LaunchPad.Infra.AWS.S3
{
    public partial interface IS3FileInfo : IMustHaveFullName
    {
        IFileContent<object> Content { get; set; }
        long? FileSize { get; set; }
    }
}