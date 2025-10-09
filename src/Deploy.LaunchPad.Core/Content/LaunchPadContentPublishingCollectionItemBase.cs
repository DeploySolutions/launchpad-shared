using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Util;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Content
{

    public abstract partial class LaunchPadContentPublishingCollectionItemBase<TSchema> : LaunchPadCommonProperties, ILaunchPadObject,        
        ILaunchPadContentPublishingItem<TSchema>
        where TSchema: Schema.NET.Thing
    {
        public virtual Guid Id { get; set; }

        public virtual LaunchPadContentItemType ContentType { get; set; }

        protected virtual TSchema? _schemaDotOrg { get; set; }

        public virtual string SchemaDotOrgJson { get; set; }

        ///<summary>
        /// Location of the resource (relative and lower than application root or a selected safe path, to avoid security holes
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri? ResourceRelativeUri { get; set; }

        protected LaunchPadContentPublishingCollectionItemBase(string name, LaunchPadContentItemType type)
        {
            Id = Guid.NewGuid();
            ContentType = type;
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
        protected LaunchPadContentPublishingCollectionItemBase(SerializationInfo info, StreamingContext context)
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
            ContentType = (LaunchPadContentItemType)info.GetValue("ContentType", typeof(LaunchPadContentItemType));
            _schemaDotOrg = (TSchema)info.GetValue("_schemaDotOrg", typeof(TSchema));
            SchemaDotOrgJson = info.GetString("SchemaDotOrgJson");
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
            info.AddValue("ContentType", ContentType);
            info.AddValue("_schemaDotOrg", _schemaDotOrg);
            info.AddValue("SchemaDotOrgJson", SchemaDotOrgJson);
        }

    }
}
