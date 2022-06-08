using System.Diagnostics;
using System.Globalization;

namespace HouseWare.Base
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string FormatWith(this string format, params object[] args)
        {
            return FormatWith(format, CultureInfo.CurrentCulture, args);
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        [DebuggerStepThrough]
        public static string NullEmpty(this string value)
        {
            return (string.IsNullOrEmpty(value)) ? null : value;
        }
        [DebuggerStepThrough]
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        [DebuggerStepThrough]
        public static string FormatInvariant(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.InvariantCulture, format, objects);
        }

        [DebuggerStepThrough]
        public static string[] SplitSafe(this string value, string separator)
        {
            if (string.IsNullOrEmpty(value))
                return new string[0];

            // Do not use separator.IsEmpty() here because whitespace like " " is a valid separator.
            // an empty separator "" returns array with value.
            if (separator == null)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var c = value[i];
                    if (c == ';' || c == ',' || c == '|')
                    {
                        return value.Split(new char[] { c }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (c == '\r' && (i + 1) < value.Length & value[i + 1] == '\n')
                    {
                        return value.GetLines(false, true).ToArray();
                    }
                }

                return new string[] { value };
            }
            else
            {
                return value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static IEnumerable<string> GetLines(this string input, bool trimLines = false, bool removeEmptyLines = false)
        {
            if (input.IsEmpty())
            {
                yield break;
            }

            using (var sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (trimLines)
                    {
                        line = line.Trim();
                    }

                    if (removeEmptyLines && IsEmpty(line))
                    {
                        continue;
                    }

                    yield return line;
                }
            }
        }

        [DebuggerStepThrough]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}