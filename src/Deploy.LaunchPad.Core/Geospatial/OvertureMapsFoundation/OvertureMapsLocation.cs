using Deploy.LaunchPad.Core.Geospatial.Overture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.Overture
{
    [Serializable]
    public partial class OvertureMapsLocation : IOvertureMapsLocation
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
