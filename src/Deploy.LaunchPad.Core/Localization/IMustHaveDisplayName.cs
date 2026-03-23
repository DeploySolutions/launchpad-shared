using Deploy.LaunchPad.Core.Localization;
using System;

namespace Deploy.LaunchPad.Core.Localization
{
    public partial interface IMustHaveDisplayName
    {
        public ILocalizableString DisplayName { get; }
    }
}