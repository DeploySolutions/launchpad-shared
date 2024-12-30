using Deploy.LaunchPad.Core.Domain.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Content
{

    public abstract partial class LaunchPadContentPublishingBundleBase<TContentItemId, TSchema> : LaunchPadCommonProperties, ILaunchPadObject, ILaunchPadContentPublishingBundle<TContentItemId, TSchema>
        where TSchema: Schema.NET.Thing
    {
        public virtual TContentItemId Id { get; set; }

        public virtual IList<ILaunchPadContentPublishingItem<TContentItemId, TSchema>> Items { get; }


        public virtual void AddItem(TContentItemId id, ILaunchPadContentPublishingItem<TContentItemId, TSchema> item, bool shouldPreventDuplicates = true)
        {

            if (shouldPreventDuplicates && !Items.Any(existingItem => existingItem.Id.Equals(item.Id)))
            {
                Items.Add(item);
            }
        }

        protected LaunchPadContentPublishingBundleBase(string name, LaunchPadContentItemType type)
        {            
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            CreationTime = DateTime.Now;
            IsActive = true;
            Culture = "en";
            Tags = "{}";
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadContentPublishingBundleBase(SerializationInfo info, StreamingContext context)
        {
            Id = (TContentItemId)info.GetValue("Id", typeof(TContentItemId));
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
            
        }

    }
}
