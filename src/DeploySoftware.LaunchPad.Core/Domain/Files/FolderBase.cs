using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public abstract partial class FolderBase<TIdType, TFileContentType, TFileStorageLocationType>
        where TFileStorageLocationType: IFileStorageLocation, new()
        where TFileContentType: class
    {
        public long Count { get; set; }

        public IList<IFile<TIdType, TFileContentType, TFileStorageLocationType>> Files { get; set; }

        public FolderBase()
        {
            Files = new List<IFile<TIdType, TFileContentType, TFileStorageLocationType>>();
        }
    }
}
