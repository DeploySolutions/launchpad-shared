using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class ZarrFile : FileBase<Dictionary<string, byte[]>>, IZarrFile
    {
        public virtual JsonDocument Metadata { get; private set; } // Represents the metadata of the Zarr file

        public override string Extension => ".zarr";

        public ZarrFile()
        {
        }

        public void LoadMetadata()
        {
            string metadataPath = Path.Combine(Location.RootUri.LocalPath, ".zarray");
            if (File.Exists(metadataPath))
            {
                string metadataContent = File.ReadAllText(metadataPath);
                Metadata = JsonDocument.Parse(metadataContent);
            }

            FileInfo fileInfo = new FileInfo(Location.RootUri.LocalPath);
            Size = Directory.GetFiles(Location.RootUri.LocalPath, "*", SearchOption.AllDirectories)
                                .Sum(f => new FileInfo(f).Length);
            CreationTime = fileInfo.CreationTime;
            LastModificationTime = fileInfo.LastWriteTime;
        }
    }
}
