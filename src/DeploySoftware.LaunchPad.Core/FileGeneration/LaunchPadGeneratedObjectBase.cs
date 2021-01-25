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
        public string Id { get; set; }

        /// <summary>
        /// The singular name of the object 
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The prefix to apply to the name (if any).
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// The suffix to apply to the name (if any).
        /// </summary>
        public string NameSuffix { get; set; }


        /// <summary>
        /// The description of the object
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The C# type of this object
        /// </summary>
        public string ObjectType { get; set; }

        /// <summary>
        /// The C# type of this object's id.
        /// </summary>
        public string IdType { get; set; } = "System.Int32";

        public LaunchPadGeneratedObjectBase() : base()
        {
            ObjectType = string.Empty;
            IdType = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
            NamePrefix = string.Empty;
            NameSuffix = string.Empty;
            Description = string.Empty;
        }

    }
}
