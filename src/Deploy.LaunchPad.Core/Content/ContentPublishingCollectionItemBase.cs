using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.Content
{

    public abstract partial class ContentPublishingCollectionItemBase : DomainEntityBase<Guid>,        
        ILaunchPadContentPublishingItem
    {
        public virtual ContentItemType ContentType { get; set; }

        public virtual bool IsPublished { get; set; }
        public virtual long PublisherUserId { get; set; }
        public virtual DateTimeOffset PublishedTimeInUtc { get; set; }

        ///<summary>
        /// Location of the resource (relative and lower than application root or a selected safe path, to avoid security holes
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri? ResourceRelativeUri { get; set; }

        protected ContentPublishingCollectionItemBase(string name, ContentItemType type)
        {
            ContentType = type;
            Name = name;
            //Description = new ElementDescription(name);
            CreationTime = DateTime.Now.ToUniversalTime();
            Tags = "{}";
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ContentPublishingCollectionItemBase(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            //Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Culture = (CultureInfo)info.GetValue("Culture", typeof(CultureInfo));
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Metadata");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = (Guid?)info.GetValue("CreatorUserId", typeof(Guid?));
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = (Guid?)info.GetValue("LastModifierUserId", typeof(Guid?));
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = (Guid?)info.GetValue("DeleterUserId", typeof(Guid?));
            DeletionTime = info.GetDateTime("DeletionTime");
            ContentType = (ContentItemType)info.GetValue("ContentType", typeof(ContentItemType));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            //info.AddValue("Description", Description);
            info.AddValue("Culture", Culture);
            info.AddValue("Checksum", Checksum);
            info.AddValue("Tags", Tags);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("ContentType", ContentType);
        }

    }
}
