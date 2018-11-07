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
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// This class presents information on the current power level of a device
    /// </summary>
    [Serializable()]
    public class DevicePower
    {
        /// <summary>
        /// The estimated date and time until this device is fully drained, at current power expenditure.
        /// If set to null, the device is not draining or is already drained, or is in an unknown state.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? RemainingChargeTime
        {
            get;set;
        }

        /// <summary>
        /// Indicates the relative state of the power supply
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public PowerChargeLevel PowerLevel { get; set; }

        public enum PowerChargeLevel
        {
            Unknown = 0,
            Charged = 1,
            Charging = 2,
            Draining = 3,
            Drained = 4
        }               

#region "Constructors"

        public DevicePower()
        {
            RemainingChargeTime = null;
            PowerLevel = PowerChargeLevel.Unknown;          
        }

        public DevicePower(PowerChargeLevel powerLevel, DateTime remainingCharge)
        {
            RemainingChargeTime = remainingCharge;
            PowerLevel = powerLevel;
        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DevicePower(SerializationInfo info, StreamingContext context)
        {
            RemainingChargeTime = (Nullable<DateTime>)info.GetValue("RemainingChargeTime", typeof(Nullable<DateTime>));
            PowerLevel = (PowerChargeLevel)info.GetValue("PowerLevel", typeof(PowerChargeLevel));
        }

#endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RemainingChargeTime", RemainingChargeTime);
            info.AddValue("PowerLevel", PowerLevel);            
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DevicePower : ");
            sb.AppendFormat(" PowerLevel={0};", PowerLevel);
            if (RemainingChargeTime.HasValue)
            {
                sb.AppendFormat(" RemainingChargeTime={0};", RemainingChargeTime);
            }
            else
            {
                sb.AppendFormat(" RemainingChargeTime=null;");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
