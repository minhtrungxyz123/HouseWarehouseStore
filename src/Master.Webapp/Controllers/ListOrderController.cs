using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Data.UnitOfWork;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ListOrderController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public ListOrderController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult LoadOrder(int orderId = 0)
        {
            return ViewComponent("LoadOrder", new { orderId });
        }
        public IActionResult Index(int? page, string madonhang, string fromdate, string todate, string customerName, string customerEmail, string customerMobile, int status = -1, int payment = 0, int pageSize = 50)
        {
            int? pageNumber = page ?? 1;
            var orders = _unitOfWork.OrderRepository.Get(orderBy: q => q.OrderByDescending(a => a.Id), includeProperties: "OrderDetails");

            if (!string.IsNullOrEmpty(madonhang))
                orders = orders.Where(a => a.MaDonHang.Contains(madonhang));
            if (!string.IsNullOrEmpty(customerName))
                orders = orders.Where(a => a.Fullname.ToLower().Contains(customerName.ToLower()));
            if (!string.IsNullOrEmpty(customerEmail))
                orders = orders.Where(a => a.Email.ToLower().Contains(customerEmail.ToLower()));
            if (!string.IsNullOrEmpty(customerMobile))
                orders = orders.Where(a => a.Mobile.Contains(customerMobile));
            if (DateTime.TryParse(fromdate, new CultureInfo("vi-VN"), DateTimeStyles.None, out var fd))
                orders = orders.Where(a => a.CreateDate.Date >= fd.Date);
            if (DateTime.TryParse(todate, new CultureInfo("vi-VN"), DateTimeStyles.None, out var td))
                orders = orders.Where(a => a.CreateDate.Date <= td.Date);
            if (status >= 0)
                orders = orders.Where(a => a.Status == status);
            if (payment > 0)
                switch (payment)
                {
                    case 1:
                        orders = orders.Where(a => !a.Payment);
                        break;

                    case 2:
                        orders = orders.Where(a => a.Payment);
                        break;
                }

            var model = new ListOrderViewModel
            {
                Orders = PaginatedList<Order>.CreateAsync(orders, pageNumber ?? 1, pageSize),
                MaDonhang = madonhang,
                Status = status,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                CustomerMobile = customerMobile,
                FromDate = fromdate,
                ToDate = todate,
                PageSize = pageSize,
                Payment = payment,
                UserVoucher = _unitOfWork.UserVoucherRepository.Get()
            };

            return View(model);
        }

        public void ExportOrder()
        {
            var fileName = "danh-sach-don-hang-thang" + DateTime.Now.Month + "-nam-" + DateTime.Now.Year + ".xlsx";

            var orders = _unitOfWork.OrderRepository.Get(includeProperties: "OrderDetails").ToList();

            using var pck = new ExcelPackage();
            //Create the worksheet
            var ws = pck.Workbook.Worksheets.Add("Danh sách đơn hàng");
            ws.DefaultColWidth = 20;
            ws.DefaultRowHeight = 40;
            ws.Cells.Style.WrapText = true;
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Column(4).Width = 40;
            ws.Column(5).Width = 50;

            ws.Cells[1, 1].Value = "Mã ĐH";
            ws.Cells[1, 2].Value = "Tên";
            ws.Cells[1, 3].Value = "Điện thoại";
            ws.Cells[1, 4].Value = "Email";
            ws.Cells[1, 5].Value = "Địa chỉ";
            ws.Cells[1, 6].Value = "Tổng tiền";
            ws.Cells[1, 7].Value = "Đã thanh toán";
            ws.Cells[1, 8].Value = "Công nợ";
            ws.Cells[1, 9].Value = "Ngày đặt";
            ws.Cells[1, 10].Value = "Ngày giao";
            ws.Cells[1, 11].Value = "Vận chuyển";
            ws.Cells[1, 12].Value = "Tình trạng Thanh toán";
            ws.Cells[1, 13].Value = "Tình trạng đơn hàng";
            ws.Cells[1, 14].Value = "Ghi chú";
            var i = 2;
            foreach (var order in orders)
            {
                var status = "Đang xử lý";
                switch (order.Status)
                {
                    case 1:
                        status = "Đang giao hàng";
                        break;

                    case 2:
                        status = "Đã thanh toán";
                        break;

                    case 3:
                        status = "Hủy đơn";
                        break;
                }
                var transport = "Hình thức khác";
                switch (order.Transport)
                {
                    case 1:
                        status = "Đến địa chỉ người nhận";
                        break;

                    case 2:
                        status = "Khách đến nhận hàng";
                        break;

                    case 3:
                        status = "Qua bưu điện";
                        break;
                }

                var total = order.OrderDetails.Sum(a => a.Price * a.Quantity);
                var congno = total - order.ThanhToanTruoc;

                ws.Cells[i, 1].Value = order.MaDonHang;
                ws.Cells[i, 2].Value = order.Fullname;
                ws.Cells[i, 3].Value = order.Mobile;
                ws.Cells[i, 4].Value = order.Email;
                ws.Cells[i, 5].Value = order.Address;
                ws.Cells[i, 6].Value = total;
                ws.Cells[i, 7].Value = order.ThanhToanTruoc;
                ws.Cells[i, 8].Value = congno;
                ws.Cells[i, 9].Value = order.CreateDate.ToString("dd/MM/yyyy HH:mm");
                ws.Cells[i, 10].Value = order.TransportDate.ToString("dd/MM/yyyy");
                ws.Cells[i, 11].Value = transport;
                ws.Cells[i, 12].Value = !order.Payment ? "Chưa thanh toán" : "Đã thanh toán";
                ws.Cells[i, 13].Value = status;
                ws.Cells[i, 14].Value = order.Body;
                i++;
            }

            //Format the header for column 1-14
            using (var rng = ws.Cells["A1:N1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));  //Set color to dark blue
                rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
            }

            //Write it back to the client
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment;  filename=" + fileName + "");
            Response.Body.WriteAsync(pck.GetAsByteArray());
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public bool DeleteOrder(string orderId)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = 3;
            _unitOfWork.SaveNotAync();
            return true;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public bool UpdateOrderNotice(string notice, int thanhtoantruoc = 0, int orderId = 0)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);
            if (order == null)
            {
                return false;
            }
            order.ThanhToanTruoc = thanhtoantruoc;
            order.Body = notice;
            _unitOfWork.SaveNotAync();
            return true;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public bool UpdateOrder(string notice, int payment = 0, int status = 0, int orderId = 0)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);
            if (order == null)
            {
                return false;
            }
            if (status >= 0)
            {
                order.Status = status;
            }
            if (payment > 0)
            {
                switch (payment)
                {
                    case 1:
                        order.Payment = false;
                        break;
                    case 2:
                        order.Payment = true;
                        break;
                }
            }

            _unitOfWork.SaveNotAync();
            return true;
        }
    }
}