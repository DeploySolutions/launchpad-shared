// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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



namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    

    /// <summary>
    /// This class represents a programmable hardware/software device (that is part of the Internet-of-Things or Web-of-Things world).
    /// </summary>
    [Serializable()]
    public partial class Device<TIdType> : DomainEntityBase<TIdType>, IPhysicallyLocatable

    {
        /// <summary>
        /// This is the current physical location of this device
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual SpaceTimeInformation CurrentLocation
        {
            get;
            set;
        }

        /// <summary>
        /// A list (not necessarily comprehensive) of this object's previous (but not current) physical positions. 
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IList<SpaceTimeInformation> PreviousLocations
        {
            get;
            set;
        }

        /// <summary>
        /// The current power level of the device 
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DevicePower Power { get; set; }

        #region "Constructors"
        /// <summary>  
        /// Initializes a new instance of the <see cref="Device">Device</see> class
        /// </summary>
        public Device(int? tenantId) : base(tenantId)
        {
            CurrentLocation = new SpaceTimeInformation();
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given some metadata. 
        /// </summary>
        /// <param name="metadata">The desired metadata for this Device</param>
        public Device(int? tenantId,MetadataInformation metadata) : base(tenantId)
        {
            Metadata = metadata;
            CurrentLocation = new SpaceTimeInformation();
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }
        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="key">The unique identifier for this device</param>
        /// <param name="metadata">The desired metadata for this device</param>
        /// <param name="currentLocation">The current physical location of this device</param>
        public Device(int? tenantId, TIdType id, MetadataInformation metadata, SpaceTimeInformation currentLocation) 
            :base(tenantId)
        {
            Id = id;
            Metadata = metadata;
            CurrentLocation = currentLocation;
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given a key, some metadata, and its current and previous locations. 
        /// </summary>
        /// <param name="key">The unique identifier for this Device</param>
        /// <param name="metadata">The desired metadata for this Device</param>
        /// <param name="currentLocation">The current physical location of this device</param>
        /// <param name="previousLocations">The previous physical location(s) of this device</param>
        public Device(int? tenantId,DomainEntityKey<TIdType> key, MetadataInformation metadata, SpaceTimeInformation currentLocation, IList<SpaceTimeInformation> previousLocations)
            :  base(tenantId,key,metadata)
        {
            CurrentLocation = currentLocation;
            PreviousLocations = previousLocations;
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given a key, metadata, its current location, and its power level. 
        /// </summary>
        /// <param name="id">The unique identifier for this Device</param>
        /// <param name="metadata">The desired metadata for this Device</param>
        /// <param name="currentLocation">The current physical location of this device</param>
        /// <param name="power">The current power level of this device</param>
        public Device(int? tenantId, TIdType id, MetadataInformation metadata, SpaceTimeInformation currentLocation, DevicePower power):
            base (tenantId)
        {
            Id = id;
            Key.Culture = "en";
            Metadata = metadata;
            CurrentLocation = currentLocation;
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = power;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected Device(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            CurrentLocation = (SpaceTimeInformation)info.GetValue("CurrentLocation", typeof(SpaceTimeInformation));
            PreviousLocations = (IList<SpaceTimeInformation>)info.GetValue("PreviousLocations", typeof(IList<SpaceTimeInformation>));
            Power = (DevicePower)info.GetValue("Power", typeof(DevicePower));
        }

#endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CurrentLocation", CurrentLocation);
            info.AddValue("PreviousLocations", PreviousLocations);
            info.AddValue("Power", Power);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Device : ");
            sb.AppendFormat(ToStringBaseProperties());
            if (CurrentLocation!=null)
                sb.AppendFormat(" CurrentLocation={0};", CurrentLocation);
            if (PreviousLocations != null)
                sb.AppendFormat(" PreviousLocations={0};", PreviousLocations);
            sb.Append(Power.ToString());
            sb.Append("]");
            return sb.ToString();
        }

    }
}
