using System.Diagnostics;

namespace HouseWare.Base
{
    public class Guard
    {
        [DebuggerStepThrough]
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }

        [DebuggerStepThrough]
        public static void NotEmpty(string arg, string argName)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw Error.Argument(argName, "String parameter '{0}' cannot be null or all whitespace.", argName);
        }

        [DebuggerStepThrough]
        public static void NotEmpty<T>(ICollection<T> arg, string argName)
        {
            if (arg == null || !arg.Any())
                throw Error.Argument(argName, "Collection cannot be null and must contain at least one item.");
        }

        [DebuggerStepThrough]
        public static void NotEmpty(Guid arg, string argName)
        {
            if (arg == Guid.Empty)
                throw Error.Argument(argName, "Argument '{0}' cannot be an empty guid.", argName);
        }
    }
}