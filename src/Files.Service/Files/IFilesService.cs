namespace Files.Service
{
    public interface IFilesService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);
    }
}