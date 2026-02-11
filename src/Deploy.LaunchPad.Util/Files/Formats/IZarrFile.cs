using Deploy.LaunchPad.Util.Files.Formats;
using System.Collections.Generic;
using System.Text.Json;

namespace Deploy.LaunchPad.Util.Files
{
    public partial interface IZarrFile : IFile<Dictionary<string, byte[]>, ZarrFileSchema>
    {
        public JsonDocument Metadata { get; }

        public void LoadMetadata();
    }
}