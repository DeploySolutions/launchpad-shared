using Castle.Core.Logging;
using Deploy.LaunchPad.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Helpers
{
    public partial class RandomGenerationHelper : HelperBase
    {
        private static readonly Random Rnd = new Random();

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
        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue; 
        /// that is, the range of return values includes minValue but not maxValue. 
        /// If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than maxValue; 
        /// that is, the range of return values ordinarily includes zero but not maxValue. 
        /// However, if maxValue equals zero, maxValue is returned.
        /// </returns>
        public static int GetRandom(int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(maxValue);
            }
        }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="int.MaxValue"/>.</returns>
        public static int GetRandom()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }

        /// <summary>
        /// Gets random of given objects.
        /// </summary>
        /// <typeparam name="T">Type of the objects</typeparam>
        /// <param name="objs">List of object to select a random one</param>
        public static T GetRandomOf<T>(params T[] objs)
        {
            if (objs.IsNullOrEmpty())
            {
                throw new ArgumentException("objs can not be null or empty!", "objs");
            }

            return objs[GetRandom(0, objs.Length)];
        }

        /// <summary>
        /// Generates a randomized list from given enumerable.
        /// </summary>
        /// <typeparam name="T">Type of items in the list</typeparam>
        /// <param name="items">items</param>
        public static List<T> GenerateRandomizedList<T>(IEnumerable<T> items)
        {
            var currentList = new List<T>(items);
            var randomList = new List<T>();

            while (currentList.Any())
            {
                var randomIndex = GetRandom(0, currentList.Count);
                randomList.Add(currentList[randomIndex]);
                currentList.RemoveAt(randomIndex);
            }

            return randomList;
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
