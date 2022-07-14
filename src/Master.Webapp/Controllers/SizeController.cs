using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SizeController : Controller
    {
        #region Fields

        private readonly IProductApiClient _productApiCient;
        private readonly ISizeApiClient _sizeApiClient;
        private readonly INotificationApiClient _notificationApiClient;

        public SizeController(ISizeApiClient sizeApiClient,
            INotificationApiClient notificationApiClient,
            IProductApiClient productApiCient)
        {
            _sizeApiClient = sizeApiClient;
            _notificationApiClient = notificationApiClient;
            _productApiCient = productApiCient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new SizeSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _sizeApiClient.Get(request);
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
            return ViewComponent("CreateSize");
        }

        [HttpPost]
        public async Task<IActionResult> Create(SizeModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _sizeApiClient.Create(request);
            var noti = new NotificationModel();
            noti.CreatedDate = DateTime.Now;
            noti.NotiBody = request.SizeProduct;
            noti.Url = "http://localhost:5100/Size";
            noti.NotiHeader = request.SizeProduct;
            noti.IsRead = true;

            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Username").Value;
            noti.UserId = $"Tai khoan [{userId}] da them 1 ban ghi !";

            if (result)
            {
                await _notificationApiClient.Create(noti);
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _sizeApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new SizeModel()
                {
                    SizeProduct = model.SizeProduct,
                    SizeId = id,
                    AvailableProduct = model.AvailableProduct
                };
                if (model.AvailableProduct.Count > 0 &&
                !string.IsNullOrEmpty(model.ProductId))
                {
                    var item = model.AvailableProduct
                        .FirstOrDefault(x => x.Value.Equals(model.ProductId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
                return ViewComponent("EditSize", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SizeModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _sizeApiClient.Edit(request.SizeId, request);
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
            var result = await _sizeApiClient.Delete(id);
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
            var result = await _sizeApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new SizeModel()
                {
                    SizeProduct = model.SizeProduct,
                    SizeId = id,
                    AvailableProduct = model.AvailableProduct,
                };
                if (model.AvailableProduct.Count > 0 &&
                !string.IsNullOrEmpty(model.ProductId))
                {
                    var item = model.AvailableProduct
                        .FirstOrDefault(x => x.Value.Equals(model.ProductId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
                return ViewComponent("DetailSize", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method

        #region export

        public async Task<ActionResult> ExportOrder()
        {
            var fileName = "danh-sach-size.xlsx";
            var res = await _sizeApiClient.GetActive();
            var orders = res;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var pck = new ExcelPackage();
            //Create the worksheet
            var ws = pck.Workbook.Worksheets.Add("Danh sách size");
            ws.DefaultColWidth = 20;
            ws.Cells.Style.WrapText = true;
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Column(1).Width = 10;
            ws.Column(2).Width = 15;
            ws.Column(3).Width = 25;
            ws.Column(8).Width = 15;
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[2, 1].Value = "STT";
            ws.Cells[2, 3].Value = "Tên size";
            var i = 3;
            if (orders != null)
                foreach (var order in orders)
                {
                    ws.Cells[i, 1].Value = i - 2;
                    ws.Cells[i, 2].Value = order.SizeProduct;
                    i++;
                }

            // set style title

            using (var rng = ws.Cells["D1"])
            {
                rng.Value = "Danh sách size";
                rng.Merge = true;
                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);  //Set color to dark blue
                rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
            }

            // set style name column
            using (var rng = ws.Cells["A2:B2"])
            {
                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));  //Set color to dark blue
                rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
            }

            return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        #endregion export

        #region Utilities

        private async Task GetDropDownList(SizeModel model)
        {
            var availableProd = await _productApiCient.GetActive();

            var categories = new List<SelectListItem>();
            var data = availableProd;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.ProductId,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableProduct = new List<SelectListItem>(categories);
        }

        #endregion Utilities
    }
}