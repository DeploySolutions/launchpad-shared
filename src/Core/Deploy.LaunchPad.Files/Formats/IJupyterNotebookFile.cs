using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IJupyterNotebookFile : IFile<byte[], JToken>
    {
    }
}