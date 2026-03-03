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
using System;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Base service interface for LaunchPad
    /// </summary>
    public abstract partial class LaunchPadServiceBase : ILaunchPadService
    {
        public virtual ElementNameLight Name { get; set; }
        public virtual ElementDescriptionLight Description { get; set; }


        protected LaunchPadServiceBase()
        {
            string id = Guid.NewGuid().ToString();
            string name = GetType().Name;
            Name = new ElementNameLight(string.Format("{0} {1} ", name, id));
            Description = new ElementDescriptionLight(string.Format("{0} {1} ", name, id));
        }

        protected LaunchPadServiceBase(string name)
        {
            Name = new ElementNameLight(name);
            Description = new ElementDescriptionLight(name);
        }


        protected LaunchPadServiceBase(string name, string description)
        {
            Name = new ElementNameLight(name);
            Description = new ElementDescriptionLight(description);
        }

    }
}
