using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DeploySoftware.LaunchPad.Core.Domain;
using DeploySoftware.LaunchPad.Core.Abp.Domain;
using DeploySoftware.LaunchPad.AWS.S3;

namespace DeploySoftware.LaunchPad.AWS.Abp.S3
{
    public class S3FileSet<TIdType, TFileContentType> : FileSetBase<TIdType, TFileContentType, S3BucketStorageLocation>
    {


        public S3FileSet() : base()
        {
        }
    }
}
