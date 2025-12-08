using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface IFrictionlessFileSchema : IFile<JToken, JToken>
    {
        bool ShouldGenerateDataPackage { get; set; }
    }
}