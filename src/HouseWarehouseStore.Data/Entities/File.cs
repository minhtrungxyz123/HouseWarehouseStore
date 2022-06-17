namespace HouseWarehouseStore.Data.Entities
{
    public partial class File
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string? Path { get; set; }
        public decimal Size { get; set; }
        public string? Extension { get; set; }
        public string? MimeType { get; set; }
        public string? CollectionId { get; set; }
        public string? ArticleId { get; set; }
        public string? ConfigSiteId { get; set; }
        public string? BannerId { get; set; }
    }
}