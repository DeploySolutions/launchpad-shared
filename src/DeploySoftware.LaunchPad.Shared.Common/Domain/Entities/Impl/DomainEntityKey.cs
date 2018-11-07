﻿//LaunchPad Shared
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
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    using DeploySoftware.LaunchPad.Common.Util;

    /// <summary>
    /// This class uniquely identifies an object implementing <see cref="IDomainEntity">IDomainEntity</see>.
    /// </summary>
    [ComplexType]
    public partial class DomainEntityKey : KeyBase<Guid>
    {

        /// <summary>
        /// The Globally-Unique ID that uniquely identifies this object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public override Guid UniqueKey { get; set; }
        
        /// <summary>  
        /// Initializes a new instance of the <see cref="DomainEntityKey">DomainEntityKey</see> class.  
        /// </summary>
        public DomainEntityKey()
            : base()
        {
            //UniqueId = SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString);
            UniqueKey = Guid.NewGuid();
            CultureName = "en";
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="DomainEntityKey">DomainEntityKey</see> class
        /// with the given Id. 
        /// </summary>
        /// <param name="id">The unique Id of this entity</param>
        public DomainEntityKey(Guid id)
            : base()
        {
            UniqueKey = id;
            CultureName = "en";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityKey">DomainEntityKey</see> class
        /// with the provided Id and Culture Name
        /// </summary>
        /// <param name="id">The unique Id of this entity</param>
        /// <param name="cultureName">The culture code of this entity</param>        
        public DomainEntityKey(Guid id, String cultureName)
            : base(cultureName)
        {
            UniqueKey = id;
            CultureName = cultureName;
        }
        
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DomainEntityKey(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            UniqueKey = (Guid)info.GetValue("UniqueId", typeof(Guid));
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
            info.AddValue("CultureName", CultureName);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DomainEntityKey: ");
            sb.AppendFormat("UniqueId={0};", UniqueKey);
            sb.AppendFormat("CultureName={0};", CultureName);   
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is DomainEntityKey)
            {
                return Equals(obj as DomainEntityKey);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.
        /// That means: the Entity ID, and the culture.
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns></returns>
        public virtual bool Equals(DomainEntityKey obj)
        {
            if (obj != null)
            {
                // for safe equality we need to match on business key equality.
                // These entities are functionally equal if the Id and Culture and BaseUri are equal
                return (
                    UniqueKey.Equals(obj.UniqueKey) &&
                    CultureName.Equals(obj.CultureName) 
                    );
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
