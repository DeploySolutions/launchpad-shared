// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="TokenTestsFixture.cs" company="Deploy.LaunchPad.Core.Tests">
//     Copyright (c) Deploy Software Solutions, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.Core.Tests
{
    /// <summary>
    /// Class TokenTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class TokenTestsFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the sut.
        /// </summary>
        /// <value>The sut.</value>
        public LaunchPadToken SUT { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenTestsFixture"/> class.
        /// </summary>
        public TokenTestsFixture()
        {
        }

        /// <summary>
        /// Initializes the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void Initialize(LaunchPadToken token)
        {
            SUT = token;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
