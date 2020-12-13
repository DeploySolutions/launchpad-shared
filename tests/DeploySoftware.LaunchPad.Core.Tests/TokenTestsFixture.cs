﻿using DeploySoftware.LaunchPad.Core.Domain;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

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