using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public partial class DictionaryHelper : HelperBase
    {
        public DictionaryHelper() : base()
        {

        }

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
        /// <returns></returns>
        public IDictionary<TKey, TValue> AddToDictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue item, bool shouldOverwriteIfExists = false)
        {
            Guard.Against<ArgumentNullException>(key == null, DeploySoftware_LaunchPad_Core_Resources.Guard_Input_IsNull);
            Guard.Against<ArgumentNullException>(item == null, DeploySoftware_LaunchPad_Core_Resources.Guard_Input_IsNull);
            
            if(!dictionary.ContainsKey(key))
            {
                dictionary.TryAdd(key, item);
                Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Logger_Info_ItemAdded, item));
            }
            else
            {
                if(shouldOverwriteIfExists)                   
                {
                    dictionary[key] = item;
                    Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Logger_Info_ItemAlreadyExistsOverwriting, item));
                }
                else
                {
                    Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Logger_Info_ItemAlreadyExists, item));
                }
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
