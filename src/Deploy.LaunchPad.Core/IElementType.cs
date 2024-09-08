using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementType : IComparable<ElementType>, IEquatable<ElementType>
    {
        /// <summary>
        ///The fully qualified type of the object.
        /// </summary>
        /// <value>The fully qualified type of the object.</value>
        public string FullyQualifiedType { get; }

        /// <summary>
        /// The type of this object (it should of course be identical to calling GetType() but is intended for storage for documentation purposes or sharing externally).
        /// </summary>
        /// <value>The type of this object.</value>
        public string TypeName { get; }

        /// <summary>
        /// The name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>
        public string AssemblyName { get; }

        /// <summary>
        /// The fully qualified name of the assembly.
        /// </summary>
        /// <value>The fully qualified name of the assembly.</value>
        public string AssemblyFullyQualifiedName { get; }

        /// <summary>
        /// The type of the parent.
        /// </summary>
        /// <value>The type of the parent.</value>
        public string ParentType { get; }

        /// <summary>
        /// The fully qualified type of the parent.
        /// </summary>
        /// <value>The fully qualified type of the parent.</value>
        public string ParentFullyQualifiedType { get; }

        /// <summary>
        /// Gets or sets the fully qualified types of any children.
        /// </summary>
        /// <value>The fully qualified types of children, if any.</value>
        public IDictionary<string, string> ChildrenFullyQualifiedTypes { get; set; }

        /// <summary>
        /// Gets or sets the interface(s) this object inherits from (apart from the ParentFullyQualifiedType which could be a class or interface).
        /// </summary>
        /// <value>Gets or sets the interface(s) this object inherits from (apart from the ParentFullyQualifiedType which could be a class or interface).</value>
        public IDictionary<string, string> InheritsFrom { get; set; }

        public abstract static ElementType GetTypeInformationForElement(ILogger logger, string fullyQualifiedTypeName, bool shouldThrowOnError = true, bool shouldIgnoreCase = true, IList<string> assembliesContainingChildren = null);
    }
}