// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="MetadataTag.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

namespace Deploy.LaunchPad.Core.Domain
{

    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Text;


    /// <summary>
    /// Class MetadataTag.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Domain.TagBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Domain.ILaunchPadMetadataTag" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Domain.TagBase" />
    /// <seealso cref="Deploy.LaunchPad.Core.Domain.ILaunchPadMetadataTag" />
    [Serializable()]
    [Table("DssMetadataTag")]
    public partial class MetadataTag : TagBase, ILaunchPadMetadataTag
    {

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataTag">MetadataTag</see> class
        /// </summary>
        public MetadataTag() : base()
        {


        }

        /// <summary>
        /// Creates a new instance of the <see cref="MetadataTag">MetadataTag</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="key">The key for this tag</param>
        /// <param name="value">The desired value for this tag</param>
        public MetadataTag(String key, String value) : base(key, value)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MetadataTag">MetadataTag</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="key">The key for this tag</param>
        /// <param name="value">The desired value for this tag</param>
        /// <param name="schema">The desired schema for this tag</param>
        public MetadataTag(String key, String value, String schema) : base(key, value, schema)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected MetadataTag(SerializationInfo info, StreamingContext context)
        {
            Key = info.GetString("Key");
            Value = info.GetString("Value");
            Schema = info.GetString("Schema");
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Key", Key);
            info.AddValue("Schema", Schema);
            info.AddValue("Value", Value);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Tag : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }
    }
}
