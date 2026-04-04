

using System.Globalization;

namespace Deploy.LaunchPad.Core.Localization
{
    public abstract partial class LocalizedStringBase : ILocalizedString
    {
        public virtual CultureInfo CultureInfo { get; internal set; } = CultureInfo.InvariantCulture;
        public virtual string Name { get; protected set; } = string.Empty;
        public virtual string Value { get; protected set; } = string.Empty;

        protected LocalizedStringBase(string name, string value, CultureInfo cultureInfo)
        {
            Name = name;
            Value = value;
            CultureInfo = cultureInfo;
        }
    }
}