// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DevicePower.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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


namespace Deploy.LaunchPad.Core.Domain.Devices
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// This class presents information on the current power level of a device
    /// </summary>
    [Serializable()]
    public partial class DevicePower : IDevicePower
    {
        /// <summary>
        /// The estimated date and time until this device is fully drained, at current power expenditure.
        /// If set to null, the device is not draining or is already drained, or is in an unknown state.
        /// </summary>
        /// <value>The remaining charge time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? RemainingChargeTime
        {
            get; set;
        }

        /// <summary>
        /// Indicates the relative state of the power supply
        /// </summary>
        /// <value>The power level.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public DevicePowerChargeLevel PowerLevel { get; set; }


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicePower"/> class.
        /// </summary>
        public DevicePower()
        {
            RemainingChargeTime = null;
            PowerLevel = DevicePowerChargeLevel.Unknown;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicePower"/> class.
        /// </summary>
        /// <param name="powerLevel">The power level.</param>
        /// <param name="remainingCharge">The remaining charge.</param>
        public DevicePower(DevicePowerChargeLevel powerLevel, DateTime remainingCharge)
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
            PowerLevel = (DevicePowerChargeLevel)info.GetValue("PowerLevel", typeof(DevicePowerChargeLevel));
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
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
            sb.Append(']');
            return sb.ToString();
        }
    }
}
