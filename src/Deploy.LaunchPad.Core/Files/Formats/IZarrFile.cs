using Deploy.LaunchPad.Core.Files.Formats;
using System.Collections.Generic;
using System.Text.Json;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface IZarrFile : IFile<Dictionary<string, byte[]>, ZarrSchema>
    {
        public JsonDocument Metadata { get; }

        public void LoadMetadata();
    }
}