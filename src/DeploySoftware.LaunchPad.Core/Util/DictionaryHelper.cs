using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public partial class DictionaryHelper
    {
        public ILogger Logger { get; set; }

        public DictionaryHelper()
        {
            Logger = NullLogger.Instance;
        }

        public DictionaryHelper(ILogger logger)
        {
            Logger = logger;
        }

        public IDictionary<TKey, TValue> AddToDictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue item)
        {
            Guard.Against<ArgumentNullException>(item == null, DeploySoftware_LaunchPad_Core_Resources.Guard_Input_IsNull);
            try
            {
                dictionary.Add(key, item);
                Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Logger_Info_ItemAdded, item));
            }
            catch (ArgumentException)
            {
                Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Logger_Info_ItemAlreadyExists, item));
            }
            return dictionary;
        }

        /// <summary>
        /// Merges dictionary 2 into dictionary 1 and returns the result
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public IDictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2)
        {
            Guard.Against<ArgumentNullException>(dictionary1 == null || dictionary2 == null, DeploySoftware_LaunchPad_Core_Resources.Guard_Input_IsNull);
            var merged = dictionary1.Concat(dictionary2)
                 .GroupBy(i => i.Key)
                 .ToDictionary(
                     group => group.Key,
                     group => group.First().Value);
            return merged;
        }
    

        public IDictionary<string,TBaseType> ToBaseTypeDictionary<TBaseType,TDerivedType>(IDictionary<string, TDerivedType> derivedTypesDictionary)
            where TDerivedType: TBaseType, new()
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
