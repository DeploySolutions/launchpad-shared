using Deploy.LaunchPad.AWS.S3;
using Deploy.LaunchPad.Core.Abp.Domain;

namespace Deploy.LaunchPad.AWS.Abp.S3
{
    public class S3FileSet<TIdType, TFileContentType> : FileSetBase<TIdType, TFileContentType, S3BucketStorageLocation>
    {


        public S3FileSet() : base()
        {
        }
    }
}
