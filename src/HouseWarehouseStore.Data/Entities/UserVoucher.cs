namespace HouseWarehouseStore.Data.Entities
{
    public partial class UserVoucher
    {
        public int Id { get; set; }
        public string MaDonHang { get; set; }
        public decimal SumHd { get; set; }
        public string Code { get; set; }
    }
}