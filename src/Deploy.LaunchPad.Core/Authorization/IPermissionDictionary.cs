using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Authorization
{
    public interface IPermissionDictionary : IDictionary<string, IPermission>
    {
        void AddAllPermissions();
    }
}