
using System.Collections.Generic;

using System.Text.Json;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IZarrFile : IFile<Dictionary<string, byte[]>, ZarrFileSchema>
    {
        public JsonDocument Metadata { get; }

        //public void LoadMetadata();
    }
}