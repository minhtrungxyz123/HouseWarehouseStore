using Microsoft.AspNetCore.Http;

namespace HouseWarehouseStore.Models
{
    public class FilesModel
    {
        public string? Id { get; set; }
        public string? FileName { get; set; }

        public string? Path { get; set; }

        public string? Extension { get; set; }

        public string? MimeType { get; set; }

        public decimal? Size { get; set; }

        public string? CollectionId { get; set; }
        public string? CommentId { get; set; }
        public IFormFile? filesadd { get; set; }
        public IFormFile? Coverfilesadd { get; set; }
        public string? ArticlesId { get; set; }
        public string? ConfigsiteId { get; set; }
        public string? BannerId { get; set; }
        public string? ProductCategoryId { get; set; }
        public string? ProductId { get; set; }
        public string? AdminId { get; set; }
        public string? AlbumId { get; set; }
    }
}