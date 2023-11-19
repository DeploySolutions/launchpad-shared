// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-12-2023
// ***********************************************************************
// <copyright file="PythonListJsonConverter.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Deploy.LaunchPad.Python.Json;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Class PythonListJsonConverter.
    /// Implements the <see cref="Newtonsoft.Json.JsonConverter{System.Collections.Generic.List{Deploy.LaunchPad.Python.Json.PythonInstallationJson}}" />
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter{System.Collections.Generic.List{Deploy.LaunchPad.Python.Json.PythonInstallationJson}}" />
    public partial class PythonListJsonConverter : JsonConverter<List<PythonInstallationJson>>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read. If there is no existing value then <c>null</c> will be used.</param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override List<PythonInstallationJson> ReadJson(JsonReader reader, Type objectType, List<PythonInstallationJson> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as List<PythonInstallationJson>;
            if (token.HasValues)
            {
                var childValues = token.Values();
                JTokenType type = childValues.FirstOrDefault().Type;
                if (type != JTokenType.Null && childValues.First().Any())
                {
                    if (type == JTokenType.Object) // it's a single object
                    {
                        var installationToken = childValues.FirstOrDefault();
                        PythonInstallationJson newObject = installationToken.ToObject<PythonInstallationJson>();
                        list.Add(newObject);
                    }
                    if (type == JTokenType.Array) // it's an array of objects
                    {
                        foreach (var child in childValues)
                        {
                            var locationTokenArray = child.Children();
                            foreach (var locationToken in locationTokenArray)
                            {
                                PythonInstallationJson newObject = locationToken.ToObject<PythonInstallationJson>();
                                list.Add(newObject);
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(JsonWriter writer, List<PythonInstallationJson> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    
}