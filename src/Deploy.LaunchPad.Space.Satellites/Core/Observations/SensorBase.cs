using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    [Serializable]
    public abstract partial class SensorBase : ISensor
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Abbreviation { get; set; }

        [Required]
        public virtual IDictionary<string, EarthObservationImageryType> SupportedImageryTypes { get; set; }

        protected SensorBase()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            SupportedImageryTypes = new Dictionary<string, EarthObservationImageryType>(comparer);
        }
    }
}
