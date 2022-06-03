namespace FileApi.FormFile
{
    public class CommonHelper
    {
        public static string MapPath(string path)
        {
            var root = GetApplicationRoot();

            return Combine(root, path);
        }

        public static string Combine(string root, string path)
        {
            var separator = Path.DirectorySeparatorChar;
            var result = string.Empty;
            // Windows
            if (separator == '\\')
            {
                path = path.Replace("~/", "").TrimStart('/').TrimStart('\\').Replace('/', '\\');
            }
            // Linux
            else if (separator == '/')
            {
                path = path.Replace("~/", "").TrimStart('/').TrimStart('\\').Replace('\\', '/');
            }

            result = Path.Combine(root, path);

            return result;
        }

        public static string GetApplicationRoot()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}