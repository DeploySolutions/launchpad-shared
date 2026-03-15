using System.Globalization;

namespace Deploy.LaunchPad.Core.Localization
{
    public sealed class NullableLocalizedString : LocalizedStringBase
    {
        public NullableLocalizedString(string name, string? value, CultureInfo cultureInfo)
            : base(name, value, cultureInfo)
        {
        }
    }
}