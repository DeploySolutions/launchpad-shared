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

namespace Deploy.LaunchPad.Util.ObjectComparators.IntComparators
{
    public class IntObjectComparator : ObjectComparatorBase<int, IntCompareTypes>
    {
        protected override bool Compare(int baseObject, int compareObject, IntCompareTypes compareType)
        {
            switch (compareType)
            {
                case IntCompareTypes.Equals:
                    return baseObject == compareObject;
                case IntCompareTypes.LessThan:
                    return baseObject < compareObject;
                case IntCompareTypes.LessOrEqualThan:
                    return baseObject <= compareObject;
                case IntCompareTypes.BiggerThan:
                    return baseObject > compareObject;
                case IntCompareTypes.BiggerOrEqualThan:
                    return baseObject >= compareObject;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }
    }

    public class NullableIntObjectComparator : ObjectComparatorBase<int?, NullableIntCompareTypes>
    {
        protected override bool Compare(int? baseObject, int? compareObject, NullableIntCompareTypes compareType)
        {
            switch (compareType)
            {
                case NullableIntCompareTypes.Equals:
                    return baseObject == compareObject;
                case NullableIntCompareTypes.LessThan:
                    return baseObject < compareObject;
                case NullableIntCompareTypes.LessOrEqualThan:
                    return baseObject <= compareObject;
                case NullableIntCompareTypes.BiggerThan:
                    return baseObject > compareObject;
                case NullableIntCompareTypes.BiggerOrEqualThan:
                    return baseObject >= compareObject;
                case NullableIntCompareTypes.Null:
                    return !baseObject.HasValue;
                case NullableIntCompareTypes.NotNull:
                    return baseObject.HasValue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }
    }
}
