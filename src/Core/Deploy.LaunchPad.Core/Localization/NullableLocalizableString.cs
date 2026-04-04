namespace Deploy.LaunchPad.Core.Localization
{
    public sealed class NullableLocalizableString : LocalizableStringBase
    {
        public NullableLocalizableString()
            : base(string.Empty, string.Empty)
        {
        }

        public override string Localize(ILocalizationContext context)
        {
            // Return null or custom logic for nullable
            return null;
        }

        public override string Localize(ILocalizationContext context, System.Globalization.CultureInfo culture)
        {
            // Return null or custom logic for nullable
            return null;
        }
    }
}