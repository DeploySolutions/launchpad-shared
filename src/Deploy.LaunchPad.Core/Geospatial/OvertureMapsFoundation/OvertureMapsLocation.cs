using Deploy.LaunchPad.Core.Geospatial.Overture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.OvertureMapsFoundation
{
    [Serializable]
    public partial class OvertureMapsLocation : IMayHaveOvertureMapsLocation
    {
        public virtual Guid? GERSId { get; set; }
        public virtual string Features { get; set; }

        public OvertureMapsLocation()
        {
            GERSId = Guid.NewGuid();
            Features = string.Empty;
        }

        public OvertureMapsLocation(Guid gersId)
        {
            GERSId = gersId;
            Features = string.Empty;
        }

        public OvertureMapsLocation(Guid gersId, string features)
        {
            GERSId = gersId;
            Features = features;
        }
    }
}
