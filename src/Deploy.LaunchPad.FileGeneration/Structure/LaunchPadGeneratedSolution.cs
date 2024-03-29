﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-09-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedSolution.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a solution generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedSolution : LaunchPadGeneratedObjectBase, ILaunchPadGeneratedSolution
    {
        /// <summary>
        /// Describes the overall infrastructure this solution is being generated/deployed into. May be null.
        /// </summary>
        /// <value>The software infrastructure.</value>
        public virtual ISoftwareInfrastructure SoftwareInfrastructure { get; set; }

        /// <summary>
        /// Contains configuration information related to this object's solution
        /// </summary>
        /// <value>The settings.</value>
        public virtual LaunchPadGeneratedSolutionSettings Settings { get; set; }

        /// <summary>
        /// Returns a bool indicating if the solution is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the solution is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public virtual bool CheckValidity()
        {
            bool isValid = false;
            if (Settings != null
                && !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Id.ToString()) && !String.IsNullOrEmpty(Inheritance.FullyQualifiedType)
            )
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedSolution"/> class.
        /// </summary>
        public LaunchPadGeneratedSolution() : base()
        {
            Settings = new LaunchPadGeneratedSolutionSettings();
        }
    }
}
