using System;

namespace Deploy.LaunchPad.Core.Auditing
{
    public partial interface IAuditInfo
    {
        string BrowserInfo { get; set; }
        string ClientIpAddress { get; set; }
        string ClientName { get; set; }
        string CustomData { get; set; }
        Exception Exception { get; set; }
        int ExecutionDuration { get; set; }
        DateTime ExecutionTime { get; set; }
        Guid? ImpersonatorTenantId { get; set; }
        Guid? ImpersonatorUserId { get; set; }
        string MethodName { get; set; }
        string Parameters { get; set; }
        string ReturnValue { get; set; }
        string ServiceName { get; set; }
        Guid? TenantId { get; set; }
        Guid? UserId { get; set; }

        string ToString();
    }
}