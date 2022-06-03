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
        [Description("Unknown")]
        Unknown = -1,        
        [Description("Windows.NTFS")]
        Windows_NTFS = 0,
        [Description("AWS.S3")]
        Aws_S3 = 1,
        [Description("AWS.EFS")]
        Aws_EFS = 2,
        [Description("Azure.BlobStorage")]
        Azure_BlobStorage = 3
    }
}
