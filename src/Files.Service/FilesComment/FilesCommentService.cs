using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Files.Service
{
    public class FilesCommentService : IFilesCommentService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public FilesCommentService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region Method

        public async Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string commentId)
        {
            if (commentId is null)
            {
                throw new ArgumentNullException(nameof(commentId));
            }

            var list = new List<HouseWarehouseStore.Data.Entities.File>();
            foreach (var item in entities)
            {
                var files = new HouseWarehouseStore.Data.Entities.File()
                {
                    FileName = item.FileName,
                    Extension = item.Extension,
                    MimeType = item.MimeType,
                    Path = item.Path,
                    Size = item.Size,
                    CommentId = commentId,
                };

                files.Id = Guid.NewGuid().ToString();
                list.Add(files);
            }

            await _context.Files.AddRangeAsync(list);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string commentId)
        {
            if (commentId is null)
            {
                throw new ArgumentNullException(nameof(commentId));
            }

            var itemFiles = await _context.Files.FirstOrDefaultAsync(x => x.CommentId.Equals(commentId));
            var list = new List<HouseWarehouseStore.Data.Entities.File>();
            foreach (var file in entities)
            {
                itemFiles.FileName = file.FileName;
                itemFiles.Extension = file.Extension;
                itemFiles.MimeType = file.MimeType;
                itemFiles.Path = file.Path;
                itemFiles.Size = file.Size;
                file.Id = itemFiles.Id;
                file.CommentId = itemFiles.CommentId;
            }

            list.Add(itemFiles);
            _context.Files.Update(itemFiles);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Files.FirstOrDefaultAsync(x => x.CommentId.Equals(id));

            _context.Files.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method

        #region List

        public async Task<List<FilesModel>> GetFilesComment(int take)
        {
            //1. Select join
            var query = from p in _context.Files
                        select new { p };

            var data = await query.OrderByDescending(x => x.p.FileName).Take(take)
                .Select(x => new FilesModel()
                {
                    Id = x.p.Id,
                    FileName = x.p.FileName,
                    CommentId = x.p.CommentId,
                    Size = x.p.Size,
                    Extension = x.p.Extension,
                    MimeType = x.p.MimeType,
                    Path = x.p.Path
                }).ToListAsync();

            return data;
        }

        public async Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query =
                from x in _context.Files
                where x.CommentId == id
                select x;

            return await query.FirstOrDefaultAsync();
        }

        public Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var query =
                from x in _context.Files
                where x.CommentId == name
                select x;

            return query.FirstOrDefaultAsync();
        }

        #endregion List
    }
}