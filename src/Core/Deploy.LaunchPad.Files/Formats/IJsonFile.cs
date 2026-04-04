using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IJsonFile : IFile<JToken, JToken>
    {
    }
}