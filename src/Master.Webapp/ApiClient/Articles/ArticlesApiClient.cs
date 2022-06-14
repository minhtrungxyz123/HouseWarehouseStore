using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace Master.Webapp.ApiClient
{
    public class ArticlesApiClient : IArticlesApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticlesApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region List

        public async Task<IList<ArticleCategoryModel>> GetCategory(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/article-category/get-category-active?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<ArticleCategoryModel>>(body);

            return JsonConvert.DeserializeObject<IList<ArticleCategoryModel>>(body);
        }

        public async Task<List<ArticleModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/articles/get-all");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ArticleModel>>(body);

            return JsonConvert.DeserializeObject<List<ArticleModel>>(body);
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

        public async Task<List<FilesModel>> GetFilesArticles(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files-articles/articles/{take}");
            return data;
        }

        public async Task<ApiResult<Pagination<ArticleModel>>> Get(ArticleSearchModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/articles/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<ArticleModel>>>(body);
            return model;
        }

        public async Task<ApiResult<ArticleModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/articles/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ArticleModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ArticleModel>>(body);
        }

        #endregion List

        #region Method

        public async Task<bool> Create(ArticleModel request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Id) ? "" : request.Id), "Id");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Subject) ? "" : request.Subject), "Subject");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.View.ToString()) ? "" : request.View.ToString()), "View");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArticleCategoryId) ? "" : request.ArticleCategoryId), "ArticleCategoryId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Url) ? "" : request.Url), "Url");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.KeyWord) ? "" : request.KeyWord), "KeyWord");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hot.ToString()) ? "" : request.Hot.ToString()), "Hot");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DescriptionMetaTitle) ? "" : request.DescriptionMetaTitle), "DescriptionMetaTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/articles/create", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string? id, ArticleModel request)
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

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Id) ? "" : request.Id), "Id");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Subject) ? "" : request.Subject), "Subject");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Body) ? "" : request.Body), "Body");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.View.ToString()) ? "" : request.View.ToString()), "View");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArticleCategoryId) ? "" : request.ArticleCategoryId), "ArticleCategoryId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Url) ? "" : request.Url), "Url");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.KeyWord) ? "" : request.KeyWord), "KeyWord");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hot.ToString()) ? "" : request.Hot.ToString()), "Hot");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Home.ToString()) ? "" : request.Home.ToString()), "Home");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.TitleMeta) ? "" : request.TitleMeta), "TitleMeta");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DescriptionMetaTitle) ? "" : request.DescriptionMetaTitle), "DescriptionMetaTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"/articles/update/" + id + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/articles/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> CreateImage(FilesModel request, string articleId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArticlesId) ? "" : request.ArticlesId), "ArticlesId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-articles/create-image?articlesId=" + articleId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateImage(FilesModel request, string articlesId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArticlesId) ? "" : request.ArticlesId), "ArticlesId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-articles/update-image?articleId=" + articlesId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-articles/delete-files?articleId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> DeleteDataFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-articles/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method
    }
}