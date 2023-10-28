﻿using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a basic class generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedClassBase : LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// Contains information related to this object's position with a Visual Studio solution
        /// </summary>
        public virtual VisualStudioClassConfiguration VisualStudioConfig { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public virtual string Namespace { get; set; }

        public virtual string ParentClass { get; set; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        public virtual HashSet<string> InheritsFrom { get; set; }

        /// <summary>
        /// The using statements generated in this file.
        /// </summary>
        public virtual HashSet<string> UsingStatements { get; set; }

        /// <summary>
        /// Whether the class is virtual. Defaults to yes to allow partial classes.
        /// </summary>
        public virtual bool IsPartial { get; set; } = true;

        /// <summary>
        /// Whether the class is static. Defaults to false to prefer instance types.
        /// </summary>
        public virtual bool IsStatic { get; set; } = false;

        /// <summary>
        /// Whether the class is abstract. Defaults to false.
        /// </summary>
        public virtual bool IsAbstract { get; set; } = false;

        /// <summary>
        /// The access modifier/level of the class. Defaults to public.
        /// </summary>
        public LaunchPadGeneratedAccessModifier AccessModifier { get; set; }


        /// <summary>
        /// The dictionary of unique base LaunchPad properties that belong to this class (received through DomainEntity or other base class inheritance).
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedProperty> BaseProperties { get; set; }

        /// <summary>
        /// The dictionary of unique custom properties that belong to this class (that were not inherited).
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedProperty> CustomProperties { get; set; }

        public virtual IDictionary<string, LaunchPadGeneratedProperty> Properties => new DictionaryHelper().MergeDictionaries(BaseProperties, CustomProperties);

        /// <summary>
        /// The dictionary of unique base methods that belong to this class 
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedMethod> BaseMethods { get; set; }

        /// <summary>
        /// The dictionary of unique custom methods that belong to this class 
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedMethod> CustomMethods { get; set; }


        public LaunchPadGeneratedClassBase() : base()
        {
            Namespace = string.Empty;
            InheritsFrom = new HashSet<string>();
            UsingStatements = new HashSet<string>();
            var comparer = StringComparer.OrdinalIgnoreCase;
            BaseProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            BaseMethods = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomMethods = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            VisualStudioConfig = new VisualStudioClassConfiguration();
            AccessModifier = LaunchPadGeneratedAccessModifier.Public;
        }
    }
}
