using System.ComponentModel;

namespace Deploy.LaunchPad.FileGeneration.Structure
{

    public enum AbpFrameworkEnum
    {
        [Description("Classic free version of AspNetBoilerplate")]
        Abp = 0,
        [Description("Commercial version of AspNetBoilerplate")]
        Abp_AspNetZero = 1,
        [Description("Volo.Abp aka ABP Community")]
        Volo_ABP_Community = 2,
        [Description("Volo.Abp aka ABP Commercial")]
        Volo_ABP_Commercial = 3
    }
}
