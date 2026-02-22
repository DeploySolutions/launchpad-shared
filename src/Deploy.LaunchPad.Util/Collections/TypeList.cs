#region "Licensing"
/*
 * SPDX-FileCopyrightText: Copyright (c) Volosoft (https://volosoft.com) and contributors
 * SPDX-License-Identifier: MIT
 *
 * This file contains code originally from the ASP.NET Boilerplate - Web Application Framework:
 *   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
 *
 * The original portions of this file remain licensed under the MIT License.
 * You may obtain a copy of the MIT License at:
 *
 *   https://opensource.org/license/mit
 *
 *
 * SPDX-FileCopyrightText: Copyright (c) 2026 Deploy Software Solutions (https://www.deploy.solutions)
 * SPDX-License-Identifier: Apache-2.0
 *
 * Modifications and additional code in this file are licensed under
 * the Apache License, Version 2.0.
 *
 * You may obtain a copy of the Apache License at:
 *
 *   https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the Apache License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the applicable license for governing permissions and limitations.
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Deploy.LaunchPad.Util.Collections
{
    /// <summary>
    /// A shortcut for <see cref="TypeList{TBaseType}"/> to use object as base type.
    /// </summary>
    public class TypeList : TypeList<object>, ITypeList
    {
    }

    /// <summary>
    /// Extends <see cref="List{Type}"/> to add restriction a specific base type.
    /// </summary>
    /// <typeparam name="TBaseType">Base Type of <see cref="Type"/>s in this list</typeparam>
    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get { return _typeList.Count; } }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public Type this[int index]
        {
            get { return _typeList[index]; }
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        private readonly List<Type> _typeList;

        /// <summary>
        /// Creates a new <see cref="TypeList{T}"/> object.
        /// </summary>
        public TypeList()
        {
            _typeList = new List<Type>();
        }

        /// <inheritdoc/>
        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        public bool TryAdd<T>() where T : TBaseType
        {
            if (Contains<T>())
            {
                return false;
            }

            Add<T>();
            return true;
        }

        /// <inheritdoc/>
        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, Type item)
        {
            _typeList.Insert(index, item);
        }

        /// <inheritdoc/>
        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        /// <inheritdoc/>
        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        /// <inheritdoc/>
        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        /// <inheritdoc/>
        public void Remove<T>() where T : TBaseType
        {
            _typeList.Remove(typeof(T));
        }

        /// <inheritdoc/>
        public bool Remove(Type item)
        {
            return _typeList.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _typeList.Clear();
        }

        /// <inheritdoc/>
        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException("Given item is not type of " + typeof(TBaseType).AssemblyQualifiedName, "item");
            }
        }
    }
}