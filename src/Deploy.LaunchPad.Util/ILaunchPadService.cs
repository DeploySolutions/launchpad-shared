// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="ILaunchPadService.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Castle.Core.Logging;
using Deploy.LaunchPad.Util.Dependency;
using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Base service interface for LaunchPad
    /// </summary>
    public partial interface ILaunchPadService : ITransientDependency
    {
        public ElementNameLight Name { get; set; }
        public ElementDescriptionLight Description { get; set; }



    }
}
