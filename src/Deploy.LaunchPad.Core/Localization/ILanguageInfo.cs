namespace Deploy.LaunchPad.Core.Localization
{
    public partial interface ILanguageInfo
    {
        string DisplayName { get; set; }
        string Icon { get; set; }
        bool IsDefault { get; set; }
        bool IsDisabled { get; set; }
        bool IsRightToLeft { get; }
        string Name { get; set; }
    }
}