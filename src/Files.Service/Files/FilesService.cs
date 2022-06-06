﻿using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Files.Service
{
    public class FilesService : IFilesService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public FilesService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region Method

        public async Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId)
        {
            if (collectionId is null)
            {
                throw new ArgumentNullException(nameof(collectionId));
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
                    CollectionId = collectionId,
                };

                files.Id = Guid.NewGuid().ToString();
                list.Add(files);
            }

            await _context.Files.AddRangeAsync(list);


            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId)
        {
            if (collectionId is null)
            {
                throw new ArgumentNullException(nameof(collectionId));
            }

            var list = new List<HouseWarehouseStore.Data.Entities.File>();
            foreach (var item in entities)
            {
                var files = new HouseWarehouseStore.Data.Entities.File()
                {
                    Id = item.Id,
                    FileName = item.FileName,
                    Extension = item.Extension,
                    MimeType = item.MimeType,
                    Path = item.Path,
                    Size = item.Size,
                    CollectionId = collectionId,
                };

                list.Add(files);
            }

            _context.Files.UpdateRange(list);


            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Files.FindAsync(id);

            _context.Files.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method

        #region List

        public async Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query =
                from x in _context.Files
                where x.CollectionId == id
                select x;

            return await query.FirstOrDefaultAsync();
        }

        #endregion
    }
}