namespace HouseWarehouseStore.Models
{
    public class AggregatedCounterModel
    {
        public string? Key { get; set; }
        public long Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}