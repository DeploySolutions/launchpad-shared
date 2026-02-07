using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public class S3FolderInfo : LaunchPadMinimalProperties, ILaunchPadObject
    {
        public long FileCount { get; set; }

        public IList<S3FileInfo> Files { get; set; }

        public S3FolderInfo()
        {
            Files = new List<S3FileInfo>();
        }
    }
}
