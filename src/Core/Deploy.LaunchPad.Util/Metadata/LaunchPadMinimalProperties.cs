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

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial class LaunchPadMinimalProperties : ILaunchPadMinimalProperties
    {
        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        //[JsonPropertyName("name")]
        [Column("core_name_full")]
        public virtual string Name { get; set;  }

        
        protected LaunchPadMinimalProperties()
        {
        }

        public LaunchPadMinimalProperties(string name)
        {
            Name = name;
        }


        protected LaunchPadMinimalProperties(ElementName name)
        {
            Name = name.Name;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadMinimalProperties(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            //Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
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
        }

    }
}
