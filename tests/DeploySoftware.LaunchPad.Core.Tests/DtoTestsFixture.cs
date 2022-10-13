using DeploySoftware.LaunchPad.Core.Abp.Domain;
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Tests
{
    public class DtoTestsFixture : IDisposable
    {
        public Device<int> SUT { get; set; }

        public DtoTestsFixture()
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
