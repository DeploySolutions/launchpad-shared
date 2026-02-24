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
using System.Collections.Immutable;
using Deploy.LaunchPad.Util.Dependency;

namespace Deploy.LaunchPad.Util.ObjectComparators
{
    public abstract class ObjectComparatorBase : IObjectComparator, ITransientDependency
    {
        public abstract Type ObjectType { get; }

        public abstract ImmutableList<string> CompareTypes { get; }

        public abstract bool Compare(object baseObject, object compareObject, string compareType);

        public abstract bool CanCompare(Type baseObjectType, string compareType);
    }

    public abstract class ObjectComparatorBase<TBaseType> : ObjectComparatorBase
    {
        public sealed override Type ObjectType => typeof(TBaseType);

        private readonly bool _isNullable = false;

        protected ObjectComparatorBase()
        {
            _isNullable = IsNullableType(typeof(TBaseType));
        }

        protected abstract bool Compare(TBaseType baseObject, TBaseType compareObject, string compareType);

        public sealed override bool Compare(object baseObject, object compareObject, string compareType)
        {
            if (!_isNullable && (baseObject == null || compareObject == null))
            {
                throw new ArgumentNullException();
            }

            TBaseType baseObjTyped;
            TBaseType compareObjTyped;

            if (baseObject == null)
            {
                baseObjTyped = default;//which is null
            }
            else
            {
                baseObjTyped = (TBaseType)baseObject;
            }

            if (compareObject == null)
            {
                compareObjTyped = default;//which is null
            }
            else
            {
                compareObjTyped = (TBaseType)compareObject;
            }

            return Compare(baseObjTyped, compareObjTyped, compareType);
        }

        protected virtual bool CanCompare(string compareType)
        {
            return CompareTypes.Contains(compareType);
        }

        public sealed override bool CanCompare(Type baseObjectType, string compareType)
        {
            return _isNullable == IsNullableType(baseObjectType) && baseObjectType.IsAssignableFrom(typeof(TBaseType)) && CanCompare(compareType);
        }

        protected static bool IsNullableType(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            return type == typeof(string);
        }
    }

    public abstract class ObjectComparatorBase<TBaseType, TEnumCompareTypes> : ObjectComparatorBase<TBaseType>
        where TEnumCompareTypes : Enum
    {
        public override ImmutableList<string> CompareTypes { get; }

        protected ObjectComparatorBase()
        {
            CompareTypes = Enum.GetNames(typeof(TEnumCompareTypes)).ToImmutableList();
        }

        protected abstract bool Compare(TBaseType baseObject, TBaseType compareObject, TEnumCompareTypes compareType);

        protected sealed override bool Compare(TBaseType baseObject, TBaseType compareObject, string compareType)
        {
            var compareTypeEnum = (TEnumCompareTypes)Enum.Parse(typeof(TEnumCompareTypes), compareType);
            return Compare(baseObject, compareObject, compareTypeEnum);
        }
    }
}
