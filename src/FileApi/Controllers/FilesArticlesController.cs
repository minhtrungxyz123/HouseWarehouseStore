﻿using FileApi.FormFile;
using Files.Service;
using HouseWarehouseStore.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FileApi.Controllers
{
    [Route("files-articles")]
    [ApiController]
    public class FilesArticlesController : ControllerBase
    {
        #region Fields

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFilesArticleService _filesArticleService;

        #endregion Fields

        #region Ctor

        public FilesArticlesController(
            IFilesArticleService filesArticleService,
            IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
            _filesArticleService = filesArticleService;
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

            var check = await _filesArticleService.GetByIdAsync(subidFile);

            if (check is null || string.IsNullOrEmpty(check.FileName) || string.IsNullOrEmpty(check.Path))

                return BadRequest(new ApiBadRequestResponse("Không có hình ảnh được chọn"));

            var fileName = check.FileName;
            //Build the File Path.
            string path = FormFile.CommonHelper.MapPath(check.Path + "/" + check.FileName);

            //Read the File data into Byte Array.
            byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        #endregion Download File

        #region Upload File

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _filesArticleService.Delete(id);
            return Ok(result);
        }

        [Route("create-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateImage([FromForm] List<IFormFile> filesadd, string articlesId, int width = 100, int height = 100)
        {
            if (filesadd == null || filesadd.Count == 0)

                return BadRequest(new ApiBadRequestResponse("No image selected"));

            var listEntity = new List<HouseWarehouseStore.Data.Entities.File>();

            string createFolderDate = DateTime.Now.ToString("yyyy/MM/dd");
            var path = FormFile.CommonHelper.MapPath(@"/wwwroot/uploads/" + createFolderDate + "");
            CreateFolderExtension.CreateFolder(path);
            if (path == null)
                path = "image";
            var filePaths = new List<string>();
            string sql = "";
            foreach (var formFile in filesadd)
            {
                if (formFile.Length > 0 && formFile.Length <= 250000000)
                {
                    var filePath = FormFile.CommonHelper.MapPath(path);
                    filePaths.Add(filePath);
                    var randomname = DateTime.Now.ToFileTime() + Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(formFile.FileName);
                    var fileNameWithPath = string.Concat(filePath, "\\", randomname);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        var tem = new HouseWarehouseStore.Data.Entities.File();
                        tem.FileName = randomname;
                        tem.Path = @"/uploads/" + createFolderDate + "";
                        tem.Extension = GetExtension(randomname);
                        tem.MimeType = formFile.ContentType;
                        tem.Size = formFile.Length;
                        listEntity.Add(tem);
                    }
                    FormFile.CommonHelper.Resize(fileNameWithPath, width, height);
                }
                else
                {
                    sql = sql + $" The file width name {formFile.FileName}  must be > 0 and <25M ! ";
                }
            }
            var listRes = new List<HouseWarehouseStore.Data.Entities.File>();
            if (listEntity.Count() > 0)
            {
                await _filesArticleService.InsertAsync(listEntity, articlesId);
                foreach (var item in listEntity)
                {
                    var tem = new HouseWarehouseStore.Data.Entities.File();
                    tem.FileName = item.FileName;
                    tem.Path = item.Path;
                    tem.ArticleId = articlesId;
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

        [Route("update-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateImage([FromForm] List<IFormFile> filesadd, string articleId, int width = 100, int height = 100)
        {
            if (filesadd == null || filesadd.Count == 0)

                return BadRequest(new ApiBadRequestResponse("No image selected"));

            var listEntity = new List<HouseWarehouseStore.Data.Entities.File>();

            string createFolderDate = DateTime.Now.ToString("yyyy/MM/dd");
            var path = FormFile.CommonHelper.MapPath(@"/wwwroot/uploads/" + createFolderDate + "");
            CreateFolderExtension.CreateFolder(path);
            if (path == null)
                path = "image";
            var filePaths = new List<string>();
            string sql = "";
            foreach (var formFile in filesadd)
            {
                if (formFile.Length > 0 && formFile.Length <= 250000000)
                {
                    var filePath = FormFile.CommonHelper.MapPath(path);
                    filePaths.Add(filePath);
                    var randomname = DateTime.Now.ToFileTime() + Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(formFile.FileName);
                    var fileNameWithPath = string.Concat(filePath, "\\", randomname);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        var tem = new HouseWarehouseStore.Data.Entities.File();
                        tem.FileName = randomname;
                        tem.Path = @"/uploads/" + createFolderDate + "";
                        tem.Extension = GetExtension(randomname);
                        tem.MimeType = formFile.ContentType;
                        tem.Size = formFile.Length;
                        listEntity.Add(tem);
                    }
                    FormFile.CommonHelper.Resize(fileNameWithPath, width, height);
                }
                else
                {
                    sql = sql + $" The file width name {formFile.FileName}  must be > 0 and <25M ! ";
                }
            }
            var listRes = new List<HouseWarehouseStore.Data.Entities.File>();
            if (listEntity.Count() > 0)
            {
                await _filesArticleService.UpdateAsync(listEntity, articleId);
                foreach (var item in listEntity)
                {
                    var tem = new HouseWarehouseStore.Data.Entities.File();
                    tem.FileName = item.FileName;
                    tem.Path = item.Path;
                    tem.ArticleId = articleId;
                    tem.MimeType = item.MimeType;
                    tem.Size = item.Size;
                    tem.Id = item.Id;
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

        [HttpDelete]
        [Route("delete-files")]
        public async Task<IActionResult> DeleteImage(string articleId)
        {
            var result = false;
            if (articleId != null)
            {
                var check = await _filesArticleService.GetByIdAsync(articleId);

                string filepath = FormFile.CommonHelper.MapPath(@"/wwwroot/" + check.Path + "/" + check.FileName);
                var deleteRes = DeleteImageByPath(filepath);
                return Ok(deleteRes);
            }
            return Ok(false);
        }

        #endregion Upload File

        #region List

        [HttpGet("articles/{take}")]
        public async Task<IActionResult> GetLatestProducts(int take)
        {
            var files = await _filesArticleService.GetFilesArticles(take);
            return Ok(files);
        }

        #endregion List

        #region Utilities

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetFile(string id)
        {
            var check = await _filesArticleService.GetByNameAsync(id);
            var filePath = FormFile.CommonHelper.MapPath(@"/wwwroot/" + check.Path + "/" + check.FileName);
            var fs = System.IO.File.OpenRead(filePath);
            return File(fs, "image/png");
        }

        private static string GetExtension(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Unknown";
            var Cut = name.Split(".");
            return Cut[Cut.Length - 1];
        }

        private bool DeleteImageByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
            }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return System.IO.File.Exists(path) == false;
        }

        #endregion Utilities
    }
}