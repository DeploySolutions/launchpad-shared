using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Geography
{
    public partial interface IObservationPoint<TPrimaryKey> : ILaunchPadObject
    {
        public IAreaOfInterest<TPrimaryKey> ParentAoi { get; set; }

        public IGeographicLocation Location { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
