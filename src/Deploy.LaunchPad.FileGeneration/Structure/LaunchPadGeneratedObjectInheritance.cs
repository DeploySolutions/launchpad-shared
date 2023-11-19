// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-11-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedObjectInheritance.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class LaunchPadGeneratedObjectInheritance.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObjectInheritance" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObjectInheritance" />
    [Serializable]
    public partial class LaunchPadGeneratedObjectInheritance : ILaunchPadGeneratedObjectInheritance
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the fully qualified.
        /// </summary>
        /// <value>The type of the fully qualified.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string FullyQualifiedType { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly fully qualified.
        /// </summary>
        /// <value>The name of the assembly fully qualified.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string AssemblyFullyQualifiedName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the parent fully qualified.
        /// </summary>
        /// <value>The type of the parent fully qualified.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string ParentFullyQualifiedType { get; set; } = string.Empty;

        /// <summary>
        /// If tracked/known, specify the fully qualified type names of any children entities (ex. MyCorp.MyApp.Orders.LineItems)
        /// </summary>
        /// <value>The children fully qualified types.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public IDictionary<string, string> ChildrenFullyQualifiedTypes { get; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        /// <value>The inherits from.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<string, string> InheritsFrom { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedObjectInheritance"/> class.
        /// </summary>
        public LaunchPadGeneratedObjectInheritance()
        {           
            var comparer = StringComparer.OrdinalIgnoreCase;
            InheritsFrom = new Dictionary<string, string>(comparer);
            ChildrenFullyQualifiedTypes = new Dictionary<string, string>(comparer);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadGeneratedObjectInheritance(SerializationInfo info, StreamingContext context)
        {
            Type = info.GetString("Type");
            FullyQualifiedType = info.GetString("FullyQualifiedType");
            AssemblyFullyQualifiedName = info.GetString("AssemblyFullyQualifiedName");
            ParentFullyQualifiedType = info.GetString("ParentFullyQualifiedType");
            ChildrenFullyQualifiedTypes = (Dictionary<string, string>)info.GetValue("ChildrenFullyQualifiedTypes", typeof(Dictionary<string, string>));
            InheritsFrom = (Dictionary<string, string>)info.GetValue("InheritsFrom", typeof(Dictionary<string, string>));
           
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", Type);
            info.AddValue("FullyQualifiedType ", FullyQualifiedType);
            info.AddValue("AssemblyFullyQualifiedName", AssemblyFullyQualifiedName);
            info.AddValue("ParentFullyQualifiedType", ParentFullyQualifiedType);
            info.AddValue("ChildrenFullyQualifiedTypes", ChildrenFullyQualifiedTypes);
            info.AddValue("InheritsFrom", InheritsFrom);

        }
    }
}
