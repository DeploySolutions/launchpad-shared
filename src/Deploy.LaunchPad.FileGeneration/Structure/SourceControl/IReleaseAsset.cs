using System;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    public partial interface IReleaseAsset
    {
        public string Name { get; set; }
        public Uri Uri { get; set; }
    }
}