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
    [Serializable]
    public partial class LaunchPadGeneratedObjectInheritance : ILaunchPadGeneratedObjectInheritance
    {
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Type { get; set; }

        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string FullyQualifiedType { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string AssemblyFullyQualifiedName { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string ParentFullyQualifiedType { get; set; } = string.Empty;

        /// <summary>
        /// If tracked/known, specify the fully qualified type names of any children entities (ex. MyCorp.MyApp.Orders.LineItems)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IDictionary<string, string> ChildrenFullyQualifiedTypes { get; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<string, string> InheritsFrom { get; set; }

        public LaunchPadGeneratedObjectInheritance()
        {

            Type = this.GetType().Name;
            FullyQualifiedType = this.GetType().FullName;
            AssemblyFullyQualifiedName = this.GetType().Assembly.FullName;
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
        /// <param name="info"></param>
        /// <param name="context"></param>
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
