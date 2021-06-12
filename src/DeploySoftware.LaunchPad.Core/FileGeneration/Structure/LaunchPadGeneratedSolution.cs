﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a solution generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedSolution : LaunchPadGeneratedObjectBase, ILaunchPadGeneratedSolution
    {

        public virtual SolutionInfrastructure Infra { get; set; }

        /// <summary>
        /// Contains configuration information related to this object's solution
        /// </summary>
        public virtual LaunchPadGeneratedSolutionSettings Settings { get; set; }

        /// <summary>
        /// Returns a bool indicating if the solution is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the solution is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public virtual bool CheckValidity()
        {
            bool isValid = false;
            if (Settings != null 
                && !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Id) && !String.IsNullOrEmpty(ObjectTypeName)                 
            )
            {
                isValid = true;
            }
            return isValid;
        }

        public LaunchPadGeneratedSolution() : base()
        {
            Infra = new SolutionInfrastructure(); 
            Settings = new LaunchPadGeneratedSolutionSettings();
        }
    }
}
