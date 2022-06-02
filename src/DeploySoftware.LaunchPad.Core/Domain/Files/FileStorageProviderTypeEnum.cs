using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public enum FileStorageProviderTypeEnum
    {
        [Description("AWS.S3")]
        Aws_S3 = 0,
        [Description("AWS.EFS")]
        Aws_EFS = 1,
        [Description("Azure.BlobStorage")]
        Azure_BlobStorage = 2,
        [Description("Windows.NTFS")]
        Windows_NTFS = 3
    }
}
