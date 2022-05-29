namespace HouseWarehouseStore.Models
{
    public class OrderModel
    {
        public string MaDonHang { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Payment { get; set; }
        public int TypePay { get; set; }
        public int Transport { get; set; }
        public DateTime TransportDate { get; set; }
        public int Status { get; set; }
        public int? OrderMemberId { get; set; }
        public bool Viewed { get; set; }
        public int ThanhToanTruoc { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
    }
}