//LaunchPad Space
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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


namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{

    using DeploySoftware.LaunchPad.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public abstract class SatelliteBase<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, ISatellite
    {
        private IEnumerable<ISatelliteOperator<Guid>> _operators;
        private string _satelliteCatalogNumber;
        private string _cosparID;
        private Uri _website;
        
        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public IEnumerable<ISatelliteOperator<Guid>> Operators
        {
            get { return _operators; }
            set { _operators = value; }
        }

        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public string SatelliteCatalogNumber
        {
            get { return _satelliteCatalogNumber; }
            set { _satelliteCatalogNumber = value; }
        }

        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public string CosparID
        {
            get { return _cosparID; }
            set { _cosparID = value; }
        }

        [System.ComponentModel.DataObjectField(true)]
        [XmlAttribute]
        public Uri Website
        {
            get { return _website; }
            set { _website = value; }
        }

        protected SatelliteBase() : base()
        {
            Operators = null; 
            SatelliteCatalogNumber = string.Empty;
            CosparID = string.Empty;
        }
    }
}
