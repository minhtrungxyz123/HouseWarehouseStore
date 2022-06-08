namespace HouseWare.Base
{
    public static class DateTimeExtensions
    {
        public static readonly DateTime BeginOfEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTime(this long unixTime)
        {
            return BeginOfEpoch.AddSeconds(unixTime);
        }
        public static long ToUnixTime(this DateTime value)
        {
            return Convert.ToInt64((value.ToUniversalTime() - BeginOfEpoch).TotalSeconds);
        }
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> instance, IDictionary<TKey, TValue> from, bool replaceExisting = true)
        {
            foreach (var kvp in from)
            {
                if (replaceExisting || !instance.ContainsKey(kvp.Key))
                {
                    instance[kvp.Key] = kvp.Value;
                }
            }

            return instance;
        }
    }
}