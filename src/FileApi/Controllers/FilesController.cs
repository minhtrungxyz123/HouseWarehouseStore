using FileApi.FormFile;
using Files.Service;
using HouseWarehouseStore.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FileApi.Controllers
{
    [Route("files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        #region Fields

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFilesService _fileService;

        #endregion Fields

        #region Ctor

        public FilesController(
            IFilesService fileService,
            IWebHostEnvironment hostEnvironment)
        {
            _fileService = fileService;
            _hostingEnvironment = hostEnvironment;
        }

        #endregion Ctor

        #region Download File

        [Route("download-file")]
        [HttpGet]
        public async Task<IActionResult> DownloadAsync([Required] string? subidFile)
        {
            if (subidFile is null)
            {
                throw new ArgumentNullException(nameof(subidFile));
            }

            var check = await _fileService.GetByIdAsync(subidFile);

            if (check is null || string.IsNullOrEmpty(check.FileName) || string.IsNullOrEmpty(check.Path))

                return BadRequest(new ApiBadRequestResponse("Không có hình ảnh được chọn"));

            var fileName = check.FileName;
            //Build the File Path.
            string path = CommonHelper.MapPath(check.Path + "/" + check.FileName);

            //Read the File data into Byte Array.
            byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        #endregion Download File

        #region Upload File

        [Route("create-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateImage([FromForm] List<IFormFile> filesadd)
        {
            if (filesadd == null || filesadd.Count == 0)

                return BadRequest(new ApiBadRequestResponse("No image selected"));

            var listEntity = new List<HouseWarehouseStore.Data.Entities.Files>();

            string createFolderDate = DateTime.Now.ToString("yyyy/MM/dd");
            var path = CommonHelper.MapPath(@"/wwwroot/uploads/" + createFolderDate + "");
            CreateFolderExtension.CreateFolder(path);
            if (path == null)
                path = "image";
            var filePaths = new List<string>();
            string sql = "";
            foreach (var formFile in filesadd)
            {
                //if (!FormFileExtensions.IsImage(formFile) && !FormFileExtensions.IsExcel(formFile) && !FormFileExtensions.IsWord(formFile) && !FormFileExtensions.IsZipRar(formFile))
                //{
                //    if (sql.Length == 0)
                //        sql = $"File width name {formFile.FileName} is not type word, excel, zip or image, rar";
                //    else
                //        sql = sql + $" .File width name {formFile.FileName} is not type word, excel, zip or image, rar";
                //}
                //else
                //{
                if (formFile.Length > 0 && formFile.Length <= 250000000)
                {
                    var filePath = CommonHelper.MapPath(path);
                    filePaths.Add(filePath);
                    var randomname = DateTime.Now.ToFileTime() + Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(formFile.FileName);
                    var fileNameWithPath = string.Concat(filePath, "\\", randomname);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        var tem = new HouseWarehouseStore.Data.Entities.Files();
                        tem.FileName = randomname;
                        tem.Path = @"/wwwroot/uploads/" + createFolderDate + "";
                        tem.Extension = GetExtension(randomname);
                        tem.MimeType = formFile.ContentType;
                        tem.Size = formFile.Length;
                        listEntity.Add(tem);
                    }
                }
                else
                {
                    sql = sql + $" The file width name {formFile.FileName}  must be > 0 and <25M ! ";
                }
                //}
            }
            var listRes = new List<HouseWarehouseStore.Data.Entities.Files>();
            if (listEntity.Count() > 0)
            {
                await _fileService.InsertAsync(listEntity);
                foreach (var item in listEntity)
                {
                    var tem = new HouseWarehouseStore.Data.Entities.Files();
                    tem.FileName = item.FileName;
                    tem.Path = item.Path;
                    listRes.Add(tem);
                }
            }
            return Ok(new
            {
                error = sql,
                result = true,
                res = listRes,
            });
        }

        #endregion Upload File

        #region Utilities

        private static string GetExtension(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Unknown";
            var Cut = name.Split(".");
            return Cut[Cut.Length - 1];
        }

        #endregion Utilities
    }
}