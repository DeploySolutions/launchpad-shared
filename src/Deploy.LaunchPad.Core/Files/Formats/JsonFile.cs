using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class JsonFile : FileBase<JToken, JToken>, IJsonFile
    {
        public override string Extension => ".json";


        /// <summary>
        /// Constructor
        /// </summary>
        public JsonFile(string fileName) : base(fileName)
        {
        }
    }
}
