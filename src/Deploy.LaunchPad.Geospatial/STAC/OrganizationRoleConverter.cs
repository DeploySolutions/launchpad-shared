// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="OrganizationRoleConverter.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Geospatial.STAC
{
    /// <summary>
    /// Class OrganizationRoleConverter.
    /// Implements the <see cref="JsonConverter" />
    /// </summary>
    /// <seealso cref="JsonConverter" />
    internal class OrganizationRoleConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns><c>true</c> if this instance can convert the specified t; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type t) => t == typeof(OrganizationRole) || t == typeof(OrganizationRole?);

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="t">The t.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.Exception">Cannot unmarshal type OrganizationRole</exception>
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "host":
                    return OrganizationRole.Host;
                case "licensor":
                    return OrganizationRole.Licensor;
                case "processor":
                    return OrganizationRole.Processor;
                case "producer":
                    return OrganizationRole.Producer;
            }
            throw new Exception("Cannot unmarshal type OrganizationRole");
        }

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="untypedValue">The untyped value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <exception cref="System.Exception">Cannot marshal type OrganizationRole</exception>
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OrganizationRole)untypedValue;
            switch (value)
            {
                case OrganizationRole.Host:
                    serializer.Serialize(writer, "host");
                    return;
                case OrganizationRole.Licensor:
                    serializer.Serialize(writer, "licensor");
                    return;
                case OrganizationRole.Processor:
                    serializer.Serialize(writer, "processor");
                    return;
                case OrganizationRole.Producer:
                    serializer.Serialize(writer, "producer");
                    return;
            }
            throw new Exception("Cannot marshal type OrganizationRole");
        }

        /// <summary>
        /// The singleton
        /// </summary>
        public static readonly OrganizationRoleConverter Singleton = new OrganizationRoleConverter();
    }

}
