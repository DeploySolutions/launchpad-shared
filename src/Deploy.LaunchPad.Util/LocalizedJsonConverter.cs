// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LocalizedJsonConverter.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Class LocalizedJsonConverter.
    /// Implements the <see cref="JsonConverter" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="JsonConverter" />
    public partial class LocalizedJsonConverter<T> : JsonConverter
    {
        /// <summary>
        /// Class Value.
        /// </summary>
        class Value
        {
            /// <summary>
            /// Gets or sets the en.
            /// </summary>
            /// <value>The en.</value>
            public T en { get; set; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.MoveToContent().TokenType)
            {
                case JsonToken.Null:
                    return null;

                default:
                    return serializer.Deserialize<Value>(reader).en;
            }
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, new Value { en = (T)value });
        }
    }

    /// <summary>
    /// Class JsonExtensions.
    /// </summary>
    public static partial class JsonExtensions
    {
        /// <summary>
        /// Moves to content.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>JsonReader.</returns>
        public static JsonReader MoveToContent(this JsonReader reader)
        {
            if (reader.TokenType == JsonToken.None)
                reader.Read();
            while (reader.TokenType == JsonToken.Comment && reader.Read())
                ;
            return reader;
        }
    }
}
