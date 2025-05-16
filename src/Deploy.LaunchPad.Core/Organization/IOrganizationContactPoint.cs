using System.Collections.Generic;
using System.Globalization;

namespace Deploy.LaunchPad.Core.Organization
{
    public partial interface IOrganizationContactPoint
    {
        string Name { get; }
        List<CultureInfo> AvailableLanguages { get; set; }
        string ContactType { get; set; }
        List<string> Email { get; set; }
        string FaxNumber { get; set; }
        List<string> Telephone { get; set; }
    }
}