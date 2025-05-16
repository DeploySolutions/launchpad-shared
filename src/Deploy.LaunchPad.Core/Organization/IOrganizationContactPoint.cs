using System.Collections.Generic;
using System.Globalization;

namespace Deploy.LaunchPad.Core.Organization
{
    public partial interface IOrganizationContactPoint
    {
        List<CultureInfo> AvailableLanguages { get; set; }
        string ContactType { get; set; }
        string Culture { get; }
        List<string> Email { get; set; }
        string FaxNumber { get; set; }
        ElementNameLight Name { get; }
        List<string> Telephone { get; set; }
    }
}