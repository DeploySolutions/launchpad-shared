﻿using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    public partial class LaunchPadCommonProperties : ILaunchPadCommonProperties
    {

        public virtual ElementName Name { get; set; }

        public virtual ElementDescription Description { get; set; }

        public virtual string Culture { get; set; }

        public virtual string Checksum { get; set; }

        public virtual int SeqNum { get; set; }

        public virtual string Tags { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public string DeleterUserName { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual string CreatorUserName { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual string LastModifierUserName { get; set; }

        /// <summary>
        ///  constructor 
        /// </summary>
        public LaunchPadCommonProperties()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadCommonProperties(SerializationInfo info, StreamingContext context)
        {
            SerializeCommonProperties(info, context);
        }

        public virtual void SerializeCommonProperties(SerializationInfo info, StreamingContext context)
        {

            Culture = info.GetString("Culture");
            Name = (ElementName)info.GetValue("Name", typeof(ElementName));
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Tags");
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

        public virtual void DeserializeCommonProperties(SerializationInfo info, StreamingContext context)
        {
            Culture = info.GetString("Culture");
            Name = (ElementName)info.GetValue("Name", typeof(ElementName));
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Tags");
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
    }
}
