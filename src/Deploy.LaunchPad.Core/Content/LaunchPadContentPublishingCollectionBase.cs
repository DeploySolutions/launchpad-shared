using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Domain.Content
{

    public abstract partial class LaunchPadContentPublishingCollectionBase : LaunchPadCoreProperties, 
        ILaunchPadContentPublishingCollection
    {

        public virtual IList<ILaunchPadContentPublishingCollectionItem> Items { get; }
        public virtual bool IsPublished { get; set; }
        public virtual long PublisherUserId { get; set; }
        public virtual DateTimeOffset PublishedTimeInUtc { get; set; }

        public abstract void AddItem(Guid id, ILaunchPadContentPublishingCollectionItem item, bool shouldPreventDuplicates = true);        

        protected LaunchPadContentPublishingCollectionBase()
        {
            string name = "New Bundle " + DateTime.UtcNow.ToString();
            Name = name;
           // Description = new ElementDescription(name);
            CreationTime = DateTime.Now.ToUniversalTime();
            IsActive = true;
            Culture = "en";
            Tags = "{}";
            Items = new List<ILaunchPadContentPublishingCollectionItem>();
        }

        protected LaunchPadContentPublishingCollectionBase(string name)
        {            
            Name = name;
            //Description = new ElementDescription(name);
            CreationTime = DateTime.Now.ToUniversalTime();
            IsActive = true;
            Culture = "en";
            Tags = "{}";
            Items = new List<ILaunchPadContentPublishingCollectionItem>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadContentPublishingCollectionBase(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
           // Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Culture = info.GetString("Culture");
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Metadata");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = (Guid?)info.GetValue("CreatorUserId", typeof(Guid?));
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = (Guid?)info.GetValue("LastModifierUserId", typeof(Guid?));
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = (Guid?)info.GetValue("DeleterUserId", typeof(Guid?));
            DeletionTime = info.GetDateTime("DeletionTime");
            IsActive = info.GetBoolean("IsActive");
            SeqNum = info.GetInt32("SeqNum");
            Items = (IList<ILaunchPadContentPublishingCollectionItem>)info.GetValue("Items", typeof(IList<ILaunchPadContentPublishingCollectionItem>));            

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
           // info.AddValue("Description", Description);
            info.AddValue("Culture", Culture);
            info.AddValue("Checksum", Checksum);
            info.AddValue("Tags", Tags);
            info.AddValue("SeqNum", SeqNum);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("IsActive", IsActive);
            info.AddValue("Items", Items);

        }

    }
}
