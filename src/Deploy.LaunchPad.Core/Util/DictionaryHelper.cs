// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 05-29-2023
// ***********************************************************************
// <copyright file="DictionaryHelper.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core.Util
{
    /// <summary>
    /// Enum DictionaryHelperLoggingStrategy
    /// </summary>
    public enum DictionaryHelperLoggingStrategy
    {
        /// <summary>
        /// The log everything
        /// </summary>
        LogEverything = 0,
        /// <summary>
        /// The log only overwrites
        /// </summary>
        LogOnlyOverwrites = 1,
        /// <summary>
        /// The log only additions and overwrites
        /// </summary>
        LogOnlyAdditionsAndOverwrites = 2,
        /// <summary>
        /// The log only when skipping existing items
        /// </summary>
        LogOnlyWhenSkippingExistingItems = 3
    }

    /// <summary>
    /// Class DictionaryHelper.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Util.HelperBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Util.HelperBase" />
    public partial class DictionaryHelper : HelperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryHelper"/> class.
        /// </summary>
        public DictionaryHelper() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public DictionaryHelper(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Adds the item with the given key to the provided dictionary. If item key is already in the dictionary, it will
        /// simply log that fact, but will not raise an error.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="dictionary">The existing dictionary we wish to place the item in.</param>
        /// <param name="key">The key to which the item should be stored in the dictionary.</param>
        /// <param name="item">The value to store.</param>
        /// <param name="shouldOverwriteIfExists">if set to <c>true</c> [should overwrite if exists].</param>
        /// <param name="loggingStrategy">The logging strategy.</param>
        /// <returns>IDictionary&lt;TKey, TValue&gt;.</returns>
        public IDictionary<TKey, TValue> AddToDictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue item, bool shouldOverwriteIfExists = false, DictionaryHelperLoggingStrategy loggingStrategy = DictionaryHelperLoggingStrategy.LogOnlyOverwrites)
        {
            Guard.Against<ArgumentNullException>(key == null, Deploy_LaunchPad_Core_Resources.Guard_Input_IsNull);
            Guard.Against<ArgumentNullException>(item == null, Deploy_LaunchPad_Core_Resources.Guard_Input_IsNull);

            if (!dictionary.ContainsKey(key))
            {
                bool successfullyAdded = dictionary.TryAdd(key, item);
                if (successfullyAdded &&
                    (loggingStrategy == DictionaryHelperLoggingStrategy.LogEverything || loggingStrategy == DictionaryHelperLoggingStrategy.LogOnlyAdditionsAndOverwrites)
                ) 
                {
                    Logger.Debug(string.Format(Deploy_LaunchPad_Core_Resources.Logger_Info_ItemAdded, item));
                }                
            }
            else
            {
                if (shouldOverwriteIfExists)
                {
                    dictionary[key] = item;
                    if(
                        loggingStrategy == DictionaryHelperLoggingStrategy.LogEverything 
                        || loggingStrategy == DictionaryHelperLoggingStrategy.LogOnlyAdditionsAndOverwrites 
                        || loggingStrategy == DictionaryHelperLoggingStrategy.LogOnlyOverwrites
                    )
                    {
                        Logger.Debug(string.Format(Deploy_LaunchPad_Core_Resources.Logger_Info_ItemAlreadyExistsOverwriting, item));
                    }                    
                }
                else
                {
                    // skipping since it exists and we don't want to override. Possibly do a log entry
                    if (
                        loggingStrategy == DictionaryHelperLoggingStrategy.LogEverything
                        || loggingStrategy == DictionaryHelperLoggingStrategy.LogOnlyWhenSkippingExistingItems
                    )
                    {
                        Logger.Debug(string.Format(Deploy_LaunchPad_Core_Resources.Logger_Info_ItemAlreadyExists, item));
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Merges dictionary 2 into dictionary 1 and returns the result
        /// </summary>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="dictionary1">The dictionary1.</param>
        /// <param name="dictionary2">The dictionary2.</param>
        /// <returns>IDictionary&lt;TKey, TValue&gt;.</returns>
        public IDictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2)
        {
            Guard.Against<ArgumentNullException>(dictionary1 == null || dictionary2 == null, Deploy_LaunchPad_Core_Resources.Guard_Input_IsNull);
            var merged = dictionary1.Concat(dictionary2)
                 .GroupBy(i => i.Key)
                 .ToDictionary(
                     group => group.Key,
                     group => group.First().Value);
            return merged;
        }


        /// <summary>
        /// Converts to basetypedictionary.
        /// </summary>
        /// <typeparam name="TBaseType">The type of the t base type.</typeparam>
        /// <typeparam name="TDerivedType">The type of the t derived type.</typeparam>
        /// <param name="derivedTypesDictionary">The derived types dictionary.</param>
        /// <returns>IDictionary&lt;System.String, TBaseType&gt;.</returns>
        public IDictionary<string, TBaseType> ToBaseTypeDictionary<TBaseType, TDerivedType>(IDictionary<string, TDerivedType> derivedTypesDictionary)
            where TDerivedType : TBaseType, new()
        {
            IDictionary<string, TBaseType> baseDictionary = derivedTypesDictionary.ToDictionary(
                k => k.Key,
                v => (TBaseType)v.Value,
                StringComparer.OrdinalIgnoreCase
            );
            return baseDictionary;
        }
    }
}
