using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface IFrictionlessSchemaFile : IFile<JToken>
    {
        bool ShouldGenerateDataPackage { get; set; }
    }
}