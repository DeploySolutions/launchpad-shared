using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IFrictionlessFileSchema : IFile<JToken, JToken>
    {
        bool ShouldGenerateDataPackage { get; set; }
    }
}