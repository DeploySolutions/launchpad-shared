using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Space.Satellites.Core;

namespace Deploy.LaunchPad.Space.Satellites.Sentinel
{
    public partial class Sentinel1ObservationScene<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>,
        ISentinel1ObservationScene<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public string Bucket { get; set; }
        public string ProductId { get; set; }
        public string Path { get; set; }
        public string ProductType { get; set; }
        public string Version { get; set; }

        public Sentinel1ObservationScene()
        {

        }

    }
}
