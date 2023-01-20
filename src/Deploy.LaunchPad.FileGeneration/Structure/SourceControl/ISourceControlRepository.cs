using System;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    public partial interface ISourceControlRepository
    {
        string Name { get; set; }
        Uri Uri { get; set; }
    }
}