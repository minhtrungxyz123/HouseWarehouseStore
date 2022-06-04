namespace HouseWarehouseStore.Models
{
    public class JobQueueModel
    {
        public string? Id { get; set; }
        public long JobId { get; set; }
        public string Queue { get; set; }
        public DateTime? FetchedAt { get; set; }
    }
}