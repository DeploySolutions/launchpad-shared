using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Authorization
{
    public partial interface IPermissionDictionary : IDictionary<string, IPermission>
    {
        void AddAllPermissions();
    }
}