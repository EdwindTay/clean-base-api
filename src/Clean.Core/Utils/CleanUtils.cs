using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Clean.Core.Exceptions;

namespace Clean.Core.Utils
{
    public static class CleanUtils
    {
        #region throw if null/empty/default/whitespace

        /// <summary>
        /// Throw friendly exception if it is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static T ThrowIfNull<T>(this T input, string exceptionMessage)
        {
            if (input == null)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw not found exception if it is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static T ThrowNotFoundIfNull<T>(this T input, string exceptionMessage)
        {
            if (input == null)
            {
                throw new NotFoundException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw friendly exception if it is null or default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static T ThrowIfNullOrDefault<T>(this T input, string exceptionMessage)
        {
            //if (input == null || input == default)
            if (input == null)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw friendly exception if it is null or white space
        /// </summary>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static string ThrowIfNullOrWhiteSpace(this string input, string exceptionMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw friendly exception if it is null or empty list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static ICollection<T> ThrowIfNullOrEmptyList<T>(this ICollection<T> input, string exceptionMessage)
        {
            if (input == null || !input.Any())
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw friendly exception if it is null or empty list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static IEnumerable<T> ThrowIfNullOrEmptyList<T>(this IEnumerable<T> input, string exceptionMessage)
        {
            if (input == null || !input.Any())
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        #endregion

        #region throw if not equal

        /// <summary>
        /// Throw error if int value is not equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static int ThrowIfNotEqual(this int input, int compareValue, string exceptionMessage)
        {
            if (input != compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw error if long value is not equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static long ThrowIfNotEqual(this long input, long compareValue, string exceptionMessage)
        {
            if (input != compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw error if string value is not equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static string ThrowIfNotEqual(this string input, string compareValue, string exceptionMessage)
        {
            if (input != compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw error if bool value is not equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static bool ThrowIfNotEqual(this bool input, bool compareValue, string exceptionMessage)
        {
            if (input != compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        #endregion

        #region throw if equal

        /// <summary>
        /// Throw error if int value is equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static int ThrowIfEqual(this int input, int compareValue, string exceptionMessage)
        {
            if (input == compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw error if long value is equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static long ThrowIfEqual(this long input, long compareValue, string exceptionMessage)
        {
            if (input == compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw error if string value is equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static string ThrowIfEqual(this string input, string compareValue, string exceptionMessage)
        {
            if (input == compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Throw error if bool value is equal to comparison value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compareValue"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static bool ThrowIfEqual(this bool input, bool compareValue, string exceptionMessage)
        {
            if (input == compareValue)
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        #endregion

        /// <summary>
        /// Throw error if string value is not in comparison values
        /// </summary>
        /// <param name="input"></param>
        /// <param name="exceptionMessage"></param>
        /// <param name="compareValues"></param>
        /// <returns></returns>
        public static string ThrowIfNotIn(this string input, string exceptionMessage, params string[] compareValues)
        {
            if (!compareValues.Contains(input))
            {
                throw new FriendlyException(exceptionMessage);
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Convert datetime to utc time then to specific country time 
        /// </summary>
        /// <param name="utcDate"></param>
        /// <returns></returns>
        public static DateTime ToCountryLocalDateTime(this DateTime date)
        {
            return date.ToUniversalTime().AddHours(8); //malaysia local time
        }

        /// <summary>
        /// Manually validate data annotations in a class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        public static void ValidateDataAnnotations<T>(this T input)
        {
            var ctx = new ValidationContext(input);

            // A list to hold the validation result.
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(input, ctx, results, true))
            {
                throw new FriendlyException(string.Join(", ", results));
            }
        }
    }
}
