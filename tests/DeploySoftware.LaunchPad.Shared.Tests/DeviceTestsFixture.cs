using DeploySoftware.LaunchPad.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Shared.Tests
{
    public class DeviceTestsFixture : IDisposable
    {
        public Device<int> SUT { get; set; }

        public DeviceTestsFixture()
        {
        }

        public void Initialize(Device<int> device)
        {
            SUT = device;
        }

        public void Dispose()
        {

        }
    }
}
