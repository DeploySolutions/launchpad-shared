using System;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The base class containing properties for all LaunchPad RAD file generation processes.
    /// This is the top level element in the LaunchPad Generated object hierarchy. 
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedObjectBase : ILaunchPadGeneratedObject
    {
        /// <summary>
        /// The unique id of the object (if present)
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// The singular name of the object 
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// The prefix to apply to the name (if any).
        /// </summary>
        public virtual string NamePrefix { get; set; }

        /// <summary>
        /// The suffix to apply to the name (if any).
        /// </summary>
        public virtual string NameSuffix { get; set; }


        /// <summary>
        /// The description of the object
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Code annotations for the object
        /// </summary>
        public virtual string Annotations { get; set; }

        /// <summary>
        /// The C# type of this object
        /// </summary>
        public virtual string ObjectType { get; set; }

        /// <summary>
        /// The C# type of this object's id.
        /// </summary>
        public virtual string IdType { get; set; } = "System.Int32";

        public LaunchPadGeneratedObjectBase() : base()
        {
            ObjectType = this.GetType().Name;
            IdType = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
            NamePrefix = string.Empty;
            NameSuffix = string.Empty;
            Description = string.Empty;
            Annotations = string.Empty;
        }

    }
}
