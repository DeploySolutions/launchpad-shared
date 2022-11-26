using DeploySoftware.LaunchPad.AWS.S3;
using DeploySoftware.LaunchPad.Core.Abp.Domain;
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.AWS.Abp.S3
{
    public partial class S3File<TIdType, TFileContentType> : FileBase<TIdType, TFileContentType>
    {
        

        public S3File() : base()
        {

        }

    }
}
