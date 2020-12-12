using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public class LaunchPadToken
    {
        /// <summary>
        /// The name of this token
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// The value of this token
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Value { get; set; }

        public LaunchPadToken(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
