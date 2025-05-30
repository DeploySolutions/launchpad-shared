﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="ILaunchPadSystemIntegrationService.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Core.Services
{
    /// <summary>
    /// Marker interface for integrating LaunchPad with some external service
    /// </summary>
    public partial interface ILaunchPadSystemIntegrationService : ILaunchPadService
    {

    }
}
