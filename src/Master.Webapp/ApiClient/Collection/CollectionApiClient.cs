using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace Master.Webapp.ApiClient
{
    public class CollectionApiClient : ICollectionApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CollectionApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region List

        public async Task<IList<CollectionModel>> GetActive(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/collection/get-active?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<CollectionModel>>(body);

            return JsonConvert.DeserializeObject<IList<CollectionModel>>(body);
        }
        public async Task<List<CollectionModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/collection/get-all");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<CollectionModel>>(body);

            return JsonConvert.DeserializeObject<List<CollectionModel>>(body);
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

        public async Task<List<FilesModel>> GetFilesCollection(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files/collection/{take}");
            return data;
        }

        public async Task<ApiResult<Pagination<CollectionModel>>> Get(CollectionSearchModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/collection/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<CollectionModel>>>(body);
            return model;
        }

        public async Task<ApiResult<CollectionModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/collection/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<CollectionModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<CollectionModel>>(body);
        }

        #endregion List

        #region Method

        public async Task<bool> Create(CollectionModel request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CollectionId) ? "" : request.CollectionId), "CollectionId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Quantity.ToString()) ? "" : request.Quantity.ToString()), "Quantity");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Factory) ? "" : request.Factory), "Factory");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Price.ToString()) ? "" : request.Price.ToString()), "Price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Sort.ToString()) ? "" : request.Sort.ToString()), "Sort");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hot.ToString()) ? "" : request.Hot.ToString()), "Hot");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Content) ? "" : request.Content), "Content");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.StatusProduct.ToString()) ? "" : request.StatusProduct.ToString()), "StatusProduct");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.BarCode) ? "" : request.BarCode), "BarCode");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateBy) ? "" : request.CreateBy), "CreateBy");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/collection/create", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string? id, CollectionModel request)
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

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CollectionId) ? "" : request.CollectionId), "CollectionId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Quantity.ToString()) ? "" : request.Quantity.ToString()), "Quantity");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Factory) ? "" : request.Factory), "Factory");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Price.ToString()) ? "" : request.Price.ToString()), "Price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Sort.ToString()) ? "" : request.Sort.ToString()), "Sort");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hot.ToString()) ? "" : request.Hot.ToString()), "Hot");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Content) ? "" : request.Content), "Content");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.StatusProduct.ToString()) ? "" : request.StatusProduct.ToString()), "StatusProduct");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.BarCode) ? "" : request.BarCode), "BarCode");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateBy) ? "" : request.CreateBy), "CreateBy");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"/collection/update/" + id + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/collection/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> CreateImage(FilesModel request, string collectionId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CollectionId) ? "" : request.CollectionId), "CollectionId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files/create-image?collectionId=" + collectionId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateImage(FilesModel request, string collectionId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CollectionId) ? "" : request.CollectionId), "CollectionId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files/update-image?collectionId=" + collectionId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files/delete-files?collectionId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> DeleteDataFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<AdminModel> GetByUserId(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/keycloak/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<AdminModel>(body);

            return JsonConvert.DeserializeObject<AdminModel>(body);
        }

        #endregion Method
    }
}