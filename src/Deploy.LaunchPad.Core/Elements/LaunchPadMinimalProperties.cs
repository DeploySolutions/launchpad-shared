using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Elements
{
    public partial class LaunchPadMinimalProperties : ILaunchPadMinimalProperties
    {

        protected ElementName _name;
        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonPropertyName("name")]
        public virtual ElementName Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected ElementDescription _description;
        /// <summary>
        /// A  description for this entity
        /// </summary>
        /// <value>The description.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonPropertyName("description")]
        public virtual ElementDescription Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public LaunchPadMinimalProperties()
        {
            _name = new ElementName();
            _description = new ElementDescription();
        }

        public LaunchPadMinimalProperties(string name, string description = null)
        {
            _name = new ElementName(name);
            _description = description != null ? new ElementDescription(description) : new ElementDescription();
        }


        public LaunchPadMinimalProperties(ElementName name, ElementDescription description = null)
        {
            _name = name;
            _description = description ?? new ElementDescription();
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
