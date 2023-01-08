using Deploy.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The base class containing properties for all LaunchPad RAD file generation processes.
    /// This is the top level element in the LaunchPad Generated object hierarchy. 
    /// </summary>    
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract partial class LaunchPadGeneratedObjectBase : ILaunchPadGeneratedObject
    {
        /// <summary>
        /// The unique id of the object (if present)
        /// </summary>
        [JsonProperty("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// The singular name of the object 
        /// </summary>
        [JsonProperty("name")]
        public virtual string Name { get; set; } = string.Empty;


        /// <summary>
        /// The abbreviation of the object (if any)
        /// </summary>
        [JsonConverter(typeof(LocalizedJsonConverter<string>))]
        public virtual string Abbreviation { get; set; } = string.Empty;

        /// <summary>
        /// The prefix to apply to the name (if any).
        /// </summary>
        public virtual string NamePrefix { get; set; } = string.Empty;

        /// <summary>
        /// The suffix to apply to the name (if any).
        /// </summary>
        public virtual string NameSuffix { get; set; } = string.Empty;


        /// <summary>
        /// The description of the object
        /// </summary>
        [JsonProperty("description")]
        public virtual string Description { get; set; } = string.Empty;

        /// <summary>
        /// Code annotations for the object
        /// </summary>
        public virtual string Annotations { get; set; } = string.Empty;

        /// <summary>
        /// The C# type of this object
        /// </summary>
        public virtual string ObjectTypeName { get; set; } = string.Empty;

        /// <summary>
        /// The C# full type name of this object
        /// </summary>
        public virtual string ObjectTypeFullName { get; set; } = string.Empty;

        /// <summary>
        /// The assembly name in which this C# object is defined
        /// </summary>
        public virtual string ObjectTypeAssemblyName { get; set; } = string.Empty;

        /// <summary>
        /// The C# type of this object's id.
        /// </summary>
        public virtual string IdType { get; set; } = "System.Int32";


        public LaunchPadGeneratedObjectBase() : base()
        {
            ObjectTypeName = this.GetType().Name;
            ObjectTypeFullName = this.GetType().FullName;
            ObjectTypeAssemblyName = this.GetType().Assembly.FullName;
            IdType = string.Empty;
            Id = string.Empty;
        }

    }
}
