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
using Deploy.LaunchPad.Core.Data;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// This is a score for a particular item
    /// </summary>
    [Serializable]
    public partial class Score : IMustHaveUnitOfMeasure, IScore
    {
        /// <summary>
        /// Provides a friendly label for a particular score, useful for display purposes
        /// such as in pills or when displaying a score in a table as "Very Good" or "Very Poor" 
        /// rather than a numeric data.
        /// </summary>
        [JsonProperty("name")]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementNameLight Name { get; set; } = new ElementNameLight();

        /// <summary>
        /// Provides a description of the purpose of a particular score, useful for display purposes
        /// such as describing the algorithms, calculation process, or purpose of the scoring, in on-hover/alt text, or when displaying a score in a table as "Very Good" or "Very Poor" 
        /// rather than a numeric data.
        /// </summary>
        [JsonProperty("description")]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementDescriptionLight Description { get; set; }

        /// <summary>
        /// Provides a explanation for why the particular score was the result.
        /// </summary>
        [JsonProperty("explanation")]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementDescriptionLight Explanation { get; set; }

        [JsonProperty("total")]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual decimal Total { get; set; } = 0.0M;

        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(50, ErrorMessageResourceName = "Validation_Name_Short_50CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        public virtual string UnitOfMeasure { get; set; } = string.Empty;

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
            Description = new ElementDescription(string.Empty);
        }

        public Score(ElementNameLight name, ElementDescriptionLight description)
        {
            Name = name;
            Description = description;
        }

        public Score(ElementNameLight name, ElementDescriptionLight description, string unitOfMeasure)
        {
            Name = name;
            Description = description;
            UnitOfMeasure = unitOfMeasure;
        }

        public Score(string unitOfMeasure, decimal total)
        {
            Description = new ElementDescriptionLight(string.Empty);
            UnitOfMeasure = unitOfMeasure;
            Total = total;
            Highest = total;
            Lowest = total;
            Average = total;
        }

        public Score(ElementNameLight name, ElementDescriptionLight description, string unitOfMeasure, decimal total)
        {
            Name = name;
            Description = description;
            UnitOfMeasure = unitOfMeasure;
            Total = total;
            Highest = total;
            Lowest = total;
            Average = total;
        }
        public Score(ElementNameLight name, ElementDescriptionLight description, string unitOfMeasure,
            decimal total, decimal? highest, decimal? lowest)
        {
            Name = name;
            Description = description;
            UnitOfMeasure = unitOfMeasure;
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

        public Score(string unitOfMeasure, decimal total, decimal? highest, decimal? lowest)
        {
            Description = new ElementDescriptionLight(string.Empty);
            UnitOfMeasure = unitOfMeasure;
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
