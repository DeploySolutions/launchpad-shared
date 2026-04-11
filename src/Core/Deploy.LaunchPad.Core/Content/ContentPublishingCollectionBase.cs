using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Util.Metadata;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Domain.Content
{

    public abstract partial class ContentPublishingCollectionBase : DomainEntityBase<Guid>, 
        IContentPublishingCollection
    {

        public virtual IList<IContentPublishingCollectionItem> Items { get; }
        public virtual bool IsPublished { get; set; }
        public virtual long PublisherUserId { get; set; }
        public virtual DateTimeOffset PublishedTimeInUtc { get; set; }

        public abstract void AddItem(Guid id, IContentPublishingCollectionItem item, bool shouldPreventDuplicates = true);        

        protected ContentPublishingCollectionBase()
        {
            string name = "New Bundle " + DateTime.UtcNow.ToString();
            Name = name;
           // Description = new ElementDescription(name);
            CreationTime = DateTime.Now.ToUniversalTime();
            Tags = "{}";
            Items = new List<IContentPublishingCollectionItem>();
        }

        protected ContentPublishingCollectionBase(string name)
        {            
            Name = name;
            //Description = new ElementDescription(name);
            CreationTime = DateTime.Now.ToUniversalTime();
            Tags = "{}";
            Items = new List<IContentPublishingCollectionItem>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ContentPublishingCollectionBase(SerializationInfo info, StreamingContext context): base(info, context)
        {
            Items = (IList<IContentPublishingCollectionItem>)info.GetValue("Items", typeof(IList<IContentPublishingCollectionItem>));            
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Items", Items);
        }

    }
}
