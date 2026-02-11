// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DtoTestsFixture.cs" company="Deploy.LaunchPad.Core.Tests">
//     Copyright (c) Deploy Software Solutions, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp;
using Deploy.LaunchPad.Core.Abp.Devices;
using Deploy.LaunchPad.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Domain.Tests
{
    /// <summary>
    /// Class DtoTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class DtoTestsFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the sut.
        /// </summary>
        /// <value>The sut.</value>
        public Device<int> SUT { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DtoTestsFixture"/> class.
        /// </summary>
        public DtoTestsFixture()
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
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
