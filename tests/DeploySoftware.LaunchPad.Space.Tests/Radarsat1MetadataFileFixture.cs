using DeploySoftware.LaunchPad.Shared.Domain;
using DeploySoftware.LaunchPad.Space.Satellites.Canada;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Space.Tests
{
    public class Radarsat1MetadataFileFixture : IDisposable
    {
        public Radarsat1Observation Observation { get; set; }

        public FileKey Radarsat1MetadataFileKey { get; set; }

        public Radarsat1MetadataFileFixture()
        {
        }

        public void Initialize(FileKey _radarsat1MetadataKey)
        {
            Radarsat1MetadataFileKey = _radarsat1MetadataKey;
            Observation = Radarsat1MetadataParser.GetRadarsat1ObservationFromMetadataFile(Radarsat1MetadataFileKey);
        }

        public void Dispose()
        {

        }
    }
}
