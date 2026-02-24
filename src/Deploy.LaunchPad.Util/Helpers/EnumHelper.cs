using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.Util.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                ?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                .FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        /// <summary>
        /// Gets the description from enum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="shouldReturnOriginalValueIfDescriptionEmpty">if set to <c>true</c> [should return original value if description empty].</param>
        /// <returns>System.String.</returns>
        public static string GetDescriptionFromEnum(Enum value, bool shouldReturnOriginalValueIfDescriptionEmpty = true)
        {
            var descriptionAttribute = value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)
                .FirstOrDefault() as System.ComponentModel.DescriptionAttribute;

            if (descriptionAttribute != null && !string.IsNullOrWhiteSpace(descriptionAttribute.Description))
            {
                return descriptionAttribute.Description;
            }

            return shouldReturnOriginalValueIfDescriptionEmpty ? value.ToString() : string.Empty;

        }

    }
}
