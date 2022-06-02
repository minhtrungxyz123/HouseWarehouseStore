namespace Files.Service
{
    public interface IFilesService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.Files> entities);

        Task<HouseWarehouseStore.Data.Entities.Files> GetByIdAsync(int? id);
    }
}