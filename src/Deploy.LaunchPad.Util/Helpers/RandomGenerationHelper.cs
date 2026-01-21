using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Helpers
{
    public partial class RandomGenerationHelper : HelperBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomGenerationHelper"/> class.
        /// </summary>
        public RandomGenerationHelper() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomGenerationHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public RandomGenerationHelper(ILogger logger) : base(logger)
        {

        }

        public virtual string GenerateRandomAllowedLetter()
        {
            Random random = new Random();

            // Allowed letters for house numbers
            string allowedLetters = "ABCDEFNSW";

            // Pick a random letter from the allowed set
            return allowedLetters[random.Next(allowedLetters.Length)].ToString();
        }

        public virtual string GenerateRandomDigits(int length)
        {
            Random random = new Random();

            char[] digits = new char[length];
            digits[0] = (char)random.Next('1', '9' + 1); // Ensure the first digit is 1-9
            for (int i = 1; i < length; i++)
            {
                digits[i] = (char)random.Next('0', '9' + 1); // Remaining digits can be 0-9
            }
            return new string(digits);
        }

        public virtual string GenerateRandomLetter()
        {
            Random random = new Random();

            return ((char)random.Next('A', 'Z' + 1)).ToString(); // Single uppercase letter
        }
    }
}
