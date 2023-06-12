using Deploy.LaunchPad.Core.Util;
using H3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.H3
{
    public partial class H3Helper : HelperBase
    {
        public virtual H3Index CreateH3IndexFromHex(string hex)
        {
            H3Index index = new H3Index(hex);
            return index;

        }

        public virtual H3Index CreateH3IndexFromULong(ulong indexValue)
        {
            H3Index index = new H3Index(indexValue);
            return index;
        }
    }
}
