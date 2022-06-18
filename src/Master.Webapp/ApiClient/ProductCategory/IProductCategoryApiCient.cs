﻿using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IProductCategoryApiCient
    {
        public Task<bool> Create(ProductCategoryModel request);

        public Task<bool> CreateImage(FilesModel request, string productCategoryId);

        public Task<bool> UpdateImage(FilesModel request, string productCategoryId);

        public Task<bool> Edit(string? id, ProductCategoryModel request);

        Task<ApiResult<Pagination<ProductCategoryModel>>> Get(ProductCategorySearchModel request);

        Task<ApiResult<ProductCategoryModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesProductCategory(int take);

        Task<bool> DeleteFiles(string id);

        Task<List<ProductCategoryModel>> GetAll();
    }
}