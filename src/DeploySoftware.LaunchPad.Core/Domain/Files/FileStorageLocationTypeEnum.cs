using System;
using System.ComponentModel;

namespace DeploySoftware.LaunchPad.Core.Domain
{

    [Serializable]
    public enum FileStorageLocationTypeEnum
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
