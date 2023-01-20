using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    public partial interface IRelease
    {
        public IDictionary<string, IReleaseAsset> Assets { get; set; }
        public string Checksum { get; set; }
        public ReleaseSourceCode SourceCodeZip { get; set; }
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}