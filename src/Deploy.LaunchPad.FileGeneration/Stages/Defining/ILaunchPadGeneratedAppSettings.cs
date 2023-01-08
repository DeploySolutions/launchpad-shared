using Castle.Core.Logging;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace Deploy.LaunchPad.FileGeneration.Stages.Defining
{
    public interface ILaunchPadGeneratedAppSettings
    {

        public T LoadAppSettingsFromXml<T>(XmlDocument doc, ILogger logger, string xmlns)
           where T : ILaunchPadGeneratedAppSettings, new();

        public JObject ToJson();



    }
}
