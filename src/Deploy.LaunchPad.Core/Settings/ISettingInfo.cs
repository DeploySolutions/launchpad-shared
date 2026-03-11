using System;

namespace Deploy.LaunchPad.Core.Configuration
{
    public interface ISettingInfo
    {
        string Name { get; set; }
        Guid? TenantId { get; set; }
        Guid? UserId { get; set; }
        string Value { get; set; }
    }
}