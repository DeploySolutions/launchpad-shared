using DeploySoftware.LaunchPad.FileGeneration.Stages;
using System;

namespace DeploySoftware.LaunchPad.Core.Tests
{
    public class TokenTestsFixture : IDisposable
    {
        public LaunchPadToken SUT { get; set; }

        public TokenTestsFixture()
        {
        }

        public void Initialize(LaunchPadToken token)
        {
            SUT = token;
        }

        public void Dispose()
        {

        }
    }
}
