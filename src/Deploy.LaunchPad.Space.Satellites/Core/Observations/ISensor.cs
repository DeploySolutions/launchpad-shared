using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    public partial interface ISensor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }

        [Required]
        public IDictionary<string, EarthObservationImageryType> SupportedImageryTypes { get; set; }
    }
}
