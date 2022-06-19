using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace Master.Webapp.ApiClient
{
    public class ProductCategoryApiCient : IProductCategoryApiCient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductCategoryApiCient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region List

        public async Task<List<ProductCategoryModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/product-category/get-all");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ProductCategoryModel>>(body);

            return JsonConvert.DeserializeObject<List<ProductCategoryModel>>(body);
        }

        public async Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
                return data;
            }
            throw new Exception(body);
        }

        public async Task<List<FilesModel>> GetFilesProductCategory(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files-product-category/productCategory/{take}");
            return data;
        }

        public async Task<ApiResult<Pagination<ProductCategoryModel>>> Get(ProductCategorySearchModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/product-category/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<ProductCategoryModel>>>(body);
            return model;
        }

        public async Task<ApiResult<ProductCategoryModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/product-category/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ProductCategoryModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ProductCategoryModel>>(body);
        }

        #endregion List

        #region Method

        public async Task<bool> Create(ProductCategoryModel request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.filesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.filesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.filesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "filesadd", request.filesadd.FileName);
            }

            if (request.Coverfilesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Coverfilesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Coverfilesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Coverfilesadd", request.filesadd.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategorieId) ? "" : request.ProductCategorieId), "ProductCategorieId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CoverImage) ? "" : request.CoverImage), "CoverImage");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Url) ? "" : request.Url), "Url");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Soft.ToString()) ? "" : request.Soft.ToString()), "Soft");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ParentId) ? "" : request.ParentId), "ParentId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DescriptionMeta) ? "" : request.DescriptionMeta), "DescriptionMeta");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/product-category/create", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string? id, ProductCategoryModel request)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var requestContent = new MultipartFormDataContent();

            if (request.filesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.filesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.filesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "filesadd", request.filesadd.FileName);
            }

            if (request.Coverfilesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Coverfilesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Coverfilesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Coverfilesadd", request.Coverfilesadd.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategorieId) ? "" : request.ProductCategorieId), "ProductCategorieId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CoverImage) ? "" : request.CoverImage), "CoverImage");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Url) ? "" : request.Url), "Url");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Soft.ToString()) ? "" : request.Soft.ToString()), "Soft");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ParentId) ? "" : request.ParentId), "ParentId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DescriptionMeta) ? "" : request.DescriptionMeta), "DescriptionMeta");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"/product-category/update/" + id + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/product-category/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> CreateImage(FilesModel request, string productCategoryId)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.filesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.filesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.filesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "filesadd", request.filesadd.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategoryId) ? "" : request.ProductCategoryId), "ProductCategoryId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-product-category/create-image?productCategoryId=" + productCategoryId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateImage(FilesModel request, string productCategoryId)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.filesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.filesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.filesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "filesadd", request.filesadd.FileName);
            }
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategoryId) ? "" : request.ProductCategoryId), "ProductCategoryId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-product-category/update-image?productCategoryId=" + productCategoryId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-product-category/delete-files?productCategoryId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> DeleteDataFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-product-category/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> CreateImageConver(FilesModel request, string productCategoryId)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Coverfilesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Coverfilesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Coverfilesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Coverfilesadd", request.Coverfilesadd.FileName);
            }

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategoryId) ? "" : request.ProductCategoryId), "ProductCategoryId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-product-category/create-image-cover?productCategoryId=" + productCategoryId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<FilesModel>> GetFilesCoverProductCategory(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files-product-category/filesProductCategory/{take}");
            return data;
        }

        public async Task<bool> DeleteFilesCover(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-product-category/delete-files-cover?productCategoryId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> UpdateImageCover(FilesModel request, string productCategoryId)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Coverfilesadd != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Coverfilesadd.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Coverfilesadd.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Coverfilesadd", request.Coverfilesadd.FileName);
            }
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategoryId) ? "" : request.ProductCategoryId), "ProductCategoryId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-product-category/update-image-cover?productCategoryId=" + productCategoryId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDataFilesCover(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-product-category/delete-cover?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method
    }
}