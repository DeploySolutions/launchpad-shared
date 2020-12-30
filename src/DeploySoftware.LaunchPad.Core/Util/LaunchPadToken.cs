using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Util
{
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
            StringBuilder sb = new StringBuilder("Token: {{");
            sb.Append("p:");
            sb.Append(Prefix);
            sb.Append("|n:");
            sb.Append(Name);
            if(!String.IsNullOrEmpty(DefaultValue))
            {
                sb.Append("|dv:");
                sb.Append(DefaultValue);
            }
            sb.Append("}} ");
            if (!String.IsNullOrEmpty(DefaultValue))
            {
                sb.Append(" Value:");
                sb.Append(Value);
            }
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
                return Name.Equals(obj.Name) && Prefix.Equals(obj.Prefix)
                    && Value.Equals(obj.Value) && DefaultValue.Equals(obj.DefaultValue);

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
                + Value.GetHashCode()
                + DefaultValue.GetHashCode()
                ;
        }

    }
}
