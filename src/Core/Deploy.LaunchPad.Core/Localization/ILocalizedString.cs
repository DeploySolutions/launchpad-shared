using System.Globalization;

namespace Deploy.LaunchPad.Core.Localization
{
    public partial interface ILocalizedString
    {
        CultureInfo CultureInfo { get; }
        string Name { get; }
        string Value { get; }
    }
}