using Castle.Core.Logging;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.ValueConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util
{
    [Serializable]
    [ComplexType]
    public partial class ElementType : IElementType
    {

        protected string _fullyQualifiedType = string.Empty;
        /// <summary>
        ///The fully qualified type of the object.
        /// </summary>
        /// <value>The fully qualified type of the object.</value>
        [Required]
        [MaxLength(511, ErrorMessageResourceName = "Validation_ElementType_FullyQualifiedType_511CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [RegularExpression(@"^([a-zA-Z_][a-zA-Z0-9_]*\.)*[a-zA-Z_][a-zA-Z0-9_]*$", ErrorMessageResourceName = "Validation_ElementType_InvalidType", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(true)]
        [XmlAttribute]
        [JsonProperty("fullyQualifiedType", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string FullyQualifiedType
        {
            get
            {
                return _fullyQualifiedType;
            }
            set
            {
                _fullyQualifiedType = value;
            }
        }
        protected string _type = string.Empty;
        /// <summary>
        /// The type of this object (it should of course be identical to calling GetType() but is intended for storage for documentation purposes or sharing externally).
        /// </summary>
        /// <value>The type of this object.</value>
        [MaxLength(20, ErrorMessageResourceName = "Validation_ElementType_FullyQualifiedType_511CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("typeName", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string TypeName
        {
            get
            {
                return _type;

            }
            set
            {                 
                _type = value;
            }
        }


        protected string _namespace = string.Empty;
        /// <summary>
        /// The namespace of this object
        /// </summary>
        /// <value>The namespace of this object.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string Namespace { get { return _namespace; } set { _namespace = value; } }

        protected string _assemblyFullyQualifiedName = string.Empty;
        /// <summary>
        /// The fully qualified namename of the assembly.
        /// </summary>
        /// <value>The fully qualified name of the assembly.</value>
        [RegularExpression(@"^[a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)*(, Version=\d+\.\d+\.\d+\.\d+)?(, Culture=[a-zA-Z0-9\-]+)?(, PublicKeyToken=[a-fA-F0-9]{16}|, PublicKeyToken=null)?$", ErrorMessageResourceName = "Validation_ElementType_InvalidAssembly", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("assemblyFullyQualifiedName", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string AssemblyFullyQualifiedName
        {
            get
            {
                return _assemblyFullyQualifiedName;
            }
            set
            {
                _assemblyFullyQualifiedName = value;
            }
        }

        protected string _assemblyName = string.Empty;
        /// <summary>
        /// The name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Util_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("assemblyName", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
            set
            {
                _assemblyName = value;
            }
        }

        protected ElementType _parentElementType;
        /// <summary>
        ///The ElementType of the object's parent.
        /// </summary>
        /// <value>The ElementType of the object's parent.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("parentElementType", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public virtual ElementType ParentElementType
        {
            get
            {
                return _parentElementType;
            }
            set
            {
                _parentElementType = value;
            }
        }

        /// <summary>
        /// Gets or sets the ElementTypes of any children.
        /// </summary>
        /// <value>The ElementTypes of children, if any. The key is the fullyQualifiedType of the parent, and the value is the Type instance. </value>
        [JsonProperty("childrenElementTypes", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public virtual IDictionary<string, ElementType> ChildrenElementTypes { get; set; }


        /// <summary>
        /// Gets or sets the interface(s) this object inherits from (apart from the ParentFullyQualifiedType which could be a class or interface).
        /// </summary>
        /// <value>The additional fully qualified interfaces this entity inherits from, if any (and apart from its Parent). The key is the fullyQualifiedType and the value is the Type.</value>

        [JsonProperty("inheritsFrom", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public virtual IDictionary<string, string> InheritsFrom { get; set; }

        protected ElementType()
        {
            ChildrenElementTypes = new Dictionary<string, ElementType>();
            InheritsFrom = new Dictionary<string, string>();
        }

        public ElementType(string assemblyFullyQualifiedName)
        {
            // Split at the first comma
            int commaIndex = assemblyFullyQualifiedName.IndexOf(',');
            string typeFullName = commaIndex >= 0
                ? assemblyFullyQualifiedName.Substring(0, commaIndex).Trim()
                : assemblyFullyQualifiedName.Trim();

            string assemblyDisplayName = commaIndex >= 0
                ? assemblyFullyQualifiedName.Substring(commaIndex + 1).Trim()
                : string.Empty;

            // TypeName is after the last dot
            int lastDotIndex = typeFullName.LastIndexOf('.');
            string typeName = lastDotIndex >= 0
                ? typeFullName.Substring(lastDotIndex + 1)
                : typeFullName;

            // Namespace is everything before the last dot
            string @namespace = lastDotIndex > 0
                ? typeFullName.Substring(0, lastDotIndex)
                : string.Empty;

            _assemblyFullyQualifiedName = assemblyFullyQualifiedName;
            _assemblyName = assemblyDisplayName.Split(',')[0].Trim();
            _fullyQualifiedType = typeFullName;
            _namespace = @namespace;
            ChildrenElementTypes = new Dictionary<string, ElementType>();
            InheritsFrom = new Dictionary<string, string>();
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(ElementType other)
        {
            // since the FullyQualified name cannot be different without being a completely different type, we only have to compare against it.
            return FullyQualifiedType.CompareTo(other.FullyQualifiedType)
            ;
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return FullyQualifiedType;
        }


        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ElementType)
            {
                return Equals(obj as ElementType);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Equals(ElementType obj)
        {
            if (obj != null)
            {
                return FullyQualifiedType.Equals(obj.FullyQualifiedType)
                    && TypeName.Equals(obj.TypeName)
                    && AssemblyName.Equals(obj.AssemblyName)
                    && AssemblyFullyQualifiedName.Equals(obj.AssemblyFullyQualifiedName)
                    && ParentElementType.Equals(obj.ParentElementType)
                    && InheritsFrom.Equals(obj.InheritsFrom)
                    && ChildrenElementTypes.Equals(obj.ChildrenElementTypes)
                ;
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(ElementType x, ElementType y)
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
        public static bool operator !=(ElementType x, ElementType y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return FullyQualifiedType.GetHashCode()
                + TypeName.GetHashCode()
                + ParentElementType.GetHashCode()
                + AssemblyFullyQualifiedName.GetHashCode()
                + InheritsFrom.GetHashCode()
                + ChildrenElementTypes.GetHashCode()
            ;
        }


        public static ElementType GetTypeInformationForElement(ILogger logger, Type type, bool shouldThrowOnError = true, bool shouldIgnoreCase = true, IList<string> assembliesContainingChildren = null)
        {
            Guard.Against<ArgumentNullException>(type == null, "Type cannot be null.");
            ElementType element = new ElementType(type.AssemblyQualifiedName);

            // Get the Parent type
            Type baseType = type.BaseType;
            if (baseType != null)
            {
                ElementType parentElementType = new ElementType(baseType.AssemblyQualifiedName);
                element.ParentElementType = parentElementType;
            }
            Type[] interfaces = type.GetInterfaces();
            foreach (Type interfaceType in interfaces)
            {
                element.InheritsFrom.TryAdd(interfaceType.FullName, interfaceType.Name);
            }
            // check children from assemblies
            if (assembliesContainingChildren == null)
            {
                assembliesContainingChildren = new List<string>();
            }
            if (!assembliesContainingChildren.Contains(element.AssemblyName))
            {
                assembliesContainingChildren.Add(element.AssemblyName);
            }
            foreach (string assemblyName in assembliesContainingChildren)
            {
                try
                {
                    Assembly assemblyWithPossibleChildren = Assembly.Load(assemblyName);
                    if (assemblyWithPossibleChildren != null)
                    {
                        Type[] types = assemblyWithPossibleChildren.GetTypes();
                        foreach (Type childType in types)
                        {
                            if (childType.BaseType == type)
                            {
                                ElementType childElementType = new ElementType(childType.AssemblyQualifiedName);
                                string message = string.Format("Found child type '{0}' for type '{1}', in assembly {2}.",
                                    childType.FullName, element.FullyQualifiedType, assemblyWithPossibleChildren.FullName
                                );
                                logger.Debug(message);
                                element.ChildrenElementTypes.TryAdd(childType.FullName, childElementType);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (shouldThrowOnError)
                    {
                        throw;
                    }
                    else
                    {
                        string message = string.Format("Error loading assembly '{0}' for type '{1}' but shouldThrowOnError was set to false, so logging only. Exception was: {2}.",
                            assemblyName, type.FullName, ex.Message
                        );
                        logger.Error(message);
                    }
                }
            }
            return element;
        }

        public static ElementType GetTypeInformationForElement(ILogger logger, string fullyQualifiedTypeName, bool shouldThrowOnError = true, bool shouldIgnoreCase = true, IList<string> assembliesContainingChildren = null)
        {
            ElementType element = new ElementType(fullyQualifiedTypeName);
            Type type = Type.GetType(fullyQualifiedTypeName, shouldThrowOnError, shouldIgnoreCase);
            if (type != null)
            {
                GetTypeInformationForElement(logger, type, shouldThrowOnError, shouldIgnoreCase, assembliesContainingChildren);
            }
            else
            {
                logger.Debug(string.Format("Type '{0}' not found.", fullyQualifiedTypeName));
            }
            return element;
        }
    }

}
