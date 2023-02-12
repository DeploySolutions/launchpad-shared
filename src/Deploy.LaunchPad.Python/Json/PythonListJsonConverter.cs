using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Deploy.LaunchPad.Python.Json;

namespace Deploy.LaunchPad.Python
{
    public partial class PythonListJsonConverter : JsonConverter<List<PythonInstallationJson>>
    {
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

        public override void WriteJson(JsonWriter writer, List<PythonInstallationJson> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    
}