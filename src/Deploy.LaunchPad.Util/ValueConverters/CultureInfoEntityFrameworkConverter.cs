
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace Deploy.LaunchPad.Util.ValueConverters
{
    public partial class CultureInfoEntityFrameworkConverter : ValueConverter<CultureInfo, string>
    {
        public CultureInfoEntityFrameworkConverter()
            : base(
                c => c == null ? null : c.Name, // Store CultureInfo.Name as string
                s => string.IsNullOrEmpty(s) ? null : new CultureInfo(s))
        { }
    }
}
