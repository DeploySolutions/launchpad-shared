using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Core.Elements;
using System;
using System.Collections.Generic;
using System.Text;
using Deploy.LaunchPad.Files;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial class S3FileInfo : LaunchPadMinimalProperties, IS3FileInfo
    {

        public IFileContent<object> Content { get; set; }
        public long? FileSize { get; set; }
        public S3FileInfo()
        {
        }
    }
}
