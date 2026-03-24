using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Templates.Configuration
{
    public partial interface ITemplateConfiguration
    {
        /// <summary>
        /// Template provider.
        /// </summary>
        ITemplateProvider Provider { get; init; }

        IDictionary<string, ILaunchPadTemplate> Templates { get; }

        ILaunchPadTemplate? GetOrNull(string name);
    }
}
