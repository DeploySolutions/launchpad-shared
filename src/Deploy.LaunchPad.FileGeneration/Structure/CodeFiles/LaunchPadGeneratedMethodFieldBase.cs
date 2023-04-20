using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using static System.Formats.Asn1.AsnWriter;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represent a C# method parameter or field
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedMethodFieldBase : LaunchPadGeneratedObjectBase
    {

        public virtual LaunchPadGeneratedItemType ItemType { get; set; }

        public LaunchPadGeneratedMethodFieldBase() : base()
        {
            ItemType = LaunchPadGeneratedItemType.Custom;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadGeneratedMethodFieldBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ItemType = (LaunchPadGeneratedItemType)info.GetValue("Id", typeof(LaunchPadGeneratedItemType));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ItemType", ItemType);
        }



    }
}
