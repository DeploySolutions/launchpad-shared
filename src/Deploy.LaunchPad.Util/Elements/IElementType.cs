
using Castle.Core.Logging;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Util
{
    public partial interface IElementType : IComparable<ElementType>, IEquatable<ElementType>
    {
        /// <summary>
        ///The fully qualified type of the object.
        /// </summary>
        /// <value>The fully qualified type of the object.</value>
        public string FullyQualifiedType { get; init; }

        /// <summary>
        /// The type of this object (it should of course be identical to calling GetType() but is intended for storage for documentation purposes or sharing externally).
        /// </summary>
        /// <value>The type of this object.</value>
        public string TypeName { get; init;  }


        /// <summary>
        /// The namespace of this object
        /// </summary>
        /// <value>The namespace of this object.</value>
        public string Namespace { get; init; }

        /// <summary>
        /// The name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>
        public string AssemblyName { get; init; }

        /// <summary>
        /// The fully qualified name of the assembly.
        /// </summary>
        /// <value>The fully qualified name of the assembly.</value>
        public string AssemblyFullyQualifiedName { get; init; }

        /// <summary>
        /// The ElementType of the parent.
        /// </summary>
        /// <value>The ElementType of the parent.</value>
        public ElementType ParentElementType { get; }

        /// <summary>
        /// Gets or sets the ElementTypes of any children.
        /// </summary>
        /// <value>The ElementTypes of children, if any. The key is the fullyQualifiedType of the parent, and the value is the Type instance. </value>
        public IDictionary<string, ElementType> ChildrenElementTypes { get; set; }

        /// <summary>
        /// Gets or sets the interface(s) this object inherits from (apart from the ParentFullyQualifiedType which could be a class or interface).
        /// </summary>
        /// <value>Gets or sets the interface(s) this object inherits from (apart from the ParentFullyQualifiedType which could be a class or interface).</value>
        public IDictionary<string, string> InheritsFrom { get; set; }

        public abstract static ElementType GetTypeInformationForElement(ILogger logger, string fullyQualifiedTypeName, bool shouldThrowOnError = true, bool shouldIgnoreCase = true, IList<string> assembliesContainingChildren = null);
    }
}