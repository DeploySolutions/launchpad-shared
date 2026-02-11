using Deploy.LaunchPad.Util.Files.Formats;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Deploy.LaunchPad.Util.Files
{
    public partial class CsvFile : FileBase<string, IFrictionlessFileSchema>, ICsvFile
    {
        public override string Extension => "." + FileExtensions.csv;
        public virtual bool IsHeaderCaseSensitive { get; set; } = false;
        public virtual string Delimiter { get; set; } = ",";
        public virtual char Quote { get; set; } = '"';

        public virtual IList<string> PropertySortOrder { get; set; } = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CsvFile(string fileName) : base(fileName)
        {
        }
    }
}
