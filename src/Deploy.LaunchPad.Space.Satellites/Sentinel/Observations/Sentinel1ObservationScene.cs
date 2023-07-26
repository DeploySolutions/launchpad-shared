using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Space.Satellites.Core;

namespace Deploy.LaunchPad.Space.Satellites.Sentinel
{
    public partial class Sentinel1ObservationScene : EarthObservationModelBase,
        ISentinel1ObservationScene
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
