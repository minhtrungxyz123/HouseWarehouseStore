using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticlesController : Controller
    {
        #region Fields

        private readonly IArticlesApiClient _articlesApiClient;

        public ArticlesController(IArticlesApiClient articlesApiClient)
        {
            _articlesApiClient = articlesApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ArticleSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _articlesApiClient.Get(request);

            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        #endregion List

        #region Method

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ArticleModel();
            model.CreateDate = DateTime.UtcNow.ToLocalTime();
            await GetDropDownList(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.CreateDate = DateTime.UtcNow.ToLocalTime();
            request.Id = Guid.NewGuid().ToString();
            request.Image = request.Id;

            var result = await _articlesApiClient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ArticlesId = request.Id;
                filemodels.filesadd = request.filesadd;
                await _articlesApiClient.CreateImage(filemodels, request.Id);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _articlesApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new ArticleModel()
                {
                    Active = model.Active,
                    Id = id,
                    TitleMeta = model.TitleMeta,
                    Body = model.Body,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Home = model.Home,
                    Hot = model.Hot,
                    AvailableArticleCategories = model.AvailableArticleCategories,
                    DescriptionMetaTitle = model.DescriptionMetaTitle,
                    Image = model.Image,
                    KeyWord = model.KeyWord,
                    Subject = model.Subject,
                    Url = model.Url,
                    View = model.View,
                    FilesModels = await _articlesApiClient.GetFilesArticles(SystemConstants.ArticleSettings.NumberOfArticle),
                };
                
                if (model.AvailableArticleCategories.Count > 0 &&
                !string.IsNullOrEmpty(model.ArticleCategoryId))
                {
                    var item = model.AvailableArticleCategories
                        .FirstOrDefault(x => x.Value.Equals(model.ArticleCategoryId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _articlesApiClient.Edit(request.Id, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ArticlesId = request.Id;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _articlesApiClient.DeleteFiles(request.Id);
                //update files
                await _articlesApiClient.UpdateImage(filemodels, request.Id);

                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _articlesApiClient.Delete(id);

            await _articlesApiClient.DeleteFiles(id);
            await _articlesApiClient.DeleteDataFiles(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var result = await _articlesApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;

                await GetDropDownList(model);

                var updateRequest = new ArticleModel()
                {
                    Active = model.Active,
                    Id = id,
                    Body = model.Body,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Home = model.Home,
                    Hot = model.Hot,
                    TitleMeta = model.TitleMeta,
                    ArticleCategoryId = model.ArticleCategoryId,
                    DescriptionMetaTitle = model.DescriptionMetaTitle,
                    Url = model.Url,
                    Image = model.Image,
                    KeyWord = model.KeyWord,
                    Subject = model.Subject,
                    View = model.View,
                    FilesModels = await _articlesApiClient.GetFilesArticles(SystemConstants.ArticleSettings.NumberOfArticle),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method

        #region Utilities

        private async Task GetDropDownList(ArticleModel model)
        {
            var availableCategory = await _articlesApiClient.GetCategory();

            var categories = new List<SelectListItem>();
            var data = availableCategory;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new SelectListItem
                    {
                        Text = m.CategoryName,
                        Value = m.ArticleCategoryId,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableArticleCategories = new List<SelectListItem>(categories);
        }

        #endregion Utilities
    }
}