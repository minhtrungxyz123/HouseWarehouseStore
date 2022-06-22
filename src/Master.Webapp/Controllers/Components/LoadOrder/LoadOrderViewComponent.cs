using HouseWarehouseStore.Data.UnitOfWork;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.LoadOrder
{
    public class LoadOrderViewComponent : ViewComponent
    {
        private readonly UnitOfWork _unitOfWork;

        public LoadOrderViewComponent(UnitOfWork context)
        {
            _unitOfWork = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orderId)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);
            var orderrderdetails = await _unitOfWork.OrderDetailRepository.GetAync(a => a.OrderId == orderId, includeProperties: "Product,Order");
            var model = new OrderViewModel
            {
                Order = order,
                OrderDetails = orderrderdetails,
                UserVoucher = _unitOfWork.UserVoucherRepository.Get(x => x.MaDonHang.Equals(order.MaDonHang))
            };
            return View(model);
        }
    }
}
