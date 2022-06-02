namespace Master.Service
{
    public class FileService : IFileService
    {
        public string WebRootPath { get; set; }
        private readonly string _folder;
        private const string FOLDER_NAME = "file";

        public FileService()
        {
            _folder = Path.Combine(WebRootPath, FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_folder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_folder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}