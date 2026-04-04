using Deploy.LaunchPad.Files.Templates;
using Deploy.LaunchPad.Util.Collections;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Templates.Configuration
{
    public partial interface ITemplateConfiguration
    {
        /// <summary>
        /// Template provider.
        /// </summary>
        ITypeList<TemplateProvider> Providers { get; }

        IDictionary<string, ILaunchPadTemplate> Templates { get; }

        ILaunchPadTemplate? GetOrNull(string name);
    }
}
