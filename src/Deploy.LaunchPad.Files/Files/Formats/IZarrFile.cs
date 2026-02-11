
using System.Collections.Generic;
using Deploy.LaunchPad.Files.Formats;

using System.Text.Json;

namespace Deploy.LaunchPad.Files
{
    public partial interface IZarrFile : IFile<Dictionary<string, byte[]>, ZarrFileSchema>
    {
        public JsonDocument Metadata { get; }

        //public void LoadMetadata();
    }
}