
namespace Deploy.LaunchPad.Core.Localization
{
    public abstract class LocalizableStringBase : ILocalizableString
    {
        public virtual string SourceName { get; protected set; }
        public virtual string Name { get; protected set; }

        protected LocalizableStringBase(string name, string sourceName)
        {
            Name = name;
            SourceName = sourceName;
        }

        public abstract string Localize(ILocalizationContext context);
        public abstract string Localize(ILocalizationContext context, System.Globalization.CultureInfo culture);
    }
}