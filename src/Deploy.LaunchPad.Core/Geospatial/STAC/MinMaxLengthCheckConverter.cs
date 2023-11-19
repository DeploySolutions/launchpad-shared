// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="MinMaxLengthCheckConverter.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.STAC
{

    /// <summary>
    /// Class MinMaxLengthCheckConverter.
    /// Implements the <see cref="JsonConverter" />
    /// </summary>
    /// <seealso cref="JsonConverter" />
    internal class MinMaxLengthCheckConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns><c>true</c> if this instance can convert the specified t; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type t) => t == typeof(string);

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="t">The t.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.Exception">Cannot unmarshal type string</exception>
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            if (value.Length >= 1)
            {
                return value;
            }
            throw new Exception("Cannot unmarshal type string");
        }

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="untypedValue">The untyped value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <exception cref="System.Exception">Cannot marshal type string</exception>
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (string)untypedValue;
            if (value.Length >= 1)
            {
                serializer.Serialize(writer, value);
                return;
            }
            throw new Exception("Cannot marshal type string");
        }

        /// <summary>
        /// The singleton
        /// </summary>
        public static readonly MinMaxLengthCheckConverter Singleton = new MinMaxLengthCheckConverter();
    }
}
