using Castle.Core.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages.Defining
{
    public interface ILaunchPadGeneratedAppSettings
    {

        public T LoadAppSettingsFromXml<T>(XmlDocument doc, ILogger logger, string xmlns)
           where T : ILaunchPadGeneratedAppSettings, new();

        public JObject ToJson();



    }
}
