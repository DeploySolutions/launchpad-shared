﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class FrictionlessSchema : FileBase<JToken, JToken>, IFrictionlessSchema
    {
        public override string Extension => ".json";

        public virtual bool ShouldGenerateDataPackage { get; set; } = true;

        /// Initializes a new instance of the <see cref="FrictionlessSchemaFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public FrictionlessSchema(string fileName) : base(fileName)
        {

        }
    }
}
