using Deploy.LaunchPad.Core.Localization;
using System;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMustHaveDisplayName
    {
        public ILocalizableString DisplayName { get; }
    }
}