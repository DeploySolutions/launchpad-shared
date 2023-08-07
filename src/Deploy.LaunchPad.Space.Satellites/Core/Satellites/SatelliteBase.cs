//LaunchPad Space
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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

    public abstract partial class SatelliteBase<TPrimaryKey> : LaunchPadDomainEntityBase<TPrimaryKey>, ISatellite
    {


        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual string ReferenceSystem { get; set; }

        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual string OrbitalRegime { get; set; }

        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual double AltitudeInKm { get; set; }

        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual double InclinationDegrees { get; set; }

        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual double OrbitalPeriodInMinutes { get; set; }

        protected IDictionary<Guid, ISatelliteOperator<Guid>> _operators;
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<Guid, ISatelliteOperator<Guid>> Operators
        {
            get { return _operators; }
            set { _operators = value; }
        }

        protected string _satelliteCatalogNumber;
        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public virtual string SatelliteCatalogNumber
        {
            get { return _satelliteCatalogNumber; }
            set { _satelliteCatalogNumber = value; }
        }

        protected string _cosparID;
        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public virtual string CosparID
        {
            get { return _cosparID; }
            set { _cosparID = value; }
        }

        protected Uri _website;
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri Website
        {
            get { return _website; }
            set { _website = value; }
        }


        [Required]
        [System.ComponentModel.DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<string, ISensor> Sensors { get; set; }

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
