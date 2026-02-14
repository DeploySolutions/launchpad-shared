using Deploy.LaunchPad.AWS.S3;
using Deploy.LaunchPad.Util.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public class ChildFolderInfo : S3FolderInfo
    {
        public new IReadOnlyS3FileCollection<ChildFileInfo> Items
        {
            get => new ReadOnlyS3FileCollection<ChildFileInfo>(_items.OfType<ChildFileInfo>());
        }

        public long RegularFactCount { get; set; }
        public long OutlierFactCount { get; set; }

        public long ErrorCount { get; set; }

        public ChildFolderInfo(string name) : base(name)
        {
            ErrorCount = 0;
        }

        public IList<ILaunchPadDataFact> GetRegularFacts()
        {
            var f = from fact in Items.Files
                    where fact.IsRegularFact()
                    select fact.RegularFact;
            return f.ToList();
        }
        public IList<ILaunchPadDataFact> GetOutlierFacts()
        {
            var f = from fact in Items.Files
                   where fact.IsOutlierFact()
                   select fact.OutlierFact;
            return f.ToList();
        }


        public override void AddItem(IS3FileInfo item)
        {
            if (item is not ChildFileInfo secondaryItem)
            {
                throw new ArgumentException("Only SecondaryStorageFileInfo items can be added.", nameof(item));
            }

            base.AddItem(secondaryItem);
        }

        public override void RemoveItem(IS3FileInfo item)
        {
            if (item is not ChildFileInfo secondaryItem)
            {
                throw new ArgumentException("Only SecondaryStorageFileInfo items can be removed.", nameof(item));
            }

            base.RemoveItem(secondaryItem);
        }
    }
}
