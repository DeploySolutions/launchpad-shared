using Deploy.LaunchPad.Util.Elements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial class LaunchPadMinimalProperties : ILaunchPadMinimalProperties
    {
        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonPropertyName("name")]
        public virtual ElementName Name { get; set;  }

        /// <summary>
        /// A  description for this entity
        /// </summary>
        /// <value>The description.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonPropertyName("description")]
        public virtual ElementDescription Description{ get; set; }

        protected LaunchPadMinimalProperties()
        {
        }

        public LaunchPadMinimalProperties(string name, string description = null)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(description);
        }


        protected LaunchPadMinimalProperties(ElementName name, ElementDescription description = null)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadMinimalProperties(SerializationInfo info, StreamingContext context)
        {
            Name = (ElementName)info.GetValue("Name", typeof(ElementName));
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
        }

    }
}
