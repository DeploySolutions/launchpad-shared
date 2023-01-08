using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Space.Satellites.Core;

namespace Deploy.LaunchPad.Space.Satellites.Landsat
{
    public partial class Landsat8ObservationScene<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>,
        ILandsatObservationScene<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public string Bucket { get; set; }
        public string ProductId { get; set; }
        public string Collection { get; set; }
        public string Projection { get; set; }
        public string SensorName { get; set; }
        public string YearAcquired { get; set; }
        public string Path { get; set; }
        public string Row { get; set; }


        public Landsat8ObservationScene()
        {

        }

    }
}
