// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="SatelliteBase.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Space.Satellites.Core
{
    using Deploy.LaunchPad.Core.Abp.Domain.Model;
    using Deploy.LaunchPad.Space.Satellites.Core.Observations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// Class SatelliteBase.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.Core.ISatellite" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.Core.ISatellite" />
    public abstract partial class SatelliteBase<TPrimaryKey> : LaunchPadDomainEntityBase<TPrimaryKey>, ISatellite
    {


        /// <summary>
        /// Gets or sets the reference system.
        /// </summary>
        /// <value>The reference system.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual string ReferenceSystem { get; set; }

        /// <summary>
        /// Gets or sets the orbital regime.
        /// </summary>
        /// <value>The orbital regime.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual string OrbitalRegime { get; set; }

        /// <summary>
        /// Gets or sets the altitude in km.
        /// </summary>
        /// <value>The altitude in km.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual double AltitudeInKm { get; set; }

        /// <summary>
        /// Gets or sets the inclination degrees.
        /// </summary>
        /// <value>The inclination degrees.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual double InclinationDegrees { get; set; }

        /// <summary>
        /// Gets or sets the orbital period in minutes.
        /// </summary>
        /// <value>The orbital period in minutes.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual double OrbitalPeriodInMinutes { get; set; }

        /// <summary>
        /// The operators
        /// </summary>
        protected IDictionary<Guid, ISatelliteOperator<Guid>> _operators;
        /// <summary>
        /// Gets or sets the operators.
        /// </summary>
        /// <value>The operators.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<Guid, ISatelliteOperator<Guid>> Operators
        {
            get { return _operators; }
            set { _operators = value; }
        }

        /// <summary>
        /// The satellite catalog number
        /// </summary>
        protected string _satelliteCatalogNumber;
        /// <summary>
        /// Gets or sets the satellite catalog number.
        /// </summary>
        /// <value>The satellite catalog number.</value>
        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public virtual string SatelliteCatalogNumber
        {
            get { return _satelliteCatalogNumber; }
            set { _satelliteCatalogNumber = value; }
        }

        /// <summary>
        /// The cospar identifier
        /// </summary>
        protected string _cosparID;
        /// <summary>
        /// Gets or sets the cospar identifier.
        /// </summary>
        /// <value>The cospar identifier.</value>
        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public virtual string CosparID
        {
            get { return _cosparID; }
            set { _cosparID = value; }
        }

        /// <summary>
        /// The website
        /// </summary>
        protected Uri _website;
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri Website
        {
            get { return _website; }
            set { _website = value; }
        }


        /// <summary>
        /// Gets or sets the sensors.
        /// </summary>
        /// <value>The sensors.</value>
        [Required]
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<string, ISensor> Sensors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SatelliteBase{TPrimaryKey}"/> class.
        /// </summary>
        protected SatelliteBase() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Sensors = new Dictionary<string, ISensor>(comparer);
            Operators = new Dictionary<Guid, ISatelliteOperator<Guid>>();
            SatelliteCatalogNumber = string.Empty;
            CosparID = string.Empty;
        }
    }
}
