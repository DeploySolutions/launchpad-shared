using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Domain.Model
{
    public partial interface IScore
    {
        public decimal? Average { get; set; }
        public ElementDescriptionLight Description { get; set; }
        public ElementDescriptionLight Explanation { get; set; }
        public decimal? Highest { get; set; }
        public decimal? Lowest { get; set; }
        public decimal? Mean { get; }
        public ElementNameLight Name { get; set; }
        public decimal Total { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}