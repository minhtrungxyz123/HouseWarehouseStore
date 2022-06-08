﻿using System.Globalization;
using System.Text;

namespace HouseWare.Base
{
    public static class ConversionExtensions
    {
        public static T Convert<T>(this object value)
        {
            return (T)(Convert(value, typeof(T)) ?? default(T));
        }

        public static T Convert<T>(this object value, T defaultValue)
        {
            return (T)(Convert(value, typeof(T)) ?? defaultValue);
        }

        public static T Convert<T>(this object value, CultureInfo culture)
        {
            return (T)(Convert(value, typeof(T), culture) ?? default(T));
        }

        public static T Convert<T>(this object value, T defaultValue, CultureInfo culture)
        {
            return (T)(Convert(value, typeof(T), culture) ?? defaultValue);
        }

        public static object Convert(this object value, Type to)
        {
            return value.Convert(to, CultureInfo.InvariantCulture);
        }

        public static object Convert(this object value, Type to, CultureInfo culture)
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            if (value == null || value == DBNull.Value || to.IsInstanceOfType(value))
            {
                return value == DBNull.Value ? null : value;
            }

            Type from = value.GetType();

            if (culture == null)
            {
                culture = CultureInfo.InvariantCulture;
            }

            // get a converter for 'to' (value -> to)
            var converter = TypeConverterFactory.GetConverter(to);
            if (converter != null && converter.CanConvertFrom(from))
            {
                return converter.ConvertFrom(culture, value);
            }

            // try the other way round with a 'from' converter (to <- from)
            converter = TypeConverterFactory.GetConverter(from);
            if (converter != null && converter.CanConvertTo(to))
            {
                return converter.ConvertTo(culture, null, value, to);
            }

            // use Convert.ChangeType if both types are IConvertible
            if (value is IConvertible && typeof(IConvertible).IsAssignableFrom(to))
            {
                return System.Convert.ChangeType(value, to, culture);
            }

            throw Error.InvalidCast(from, to);
        }
    }
}