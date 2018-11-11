﻿//LaunchPad Space
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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

using DeploySoftware.LaunchPad.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{
    public abstract class SatelliteBase : DomainEntityBase<int>, ISatellite
    {
        private IEnumerable<ISatelliteOperator<Guid>> _operators;
        private string _satelliteCatalogNumber;
        private string _cosparID;
        private string _website;
        
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
        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        protected SatelliteBase()
        {
            Operators = null; 
            SatelliteCatalogNumber = string.Empty;
            CosparID = string.Empty;
        }
    }
}
