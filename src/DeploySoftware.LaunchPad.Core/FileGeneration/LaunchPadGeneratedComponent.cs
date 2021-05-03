﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a component generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedComponent<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        : LaunchPadGeneratedObjectBase, ILaunchPadGeneratedComponent<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructions, new()
    {
        public virtual LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings,TBlueprintDefinitionInstructions> BlueprintDefinition { get; set; }

        public virtual ILogger Logger { get; set; }

        /// <summary>
        /// Returns a bool indicating if the component is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the component is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public virtual bool CheckValidity()
        {
            bool isValid = false;
            if (BlueprintDefinition != null
                && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(ObjectTypeName)
            )
            {
                isValid = true;
            }
            return isValid;
        }

        public virtual TAssembleOutput AssembleComponent<TAssembleInput, TAssembleOutput, TGeneratedObject>(TAssembleInput input)
            where TAssembleInput : AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>, new()
            where TAssembleOutput : AssembleComponentOutputBase, new()
            where TGeneratedObject : LaunchPadGeneratedSolution, new()
        {
            throw new NotImplementedException();
        }

        public LaunchPadGeneratedComponent(ILogger logger) : base()
        {
            Logger = logger;
            BlueprintDefinition = new LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>();
        }
    }
}
