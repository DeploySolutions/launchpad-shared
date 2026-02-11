using Newtonsoft.Json.Linq;

namespace Deploy.LaunchPad.Util.Files
{
    public partial interface IJupyterNotebookFile : IFile<byte[], JToken>
    {
    }
}