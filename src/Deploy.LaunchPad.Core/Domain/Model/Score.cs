using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// This is a score for a particular item
    /// </summary>
    [Serializable]
    public partial class Score
    {
        [JsonProperty("total")]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual decimal Total { get; set; } = 0.0M;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual decimal? Average { get; set; } = 0.0M;

        [NotMapped]
        [JsonIgnore]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual decimal? Mean
        {
            get
            {
                // Handle cases where both are null
                if (Highest == null && Lowest == null)
                    return null;

                // Convert nulls to zero
                decimal v1 = Highest ?? 0;
                decimal v2 = Lowest ?? 0;

                // Compute the mean without throwing errors
                return (v1 + v2) / 2;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual decimal? Lowest { get; set; } = 0.0M;


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual decimal? Highest { get; set; } = 0.0M;


        public Score()
        {
        }


        public Score(decimal total)
        {
            Total = total;
            Highest = total;
            Lowest = total;
            Average = total;
        }

        public Score(decimal total, decimal? highest, decimal? lowest)
        {
            Total = total;

            // Handle cases where highest or lowest are null
            if (highest == null)
            {
                Highest = total;
            }
            else
            {
                Highest = highest;
            }
            if (lowest == null)
            {
                Lowest = total;
            }
            else
            {
                Lowest = lowest;
            }
            // Compute the average without throwing errors
            Average = Highest / Lowest;
        }

    }
}
