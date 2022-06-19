using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace Master.Webapp.ApiClient
{
    public class ProductApiClient : IProductApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region List

        public async Task<List<ProductModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/product/get-all");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ProductModel>>(body);

            return JsonConvert.DeserializeObject<List<ProductModel>>(body);
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

        public async Task<List<FilesModel>> GetFilesProduct(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files-product/product/{take}");
            return data;
        }

        public async Task<ApiResult<Pagination<ProductModel>>> Get(ProductSearchModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/product/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<ProductModel>>>(body);
            return model;
        }

        public async Task<ApiResult<ProductModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/product/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ProductModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ProductModel>>(body);
        }

        #endregion List

        #region Method

        public async Task<bool> Create(ProductModel request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name), "Name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategorieId) ? "" : request.ProductCategorieId), "ProductCategorieId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Quantity.ToString()) ? "" : request.Quantity.ToString()), "Quantity");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Factory) ? "" : request.Factory), "Factory");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Price.ToString()) ? "" : request.Price.ToString()), "Price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SaleOff.ToString()) ? "" : request.SaleOff.ToString()), "SaleOff");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.QuyCach) ? "" : request.QuyCach), "QuyCach");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Sort.ToString()) ? "" : request.Sort.ToString()), "Sort");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hot.ToString()) ? "" : request.Hot.ToString()), "Hot");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DescriptionMeta) ? "" : request.DescriptionMeta), "DescriptionMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GiftInfo) ? "" : request.GiftInfo), "GiftInfo");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Content) ? "" : request.Content), "Content");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.StatusProduct.ToString()) ? "" : request.StatusProduct.ToString()), "StatusProduct");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CollectionId) ? "" : request.CollectionId), "CollectionId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.BarCode) ? "" : request.BarCode), "BarCode");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateBy) ? "" : request.CreateBy), "CreateBy");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/product/create", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string? id, ProductModel request)
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

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name), "Name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductCategorieId) ? "" : request.ProductCategorieId), "ProductCategorieId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Quantity.ToString()) ? "" : request.Quantity.ToString()), "Quantity");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Factory) ? "" : request.Factory), "Factory");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Price.ToString()) ? "" : request.Price.ToString()), "Price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SaleOff.ToString()) ? "" : request.SaleOff.ToString()), "SaleOff");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.QuyCach) ? "" : request.QuyCach), "QuyCach");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Sort.ToString()) ? "" : request.Sort.ToString()), "Sort");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hot.ToString()) ? "" : request.Hot.ToString()), "Hot");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DescriptionMeta) ? "" : request.DescriptionMeta), "DescriptionMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GiftInfo) ? "" : request.GiftInfo), "GiftInfo");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Content) ? "" : request.Content), "Content");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.StatusProduct.ToString()) ? "" : request.StatusProduct.ToString()), "StatusProduct");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CollectionId) ? "" : request.CollectionId), "CollectionId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.BarCode) ? "" : request.BarCode), "BarCode");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateBy) ? "" : request.CreateBy), "CreateBy");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"/product/update/" + id + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/product/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> CreateImage(FilesModel request, string productId)
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

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductId) ? "" : request.ProductId), "ProductId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-product/create-image?productId=" + productId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateImage(FilesModel request, string productId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ProductId) ? "" : request.ProductId), "ProductId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-product/update-image?productId=" + productId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-product/delete-files?productId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> DeleteDataFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-product/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method
    }
}
