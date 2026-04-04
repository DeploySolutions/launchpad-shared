using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Util.Tokens
{
    public partial class TokenHelper : HelperBase
    {
        /// <summary>
        /// Escapes RegEx characters from provided text to ensure the resulting Regex pattern is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public virtual string EscapeTextForRegex(string value)
        {
            IList<string> escapeCharacters = new List<string>();
            escapeCharacters.Add("(");
            escapeCharacters.Add(")");
            escapeCharacters.Add("[");
            escapeCharacters.Add("]");
            escapeCharacters.Add("/");
            escapeCharacters.Add("{");
            escapeCharacters.Add("}");
            escapeCharacters.Add("*");
            escapeCharacters.Add("+");
            escapeCharacters.Add("?");
            escapeCharacters.Add(".");
            escapeCharacters.Add("$");

            StringBuilder sb = new StringBuilder(value);
            foreach (string unwanted in escapeCharacters)
            {
                sb.Replace(unwanted, '\\' + unwanted);
            }
            return sb.ToString();
        }

    }
}
