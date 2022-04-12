using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DeploySoftware.LaunchPad.Core.Domain;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public class S3FileSet<TIdType, TFileContentType> : FileSetBase<TIdType, TFileContentType, S3BucketStorageLocation>
    {


        public S3FileSet() : base()
        {
        }
    }
}
