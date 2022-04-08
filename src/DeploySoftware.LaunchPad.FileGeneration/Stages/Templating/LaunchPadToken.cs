﻿using DeploySoftware.LaunchPad.Core;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
{
    [Serializable]
    public class LaunchPadToken : IEquatable<LaunchPadToken>
    {
        /// <summary>
        /// The prefix of this token
        /// </summary>
        [MaxLength(9, ErrorMessageResourceName = "Validation_Token_Prefix_9CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public String Prefix { get; set; }

        /// <summary>
        /// The name of this token
        /// </summary>
        /// 
        [MaxLength(20, ErrorMessageResourceName = "Validation_Token_Name_20CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// The value of this token. If null or empty, and a default is set, implementers should return that.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Value { get; set; }

        /// <summary>
        /// The default value of this token, if no actual value is provided.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Optional key-value pair metadata for this token. 
        /// Key and Value within a text token are prefixed by 'tags:' and separated by '='. Tags are separated by ';'
        /// Example:  tags:Key1=Value1;Key2=Value2;
        /// Dictionary is used to ensure a key only appears once. Case-insensitive comparer. 
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IDictionary<string, string> Tags { get; set; }

        public LaunchPadToken()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tags = new Dictionary<string, string>(comparer);
            Prefix = string.Empty;
            Name = string.Empty;
            Value = string.Empty;
            DefaultValue = string.Empty;

        }
        public LaunchPadToken(string tokenString)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tags = new Dictionary<string, string>(comparer);
            Value = string.Empty;
            DefaultValue = string.Empty;
            Validate(tokenString);
            Parse(tokenString);
        }

        public LaunchPadToken(string tokenString, string value)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tags = new Dictionary<string, string>(comparer);
            DefaultValue = string.Empty;
            Validate(tokenString);
            Parse(tokenString);
            Value = value;
        }

        public LaunchPadToken(string tokenString, string value, IDictionary<string, string> tags)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tags = new Dictionary<string, string>(comparer);
            Validate(tokenString);
            Parse(tokenString);
            Value = value;
            Tags = tags;
        }

        protected void Parse(string tokenString)
        {
            string tokenInnerSections = tokenString.Trim().Substring(2, tokenString.Length - 4);
            string[] tokens = tokenInnerSections.Split("|");
            foreach (string token in tokens)
            {
                DefaultValue = string.Empty;
                if (token.StartsWith("p:"))
                {
                    Prefix = token.Substring(2);
                }
                if (token.StartsWith("n:"))
                {
                    Name = token.Substring(2);
                }
                if (token.StartsWith("dv:"))
                {
                    DefaultValue = token.Substring(3); // overwrite the empty default value
                }
                if (token.StartsWith("v:"))
                {
                    Value = token.Substring(2);
                }
                if (token.StartsWith("tags:"))
                {
                    Tags = ParseTags(token.Substring(5));
                }
            }
        }

        /// <summary>
        /// Parses a given token tags element and add its KVPs to the dictionary
        /// Key and Value within a text token are prefixed by 'tags:' and separated by '='. Tags are separated by ';'
        /// Example:  tags:Key1=Value1;Key2=Value2;
        /// Therefore, we first split the tags up by ';', then the first element of each tag is the key and the second is the value.
        /// </summary>
        /// <param name="tokenTagsString">The token element</param>
        /// <returns>A dictionary containing a set of tags for a particular token</returns>
        protected IDictionary<string,string> ParseTags(string tokenTagsString)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            IDictionary<string, string> tokenTags = new Dictionary<string, string>(comparer);
            if (!string.IsNullOrEmpty(tokenTagsString))
            {
                string[] tags = tokenTagsString.Split(";");
                foreach (string tag in tags)
                {
                    if (!string.IsNullOrEmpty(tag))
                    {
                        string[] kvp = tag.Split("=");
                        if (kvp.Length > 0)
                        {
                            tokenTags.Add(kvp[0], kvp[1]);
                        }
                    }
                }
            }
            return tokenTags;
        }


        protected void Validate(string tokenString)
        {
            Guard.Against<ArgumentException>(string.IsNullOrEmpty(tokenString), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_Empty);
            Guard.Against<ArgumentException>(!tokenString.StartsWith("{{"),DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_WrongStartsWith);
            Guard.Against<ArgumentException>(!tokenString.EndsWith("}}"),DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_WrongEndWith );
            Guard.Against<ArgumentException>(!tokenString.Contains("|"), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_NoDelimiter); 
            Guard.Against<ArgumentException>(!tokenString.Contains("p:"), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_MissingPrefix);
            Guard.Against<ArgumentException>(!tokenString.Contains("n:"), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_MissingName);
            int length = tokenString.Split("|").Length;
            Guard.Against<ArgumentException>(length > 3 | length < 2, DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_WrongNumberSections);            
        }

        public LaunchPadToken(string prefix, string name, string defaultValue)
        {
            Prefix = prefix;
            Name = name;
            DefaultValue = defaultValue;
            Value = string.Empty;
        }

        public LaunchPadToken(string prefix, string name, string defaultValue, string value)
        {
            Prefix = prefix;
            Name = name;
            DefaultValue = defaultValue;
            Value = value;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("{{");
            sb.Append("p:");
            sb.Append(Prefix);
            sb.Append("|n:");
            sb.Append(Name);
            sb.Append("}}");
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the token, specifically including its default value
        /// </summary>
        /// <returns></returns>
        public virtual string ToStringWithDefaultValue()
        {
            StringBuilder sb = new StringBuilder("{{");
            sb.Append("p:");
            sb.Append(Prefix);
            sb.Append("|n:");
            sb.Append(Name);
            if (Tags != null && Tags.Count > 0)
            {
                sb.Append("|tags:");
                sb.Append(Tags.ToString());
            }
            if (!String.IsNullOrEmpty(Value))
            {
                sb.Append("|v:");
                sb.Append(Value);
            }
            if (!String.IsNullOrEmpty(DefaultValue))
            {
                sb.Append("|dv:");
                sb.Append(DefaultValue);
            }
            sb.Append("}}");
            return sb.ToString();
        }


        public override bool Equals(object obj)
        {
            if (obj != null && obj is LaunchPadToken)
            {
                return Equals(obj as LaunchPadToken);
            }
            return false;
            
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.      
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns></returns>
        public virtual bool Equals(LaunchPadToken obj)
        {
            if (obj != null)
            {
                return Name.Equals(obj.Name) && Prefix.Equals(obj.Prefix) && DefaultValue.Equals(obj.DefaultValue);

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(LaunchPadToken x, LaunchPadToken y)
        {
            if (x is null)
            {
                if (y is null)
                {
                    return true;
                }
                return false;
            }
            return x.Equals(y);
        }

        /// <summary>
        /// Override the != operator to test for inequality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are not equal based on the Equals logic</returns>
        public static bool operator !=(LaunchPadToken x, LaunchPadToken y)
        {
            return !(x == y);
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode()
                + Prefix.GetHashCode()
                + DefaultValue.GetHashCode()
                ;
        }

    }
}
