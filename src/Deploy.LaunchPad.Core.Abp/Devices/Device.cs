// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Device.cs" company="Deploy Software Solutions, inc.">
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


using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.Domain.Devices;
using Deploy.LaunchPad.Domain.Metadata;
using Deploy.LaunchPad.Geospatial;
using Deploy.LaunchPad.Geospatial.Position;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using IMayHaveTenant = Deploy.LaunchPad.Core.Entities.IMayHaveTenant;

namespace Deploy.LaunchPad.Core.Abp.Devices
{


    /// <summary>
    /// This class represents a programmable hardware/software device (that is part of the Internet-of-Things or Web-of-Things world).
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    [Serializable()]
    public partial class Device<TIdType> : LaunchPadDomainEntityBase<TIdType>, IDevice,
        IMayHaveTranslationFromId<TIdType>,
        IMustBePhysicallyLocatable, IMayHaveTenant

    {
        /// <summary>
        /// This is the current physical location of this device
        /// </summary>
        /// <value>The current location.</value>
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
        /// <value>The previous locations.</value>
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
        /// <value>The power.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DevicePower Power { get; set; }
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }

        protected TIdType? _translatedFromId;
        /// <summary>
        /// If this object is not a translation this field will be null.
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        /// <value>The translated from identifier.</value>
        [DataObjectField(false)]
        [DataMember(Name = "translatedFromId", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual TIdType? TranslatedFromId
        {
            get { return _translatedFromId; }
            set { _translatedFromId = value; }
        }

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="Device">Device</see> class
        /// </summary>
        public Device() : base()
        {
            CurrentLocation = new SpaceTimeInformation();
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public Device(int? tenantId) : base()
        {
            TenantId = tenantId;
            CurrentLocation = new SpaceTimeInformation();
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device">Device</see> class
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        public Device(int? tenantId, TIdType id) : base(id)
        {
            Id = id;
            TenantId = tenantId;
            CurrentLocation = new SpaceTimeInformation();
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        public Device(int? tenantId, TIdType id, CultureInfo culture) : base(id)
        {
            Id = id;
            TenantId = tenantId;
            Culture = culture;
            CurrentLocation = new SpaceTimeInformation();
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="currentLocation">The current physical location of this device</param>
        public Device(int? tenantId, TIdType id, SpaceTimeInformation currentLocation)
            : base()
        {
            Id = id;
            TenantId = tenantId;
            CurrentLocation = currentLocation;
            PreviousLocations = new List<SpaceTimeInformation>();
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given a key, some metadata, and its current and previous locations.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="currentLocation">The current physical location of this device</param>
        /// <param name="previousLocations">The previous physical location(s) of this device</param>
        public Device(int? tenantId, TIdType id, SpaceTimeInformation currentLocation, IList<SpaceTimeInformation> previousLocations)
            : base(id)
        {
            CurrentLocation = currentLocation;
            PreviousLocations = previousLocations;
            Power = new DevicePower();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Device">Device</see> class given a key, metadata, its current location, and its power level.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The unique identifier for this Device</param>
        /// <param name="currentLocation">The current physical location of this device</param>
        /// <param name="power">The current power level of this device</param>
        public Device(int? tenantId, TIdType id, SpaceTimeInformation currentLocation, DevicePower power) :
            base()
        {
            Id = id;
            TenantId = tenantId;
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
            PreviousLocations = (IList<SpaceTimeInformation>)info.GetValue("PreviousLocations", typeof(List<SpaceTimeInformation>));
            Power = (DevicePower)info.GetValue("Power", typeof(DevicePower));
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
            if (CurrentLocation != null)
                sb.AppendFormat(" CurrentLocation={0};", CurrentLocation);
            if (PreviousLocations != null)
                sb.AppendFormat(" PreviousLocations={0};", PreviousLocations);
            sb.Append(Power.ToString());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Device{System.Int32}"/> to <see cref="Device{TIdType}"/>.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static implicit operator Device<TIdType>(Device<int> v)
        {
            throw new NotImplementedException();
        }
    }
}
