using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial interface IReadOnlyS3FileCollection<out TFile>
        where TFile : IS3FileInfo
    {
        IEnumerable<TFile> Files { get; }
    }
}
