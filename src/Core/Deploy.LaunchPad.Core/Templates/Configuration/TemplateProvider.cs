// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="TemplateProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using Deploy.LaunchPad.Core.Secrets.Reference;
using Deploy.LaunchPad.Core.Secrets.Resolver;
using Deploy.LaunchPad.Files.Templates;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core.Templates.Configuration
{
    /// <summary>
    /// Class SecretProviderBase.
    /// Implements the <see cref="Core.Config.TemplateProvider" />
    /// </summary>
    /// <seealso cref="Core.Config.TemplateProvider" />
    public partial class TemplateProvider : LaunchPadServiceBase, ITemplateProvider, ITransientDependency
    {

        protected readonly ISecretProvider _secretProvider;

        protected readonly IDictionary<string, ILaunchPadTemplate> _templates = new Dictionary<string, ILaunchPadTemplate>();
        public virtual IDictionary<string, ILaunchPadTemplate> Templates => _templates;

        public virtual ISecretReferenceResolver SecretReferenceResolver { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateProvider"/> class.
        /// </summary>
        public TemplateProvider(ISecretProvider secretProvider)
        {
            _secretProvider = secretProvider;
            SecretReferenceResolver = new SecretReferenceResolver();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateProvider"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public TemplateProvider(ILogger logger, ISecretProvider secretProvider)
        {
            if (logger != null)
            {
                Logger = logger;
            }
            _secretProvider = secretProvider;
        }

        public virtual void AddTemplate(ILaunchPadTemplate template)
        {
            _templates.TryAdd(template.Name, template);
        }

        public virtual void RemoveTemplate(string templateName)
        {
            _templates.Remove(templateName);
        }


        public virtual List<ILaunchPadTemplate> LoadTemplatesFromConfiguration(
            ISecretConfiguration secretConfiguration,
            string templatesJson
        )
        {
            return LoadTemplatesFromConfiguration(secretConfiguration.Provider, templatesJson);
        }

        public virtual List<ILaunchPadTemplate> LoadTemplatesFromConfiguration(
            ISecretProvider secretProvider, 
            string templatesJson
        )
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                Converters = new List<JsonConverter> { new SecretFieldReferenceConverter() }
            };

            var templatesList = JsonConvert.DeserializeObject<List<ILaunchPadTemplate>>(templatesJson, settings);
            foreach (var template in templatesList)
            {
                // load and process the template
                _templates.TryAdd(template.Name, template);
            } 
            return templatesList;
        }

    }
}
