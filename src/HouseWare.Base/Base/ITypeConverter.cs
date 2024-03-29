﻿using System.Globalization;

namespace HouseWare.Base
{
    public interface ITypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter.
        /// </summary>
        /// <param name="type">A Type that represents the type you want to convert from. </param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        bool CanConvertFrom(Type type);

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="type">A Type that represents the type you want to convert to. </param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        bool CanConvertTo(Type type);

        /// <summary>
        /// Converts the given value to the type of this converter.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture. If null is passed, the invariant culture is assumed.</param>
        /// <param name="value">The object to convert.</param>
        /// <returns>An object that represents the converted value.</returns>
        object ConvertFrom(CultureInfo culture, object value);

        /// <summary>
        /// Converts the given value object to the specified type, using the arguments.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture. If null is passed, the invariant culture is assumed.</param>
        /// <param name="format">A standard or custom format expression.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="to">The type to convert the value parameter to.</param>
        /// <returns>An Object that represents the converted value.</returns>
        object ConvertTo(CultureInfo culture, string format, object value, Type to);
    }
}