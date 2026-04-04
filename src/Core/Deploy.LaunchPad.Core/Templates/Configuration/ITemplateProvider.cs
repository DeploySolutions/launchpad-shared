// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="ITemplateProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Secrets.Configuration;
using Deploy.LaunchPad.Core.Secrets.Resolver;
using Deploy.LaunchPad.Files.Templates;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Templates.Configuration
{
    /// <summary>
    /// Interface ITemplateProvider
    /// </summary>
    public partial interface ITemplateProvider :  ITransientDependency, ILaunchPadService
    {

        public ISecretReferenceResolver SecretReferenceResolver { get; set; }

        /// <summary>
        /// Gets/sets the name of the default database connection string used by ORM module.
        /// It must be the key of a Connection defined in the Connections dictionary of this configuration object.
        /// </summary>
        public IDictionary<string, ILaunchPadTemplate> Templates { get; }
            
        public void AddTemplate(ILaunchPadTemplate templateDefinition);
        public void RemoveTemplate(string templateDefinitionName);

        public List<ILaunchPadTemplate> LoadTemplatesFromConfiguration(
            ISecretProvider secretProvider,
            string templatesJson
        );

        public List<ILaunchPadTemplate> LoadTemplatesFromConfiguration(
            ISecretConfiguration secretConfiguration,
            string templatesJson
        );


    }
}
