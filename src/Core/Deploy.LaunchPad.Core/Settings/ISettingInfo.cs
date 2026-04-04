using System;

namespace Deploy.LaunchPad.Core.Configuration
{
    public partial interface ISettingInfo
    {
        string Name { get; set; }
        Guid? TenantId { get; set; }
        Guid? UserId { get; set; }
        string Value { get; set; }
    }
}