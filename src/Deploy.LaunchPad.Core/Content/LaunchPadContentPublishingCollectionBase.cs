using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Util;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Content
{

    public abstract partial class LaunchPadContentPublishingCollectionBase<TSchema> : LaunchPadCommonProperties, ILaunchPadObject, ILaunchPadContentPublishingCollection<TSchema>
        where TSchema: Schema.NET.Thing
    {
        public virtual Guid Id { get; set; }

        public virtual IList<ILaunchPadContentPublishingCollectionItem> Items { get; }


        public virtual void AddItem(Guid id, ILaunchPadContentPublishingCollectionItem item, bool shouldPreventDuplicates = true)
        {

            if (shouldPreventDuplicates && !Items.Any(existingItem => existingItem.Id.Equals(item.Id)))
            {
                Items.Add(item);
            }
        }

        protected LaunchPadContentPublishingCollectionBase()
        {
            Id = Guid.NewGuid();
            string name = "New Bundle " + DateTime.UtcNow.ToString();
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            CreationTime = DateTime.Now;
            IsActive = true;
            Culture = "en";
            Tags = "{}";
            Items = new List<ILaunchPadContentPublishingCollectionItem>();
        }

        protected LaunchPadContentPublishingCollectionBase(string name)
        {            
            Id = Guid.NewGuid();
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            CreationTime = DateTime.Now;
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
            Id = (Guid)info.GetValue("Id", typeof(Guid));
            Name = (ElementName)info.GetValue("Name", typeof(ElementName));
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Culture = info.GetString("Culture");
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Metadata");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = info.GetInt64("DeleterUserId");
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
            info.AddValue("Id", Id);
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
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
