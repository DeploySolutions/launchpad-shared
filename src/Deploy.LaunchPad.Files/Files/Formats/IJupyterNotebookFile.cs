using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Files
{
    public partial interface IJupyterNotebookFile : IFile<byte[], JToken>
    {
    }
}