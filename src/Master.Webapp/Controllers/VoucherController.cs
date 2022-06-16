using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Data.Repositories;
using HouseWarehouseStore.Data.UnitOfWork;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VoucherController : Controller
    {
        #region Fields

        private readonly IVoucherApiClient _voucherApiClient;
        private readonly UnitOfWork _unitOfWork;

        public VoucherController(IVoucherApiClient voucherApiClient,
            UnitOfWork unitOfWork)
        {
            _voucherApiClient = voucherApiClient;
            _unitOfWork = unitOfWork;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new VoucherSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _voucherApiClient.Get(request);
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
            var model = new VoucherModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoucherModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _voucherApiClient.Create(request);

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
            var result = await _voucherApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new VoucherModel()
                {
                    Name = model.Name,
                    Id = id,
                    Active = model.Active,
                    Code = model.Code,
                    Condition = model.Condition,
                    PriceDown = model.PriceDown,
                    PriceUp = model.PriceUp,
                    ReductionMax = model.ReductionMax,
                    SumUse = model.SumUse,
                    Type = model.Type,
                    Value = model.Value,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VoucherModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _voucherApiClient.Edit(request.Id, request);
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
            var result = await _voucherApiClient.Delete(id);
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
            var result = await _voucherApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new VoucherModel()
                {
                    Name = model.Name,
                    Id = id,
                    Value = model.Value,
                    Type = model.Type,
                    SumUse = model.SumUse,
                    ReductionMax = model.ReductionMax,
                    PriceUp = model.PriceUp,
                    PriceDown = model.PriceDown,
                    Active = model.Active,
                    Code = model.Code,
                    Condition = model.Condition
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method

        #region Uni

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult VoucherRandom(string code)
        {
            if (code == null)
                return Ok(false);
            var voucher = _unitOfWork.VoucherRepository.Get(x => x.Code.Equals(code)).FirstOrDefault();
            if (voucher != null)
                return Ok(false);
            return Ok(new { c = code, t = true });
        }

        #endregion
    }
}