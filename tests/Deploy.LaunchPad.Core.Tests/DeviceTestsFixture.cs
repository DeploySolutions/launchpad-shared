﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DeviceTestsFixture.cs" company="Deploy.LaunchPad.Core.Tests">
//     Copyright (c) Deploy Software Solutions, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Domain;
using Deploy.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Tests
{
    /// <summary>
    /// Class DeviceTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class DeviceTestsFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the sut.
        /// </summary>
        /// <value>The sut.</value>
        public Device<int> SUT { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTestsFixture"/> class.
        /// </summary>
        public DeviceTestsFixture()
        {
        }

        /// <summary>
        /// Initializes the specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        public void Initialize(Device<int> device)
        {
            SUT = device;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
