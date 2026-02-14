using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Geospatial.OvertureMapsFoundation
{
    [Serializable]
    public partial class OvertureMapsLocation : IOvertureMapsLocation
    {
        public virtual Guid? GERSId { get; set; }
        public virtual string? FeaturesJson { get; set; }

        public OvertureMapsLocation()
        {
            GERSId = Guid.NewGuid();
            FeaturesJson = string.Empty;
        }

        public OvertureMapsLocation(Guid gersId)
        {
            GERSId = gersId;
            FeaturesJson = string.Empty;
        }

        public OvertureMapsLocation(Guid gersId, string features)
        {
            GERSId = gersId;
            FeaturesJson = features;
        }
    }
}
