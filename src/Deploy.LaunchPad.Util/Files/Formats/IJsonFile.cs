using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Util.Files
{
    public partial interface IJsonFile : IFile<JToken, JToken>
    {
    }
}