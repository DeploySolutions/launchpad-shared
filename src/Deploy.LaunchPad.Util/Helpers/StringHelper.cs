// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="StringHelper.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Class StringHelper.
    /// Implements the <see cref="Deploy.LaunchPad.Util.IHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Util.IHelper" />
    public partial class StringHelper: HelperBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHelper"/> class.
        /// </summary>
        protected StringHelper()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public StringHelper(ILogger logger)
        {
            if (logger != null)
            {
                Logger = logger;
            }
        }
      
        /// <summary>
        /// Helps with converting to "snake case" (lower case words separated by underscores) from a regular string.
        /// Useful for generating csv and database column names from class properties.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The same string with the words converted to lower case and separated by underscores.</returns>
        public virtual string ConvertToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(char.ToLower(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    if (stringBuilder[stringBuilder.Length - 1] != '_')
                    {
                        stringBuilder.Append('_');
                    }
                    stringBuilder.Append(char.ToLower(input[i]));
                }
                else
                {
                    stringBuilder.Append(input[i]);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Helps with converting to "pascal case" (Words with capitalized letters and no separation) from a snake case or similar string.
        /// Useful for generating class property names from csv and database column names.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The same string with the words converted to pascal case.</returns>
        public virtual string ConvertToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var stringBuilder = new StringBuilder();
            bool capitalizeNext = true;

            foreach (var character in input)
            {
                if (character == '_')
                {
                    capitalizeNext = true;
                }
                else
                {
                    if (capitalizeNext)
                    {
                        stringBuilder.Append(char.ToUpper(character));
                        capitalizeNext = false;
                    }
                    else
                    {
                        stringBuilder.Append(character);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a PascalCase or camelCase string into a space-separated string of words.
        /// Example: "PascalCaseString" -> "Pascal Case String"
        /// </summary>
        /// <param name="input">The PascalCase or camelCase string.</param>
        /// <returns>A string with spaces between words.</returns>
        public virtual string SplitFromPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(input[0]);

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && !char.IsWhiteSpace(input[i - 1]))
                {
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append(input[i]);
            }

            return stringBuilder.ToString();
        }

    }
}