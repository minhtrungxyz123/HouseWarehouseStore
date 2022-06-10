using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleCategoryController : Controller
    {
        #region Fields

        private readonly IArticleCategoryApiClient _articleCategoryApiClient;

        public ArticleCategoryController(IArticleCategoryApiClient articleCategoryApiClient)
        {
            _articleCategoryApiClient = articleCategoryApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ArticleCategorySearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _articleCategoryApiClient.Get(request);
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
        public ActionResult Create()
        {
            var model = new ArticleCategoryModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCategoryModel request)
        {
            request.Slug = "1";
            if (!ModelState.IsValid)
                return View(request);

            var result = await _articleCategoryApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _articleCategoryApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ArticleCategoryModel()
                {
                    CategoryName = model.CategoryName,
                    ArticleCategoryId = id,
                    CategoryActive = model.CategoryActive,
                    CategorySort = model.CategorySort,
                    DescriptionMeta = model.DescriptionMeta,
                    Hot = model.Hot,
                    ParentId = model.ParentId,
                    ShowHome = model.ShowHome,
                    ShowMenu = model.ShowMenu,
                    Slug = model.Slug,
                    TitleMeta = model.TitleMeta,
                    Url = model.Url,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleCategoryModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _articleCategoryApiClient.Edit(request.ArticleCategoryId, request);
            if (result)
            {
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
            var result = await _articleCategoryApiClient.Delete(id);
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
            var result = await _articleCategoryApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ArticleCategoryModel()
                {
                    CategoryName = model.CategoryName,
                    ArticleCategoryId = id,
                    ParentId = model.ParentId,
                    Url = model.Url,
                    TitleMeta = model.TitleMeta,
                    Slug = model.Slug,
                    ShowMenu = model.ShowMenu,
                    ShowHome = model.ShowHome,
                    Hot = model.Hot,
                    CategoryActive = model.CategoryActive,
                    CategorySort = model.CategorySort,
                    DescriptionMeta = model.DescriptionMeta
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}