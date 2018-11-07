//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Common.Domain.Entities
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    using System;
    using Util;
    /// <summary>
    /// This class uniquely identifies a file (any object implementing <see cref="IFile">IFile</see>).
    /// </summary>
    public class FileKey : KeyBase<String>
    {
        /// <summary>
        /// The ID that uniquely identifies this object (usually the full file path and filename).
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public override String UniqueKey { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="FileKey">FileKey</see> class.  
        /// </summary>
        public FileKey()
            : base()
        {
            // if no key is provided, use a sequential guid as the file key
            UniqueKey = SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString).ToString();
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="FileKey">FileKey</see> class
        /// with the given Id. 
        /// </summary>
        /// <param name="id">The unique Id of this file (usually the full file path and filename)</param>
        public FileKey(String id)
            : base()
        {
            UniqueKey = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileKey">FileKey</see> class
        /// with the provided Id and Culture Name
        /// </summary>
        /// <param name="id">The unique Id of this file (usually the full file path and filename)</param>
        /// <param name="cultureName">The culture code of this entity</param>
        public FileKey(String id, String cultureName)
            : base(cultureName)
        {
            UniqueKey = id;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileKey(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            UniqueKey = info.GetString("Id");
            CultureName = info.GetString("CultureName");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("UniqueId", UniqueKey);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[FileKey: ");
            sb.AppendFormat("UniqueId={0};", UniqueKey);
            sb.AppendFormat("CultureName={0};", CultureName);
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) a File</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is FileKey)
            {
                return Equals(obj as FileKey);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.
        /// That means: the File ID, and the culture.
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns></returns>
        public virtual bool Equals(FileKey obj)
        {
            if (obj != null)
            {
                // for safe equality we need to match on business key equality.
                // These entities are functionally equal if the Id and Culture are equal
                return (
                    UniqueKey.Equals(obj.UniqueKey) &&
                    CultureName.Equals(obj.CultureName));
            }
            return false;
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return UniqueKey.GetHashCode() + Culture.GetHashCode();
        }
    }
}

