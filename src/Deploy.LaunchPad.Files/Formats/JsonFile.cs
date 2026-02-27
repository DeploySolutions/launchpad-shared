
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial class JsonFile : FileBase<JToken, JToken>, IJsonFile
    {
        public override string Extension => "." + FileExtension.json;

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonFile(string fileName) : base(fileName)
        {
        }
    }
}
