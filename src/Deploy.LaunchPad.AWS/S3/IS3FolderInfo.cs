using Deploy.LaunchPad.Util.Elements;
using System.Collections.Generic;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial interface IS3FolderInfo<out TFileInfo> : ILaunchPadMinimalProperties, ILaunchPadObject
        where TFileInfo : S3FileInfo
    {
        long FileCount { get; set; }
        public IReadOnlyS3FileCollection<IS3FileInfo> Items { get; set; }

        public void AddItem(IS3FileInfo item);
        
        public void RemoveItem(IS3FileInfo item);
    }
}