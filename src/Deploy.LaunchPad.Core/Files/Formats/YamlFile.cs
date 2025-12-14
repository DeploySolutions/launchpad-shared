using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    /// <summary>
    /// Yaml (YAML)
    /// </summary>
    public partial class YamlFile : FileBase<string, YamlFileSchema>, IYamlFile
    {
        public override string Extension => "." + FileExtensions.yaml;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public YamlFile(string fileName) : base(fileName)
        {

        }
    }
}
