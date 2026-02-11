using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial class S3FolderInfo : LaunchPadMinimalProperties, IS3FolderInfo<S3FileInfo>
    {
        public virtual long FileCount { get; set; }

        protected readonly List<IS3FileInfo> _items = new();

        public virtual IReadOnlyS3FileCollection<IS3FileInfo> Items { get; set; }

        public S3FolderInfo(string name)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            Items = new ReadOnlyS3FileCollection<IS3FileInfo>(_items);
        }
        public virtual void AddItem(IS3FileInfo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }

            _items.Add(item);
            FileCount = _items.Count;
        }

        public virtual void RemoveItem(IS3FileInfo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }

            _items.Remove(item);
            FileCount = _items.Count;
        }
    }
}
