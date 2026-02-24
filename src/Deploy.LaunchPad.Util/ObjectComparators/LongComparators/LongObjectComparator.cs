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

namespace Deploy.LaunchPad.Util.ObjectComparators.LongComparators
{

    public partial class LongObjectComparator : ObjectComparatorBase<long, LongCompareTypes>
    {
        protected override bool Compare(long baseObject, long compareObject, LongCompareTypes compareType)
        {
            switch (compareType)
            {
                case LongCompareTypes.Equals:
                    return baseObject == compareObject;
                case LongCompareTypes.LessThan:
                    return baseObject < compareObject;
                case LongCompareTypes.LessOrEqualThan:
                    return baseObject <= compareObject;
                case LongCompareTypes.BiggerThan:
                    return baseObject > compareObject;
                case LongCompareTypes.BiggerOrEqualThan:
                    return baseObject >= compareObject;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }
    }

    public partial class NullableLongObjectComparator : ObjectComparatorBase<long?, NullableLongCompareTypes>
    {
        protected override bool Compare(long? baseObject, long? compareObject, NullableLongCompareTypes compareType)
        {
            switch (compareType)
            {
                case NullableLongCompareTypes.Equals:
                    return baseObject == compareObject;
                case NullableLongCompareTypes.LessThan:
                    return baseObject < compareObject;
                case NullableLongCompareTypes.LessOrEqualThan:
                    return baseObject <= compareObject;
                case NullableLongCompareTypes.BiggerThan:
                    return baseObject > compareObject;
                case NullableLongCompareTypes.BiggerOrEqualThan:
                    return baseObject >= compareObject;
                case NullableLongCompareTypes.Null:
                    return !baseObject.HasValue;
                case NullableLongCompareTypes.NotNull:
                    return baseObject.HasValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }
    }
}
