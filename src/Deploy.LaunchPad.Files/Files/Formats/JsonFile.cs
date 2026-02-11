
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Deploy.LaunchPad.Files.Formats;

namespace Deploy.LaunchPad.Files
{
    public partial class JsonFile : FileBase<JToken, JToken>, IJsonFile
    {
        public override string Extension => "." + FileExtensions.json;

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonFile(string fileName) : base(fileName)
        {
        }
    }
}
