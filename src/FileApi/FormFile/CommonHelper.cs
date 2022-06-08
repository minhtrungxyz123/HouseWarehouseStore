using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

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

        public static void Resize(string h, int w, int he)
        {
            using (var image = Image.Load(h))
            {
                if (image.Width > w)
                {
                    image.Mutate(x => x.Resize(w, he));
                    image.Save(h);
                }
            }
        }
    }
}