namespace HouseWarehouseStore.Models
{
    public class CounterModel
    {
        public string? Key { get; set; }
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}