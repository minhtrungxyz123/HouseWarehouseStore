using Microsoft.Extensions.Configuration;
using System.Collections;

namespace HouseWare.Base
{
    public partial class CommonHelper
    {
        public static IConfiguration Configuration { get; set; }

        public static T GetAppSetting<T>(string key)
        {
            Guard.NotEmpty(key, nameof(key));

            var setting = Configuration.GetValue<T>(key);

            return setting.Convert<T>();
        }

        public static T GetAppSetting<T>(string key, T defValue)
        {
            Guard.NotEmpty(key, nameof(key));

            var setting = Configuration.GetValue<T>(key);

            if (setting == null)
            {
                return defValue;
            }

            return setting.Convert<T>();
        }

        public static IDictionary<string, object> ObjectToDictionary(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return FastProperty.ObjectToDictionary(
                obj,
                key => key.Replace("_", "-").Replace("@", ""));
        }

        public static bool IsTruthy(object value)
        {
            if (value == null)
                return false;

            switch (value)
            {
                case string x:
                    return x.HasValue();

                case bool x:
                    return x == true;

                case DateTime x:
                    return x > DateTime.MinValue;

                case TimeSpan x:
                    return x > TimeSpan.MinValue;

                case Guid x:
                    return x != Guid.Empty;

                case IComparable x:
                    return x.CompareTo(0) != 0;

                case IEnumerable<object> x:
                    return x.Any();

                case IEnumerable x:
                    return x.GetEnumerator().MoveNext();
            }

            if (value.GetType().IsNullable(out var wrappedType))
            {
                return IsTruthy(Convert.ChangeType(value, wrappedType));
            }

            return true;
        }
    }
}