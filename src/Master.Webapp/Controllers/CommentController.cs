using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        #region Fields

        private readonly ICommentApiClient _commentApiClient;

        public CommentController(ICommentApiClient commentApiClient)
        {
            _commentApiClient = commentApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new CommentSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _commentApiClient.Get(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        #endregion List

        #region Method

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _commentApiClient.Delete(id);

            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        #endregion Method
    }
}