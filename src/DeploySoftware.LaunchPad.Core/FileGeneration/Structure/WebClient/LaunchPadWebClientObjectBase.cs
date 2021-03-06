﻿using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The base class containing properties for all LaunchPad web client file generation processes.
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadWebClientObjectBase : LaunchPadGeneratedObjectBase
    {

        public LaunchPadWebClientObjectBase() : base()
        {
        }

    }
}
