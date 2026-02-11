using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Files
{
    public partial interface IJsonFile : IFile<JToken, JToken>
    {
    }
}