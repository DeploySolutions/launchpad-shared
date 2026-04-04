using Deploy.LaunchPad.Core.Localization.Sources;
using System.Globalization;

namespace Deploy.LaunchPad.Core.Localization
{
    public partial interface ILocalizationHelper
    {
        public ILocalizationManager Manager { get; }

        ILocalizationSource GetSource(string name);
        string GetString(string sourceName, string name);
        string GetString(string sourceName, string name, CultureInfo culture);
    }
}