using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Dynamic;

namespace HouseWare.Base
{
    public static class TypeConverterFactory
    {
        private static readonly ConcurrentDictionary<Type, ITypeConverter> _typeConverters = new ConcurrentDictionary<Type, ITypeConverter>();

        static TypeConverterFactory()
        {
            CreateDefaultConverters();
        }

        private static void CreateDefaultConverters()
        {
            _typeConverters.TryAdd(typeof(DateTime), new DateTimeConverter());
            _typeConverters.TryAdd(typeof(TimeSpan), new TimeSpanConverter());
            _typeConverters.TryAdd(typeof(bool), new BooleanConverter(
                new[] { "yes", "y", "on", "wahr" },
                new[] { "no", "n", "off", "falsch" }));

            var converter = new DictionaryTypeConverter<IDictionary<string, object>>();
            _typeConverters.TryAdd(typeof(IDictionary<string, object>), converter);
            _typeConverters.TryAdd(typeof(Dictionary<string, object>), converter);
            _typeConverters.TryAdd(typeof(RouteValueDictionary), new DictionaryTypeConverter<RouteValueDictionary>());
            _typeConverters.TryAdd(typeof(ExpandoObject), new DictionaryTypeConverter<ExpandoObject>());
            _typeConverters.TryAdd(typeof(HybridExpando), new DictionaryTypeConverter<HybridExpando>());

            _typeConverters.TryAdd(typeof(JObject), new JObjectConverter());
        }

        public static ITypeConverter GetConverter<T>()
        {
            return GetConverter(typeof(T));
        }

        public static ITypeConverter GetConverter(object component)
        {
            Guard.NotNull(component, nameof(component));

            return GetConverter(component.GetType());
        }

        public static ITypeConverter GetConverter(Type type)
        {
            Guard.NotNull(type, nameof(type));

            return _typeConverters.GetOrAdd(type, Get);

            ITypeConverter Get(Type t)
            {
                // Nullable types
                if (type.IsNullable(out Type elementType))
                {
                    return new NullableConverter(type, elementType);
                }

                // Sequence types
                if (type.IsSequenceType(out elementType))
                {
                    var converter = (ITypeConverter)Activator.CreateInstance(typeof(EnumerableConverter<>).MakeGenericType(elementType), type);
                    return converter;
                }

                // Default fallback
                return new DefaultTypeConverter(type);
            }
        }
    }
}