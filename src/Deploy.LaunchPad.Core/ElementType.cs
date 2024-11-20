﻿using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core
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
        [MaxLength(511, ErrorMessageResourceName = "Validation_ElementType_FullyQualifiedType_511CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [RegularExpression(@"^([a-zA-Z_][a-zA-Z0-9_]*\.)*[a-zA-Z_][a-zA-Z0-9_]*$", ErrorMessageResourceName = "Validation_ElementType_InvalidType", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(true)]
        [XmlAttribute]
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

        /// <summary>
        /// The type of this object (it should of course be identical to calling GetType() but is intended for storage for documentation purposes or sharing externally).
        /// </summary>
        /// <value>The type of this object.</value>
        [MaxLength(20, ErrorMessageResourceName = "Validation_ElementType_FullyQualifiedType_511CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string TypeName
        {
            get
            {
                int lastDotIndex = FullyQualifiedType.LastIndexOf('.');
                if (lastDotIndex == -1)
                {
                    return FullyQualifiedType; // No namespace, just return the name
                }
                return FullyQualifiedType.Substring(lastDotIndex + 1);

            }
        }


        /// <summary>
        /// The namespace of this object
        /// </summary>
        /// <value>The namespace of this object.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Namespace { get; set; }

        protected string _assemblyFullyQualifiedName = string.Empty;
        /// <summary>
        /// The fully qualified namename of the assembly.
        /// </summary>
        /// <value>The fully qualified name of the assembly.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [RegularExpression(@"^[a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)*(, Version=\d+\.\d+\.\d+\.\d+)?(, Culture=[a-zA-Z0-9\-]+)?(, PublicKeyToken=[a-fA-F0-9]{16}|, PublicKeyToken=null)?$", ErrorMessageResourceName = "Validation_ElementType_InvalidAssembly", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
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
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string AssemblyName
        {
            get
            {
                if(string.IsNullOrEmpty(_assemblyName))
                {
                    string pattern = @"(?<=, )[^,]+";

                    // Use regex to match the short assembly name
                    Match match = Regex.Match(AssemblyFullyQualifiedName, pattern);

                    // Return the matched value or the AssemblyFullyQualifiedName string if no match is found
                    if (match.Success)
                    {
                        _assemblyName = match.Value;
                    }
                    else
                    {
                        _assemblyName = _assemblyFullyQualifiedName;
                    }
                }
                return _assemblyName;
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
        public virtual IDictionary<string, ElementType> ChildrenElementTypes { get; set; }


        /// <summary>
        /// Gets or sets the interface(s) this object inherits from (apart from the ParentFullyQualifiedType which could be a class or interface).
        /// </summary>
        /// <value>The additional fully qualified interfaces this entity inherits from, if any (and apart from its Parent). The key is the fullyQualifiedType and the value is the Type.</value>
        public virtual IDictionary<string, string> InheritsFrom { get; set; }

        protected ElementType()
        {
            ChildrenElementTypes = new Dictionary<string, ElementType>();
            InheritsFrom = new Dictionary<string, string>();
        }

        public ElementType(string fullyQualifiedTypeName)
        {
            FullyQualifiedType = fullyQualifiedTypeName;
            ChildrenElementTypes = new Dictionary<string, ElementType>();
            InheritsFrom = new Dictionary<string, string>();
        }


        public ElementType(string fullyQualifiedTypeName, string assemblyFullyQualifiedName)
        {
            FullyQualifiedType = fullyQualifiedTypeName;
            AssemblyFullyQualifiedName = assemblyFullyQualifiedName;
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
            ElementType element = new ElementType(type.FullName);
            element.AssemblyFullyQualifiedName = type.AssemblyQualifiedName;

            // Get the Parent type
            Type baseType = type.BaseType;
            if (baseType != null)
            {
                ElementType parentElementType = new ElementType(baseType.FullName, baseType.AssemblyQualifiedName);
                parentElementType.Namespace = baseType.Namespace;
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
                                ElementType childElementType = new ElementType(childType.FullName, childType.AssemblyQualifiedName);
                                childElementType.Namespace = childType.Namespace;
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
