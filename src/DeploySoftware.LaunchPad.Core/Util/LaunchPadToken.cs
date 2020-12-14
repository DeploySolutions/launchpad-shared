using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public class LaunchPadToken
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
        public LaunchPadToken()
        {
            Prefix = string.Empty;
            Name = string.Empty;
            Value = string.Empty;
            DefaultValue = string.Empty;
        }
        public LaunchPadToken(string tokenString)
        {
            Validate(tokenString);
            Parse(tokenString);
        }

        public LaunchPadToken(string tokenString, string value)
        {
            Validate(tokenString);
            Parse(tokenString);
            Value = value;
        }

        protected void Parse(string tokenString)
        {
            string tokenInnerSections = tokenString.Substring(2, tokenString.Length - 4);
            string[] tokens = tokenInnerSections.Split("|");
            foreach (string token in tokens)
            {
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
                    DefaultValue = token.Substring(3);
                }
            }
        }

        protected void Validate(string tokenString)
        {
            Guard.Against<ArgumentException>(string.IsNullOrEmpty(tokenString), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_Empty);
            Guard.Against<ArgumentException>(!tokenString.StartsWith("{{"),DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_WrongStartsWith);
            Guard.Against<ArgumentException>(!tokenString.EndsWith("}}"),DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_WrongEndWith );
            Guard.Against<ArgumentException>(!tokenString.Contains("|"), DeploySoftware_LaunchPad_Core_Resources.Guard_LaunchPadToken_ArgumentException_NoPipes);
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
            if(!String.IsNullOrEmpty(DefaultValue))
            {
                sb.Append("|dv:");
                sb.Append(DefaultValue);
            }
            sb.Append("}}");
            return sb.ToString();
        }

    }
}
