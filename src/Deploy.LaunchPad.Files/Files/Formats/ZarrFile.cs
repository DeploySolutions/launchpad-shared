
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Files.Formats;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Deploy.LaunchPad.Files
{
    public partial class ZarrFile : FileBase<Dictionary<string, byte[]>, ZarrFileSchema>, IZarrFile
    {
        public virtual JsonDocument Metadata { get; private set; } // Represents the metadata of the Zarr file

        public override string Extension => "." + FileExtensions.zarr;


        /// <summary>
        /// Constructor
        /// </summary>
        public ZarrFile(string fileName) : base(fileName)
        {
        }

        //public void LoadMetadata()
        //{
        //    string metadataPath = Path.Combine(Locations[0].RootUri.LocalPath, ".zarray");
        //    if (File.Exists(metadataPath))
        //    {
        //        string metadataContent = File.ReadAllText(metadataPath);
        //        Metadata = JsonDocument.Parse(metadataContent);
        //    }

        //    FileInfo fileInfo = new FileInfo(Locations[0].RootUri.LocalPath);
        //    Size = Directory.GetFiles(Locations[0].RootUri.LocalPath, "*", SearchOption.AllDirectories)
        //                        .Sum(f => new FileInfo(f).Length);
        //    CreationTime = fileInfo.CreationTime;
        //    LastModificationTime = fileInfo.LastWriteTime;
        //}
    }
}
