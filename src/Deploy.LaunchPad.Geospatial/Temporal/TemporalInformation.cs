// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMayHaveAltitude.cs" company="Deploy Software Solutions, inc.">
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

using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Deploy.LaunchPad.Geospatial.Temporal
{
   
    /// <summary>
    /// This class stores the temporal (time) boundaries around something, including its effective date, and potentially a start and end date
    /// </summary>
    /// 
    [Serializable]
    public partial class TemporalInformation
    {
        public virtual TemporalContext Context { get; set; } = TemporalContext.IsUnknownOrUnspecified;

        /// <summary>
        /// The searchable effective date of the item, in UTC (Formatted in RFC 3339)
        /// https://github.com/radiantearth/stac-spec/blob/master/commons/common-metadata.md#date-and-time-range
        /// </summary>
        /// <value>Effective date / time of the object, in UTC.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? EffectiveDateTimeInUtc { get; set; }

        /// <summary>
        /// The searchable start date of the item, in UTC (Formatted in RFC 3339)
        /// https://github.com/radiantearth/stac-spec/blob/master/commons/common-metadata.md#date-and-time-range
        /// </summary>
        /// <value>Start date / time of the object, in UTC.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? StartDateTimeInUtc { get; set; }

        /// <summary>
        /// The searchable end date of the item, in UTC (Formatted in RFC 3339)
        /// https://github.com/radiantearth/stac-spec/blob/master/commons/common-metadata.md#date-and-time-range
        /// </summary>
        /// <value>End date / time of the object, in UTC.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? EndDateTimeInUtc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public TemporalInformation()
        {
            EffectiveDateTimeInUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public TemporalInformation(TemporalContext context, DateTimeOffset? effectiveDateTimeInUtc, DateTimeOffset? startDateTimeInUtc, DateTimeOffset? endDateTimeInUtc)
        {
            Context = context;
            EffectiveDateTimeInUtc = effectiveDateTimeInUtc;
            StartDateTimeInUtc = startDateTimeInUtc;
            EndDateTimeInUtc = endDateTimeInUtc;
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public TemporalInformation(SerializationInfo info, StreamingContext context)
        {
            Context = (TemporalContext)info.GetValue("Context", typeof(TemporalContext));
            EffectiveDateTimeInUtc = (DateTimeOffset?)info.GetValue("EffectiveDateTimeInUtc", typeof(DateTimeOffset?));
            EndDateTimeInUtc = (DateTimeOffset?)info.GetValue("EndDateTimeInUtc", typeof(DateTimeOffset?));
            StartDateTimeInUtc = (DateTimeOffset?)info.GetValue("StartDateTimeInUtc", typeof(DateTimeOffset?));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Context", Context);
            info.AddValue("EffectiveDateTimeInUtc", EffectiveDateTimeInUtc);
            info.AddValue("EndDateTimeInUtc", EndDateTimeInUtc);
            info.AddValue("StartDateTimeInUtc", StartDateTimeInUtc);
        }

        /// <summary>
        /// finite resources that
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other
        public virtual void OnDeserialization(object sender)
        {
            // Recalculate derived properties if needed

            // reconnect connection strings and other resources that won't be serialized
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[TemporalInformation : ");
            sb.Append(string.Format("Context: {0}", Context));
            sb.Append(string.Format("EffectiveDateTimeInUtc: {0}", EffectiveDateTimeInUtc));
            sb.Append(string.Format("StartDateTimeInUtc: {0}", StartDateTimeInUtc));
            sb.Append(string.Format("EndDateTimeInUtc: {0}", EndDateTimeInUtc));
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast objectToSerialize in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) the right type</param>
        /// <returns>True if the objects are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is TemporalInformation)
            {
                return Equals(obj as TemporalInformation);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type. We need to test for
        /// property equality - which in this case means the comparing double vlaues to each other, which is imprecise. So we need to accept some small level of imprecision
        /// </summary>
        /// <param name="obj">The other object of this type we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(TemporalInformation obj)
        {
            if (obj == null)
            {
                return false;
            }

            // Compare Context
            bool contextEqual = Context == obj.Context;

            // Compare EffectiveDateTimeInUtc
            bool effectiveDateEqual = Nullable.Equals(EffectiveDateTimeInUtc, obj.EffectiveDateTimeInUtc);

            // Compare StartDateTimeInUtc
            bool startDateEqual = Nullable.Equals(StartDateTimeInUtc, obj.StartDateTimeInUtc);

            // Compare EndDateTimeInUtc
            bool endDateEqual = Nullable.Equals(EndDateTimeInUtc, obj.EndDateTimeInUtc);

            // Return true only if all properties are equal
            return contextEqual && effectiveDateEqual && startDateEqual && endDateEqual;
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Context.GetHashCode() + EffectiveDateTimeInUtc.GetHashCode() 
                + StartDateTimeInUtc.GetHashCode()
                + EndDateTimeInUtc.GetHashCode()
            ;
        }
    }
}