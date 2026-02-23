using System;
using System.Collections.Generic;
using System.Globalization;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial class CsvFile : FileBase<string, IFrictionlessFileSchema>, ICsvFile
    {
        public override string Extension => "." + FileExtension.csv;
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
