using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace Master.Webapp.ApiClient
{
    public class ConfigSiteApiClient : IConfigSiteApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfigSiteApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region List

        public async Task<List<ConfigSiteModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/configSite/get-all");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ConfigSiteModel>>(body);

            return JsonConvert.DeserializeObject<List<ConfigSiteModel>>(body);
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

        public async Task<List<FilesModel>> GetFilesConfigSite(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files-configsite/configsite/{take}");
            return data;
        }

        public async Task<ApiResult<Pagination<ConfigSiteModel>>> Get(ConfigSiteSearchModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/configSite/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<ConfigSiteModel>>>(body);
            return model;
        }

        public async Task<ApiResult<ConfigSiteModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/configSite/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ConfigSiteModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ConfigSiteModel>>(body);
        }

        #endregion List

        #region Method

        public async Task<bool> Create(ConfigSiteModel request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ConfigSiteId) ? "" : request.ConfigSiteId), "ConfigSiteId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Facebook) ? "" : request.Facebook), "Facebook");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Linkedin) ? "" : request.Linkedin), "Linkedin");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Youtube) ? "" : request.Youtube), "Youtube");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GoogleAnalytics) ? "" : request.GoogleAnalytics), "GoogleAnalytics");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.LiveChat) ? "" : request.LiveChat), "LiveChat");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.nameShopee) ? "" : request.nameShopee), "nameShopee");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.urlWeb) ? "" : request.urlWeb), "urlWeb");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GooglePlus) ? "" : request.GooglePlus), "GooglePlus");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Twitter) ? "" : request.Twitter), "Twitter");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GoogleMap) ? "" : request.GoogleMap), "GoogleMap");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title), "Title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ContactInfo) ? "" : request.ContactInfo), "ContactInfo");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FooterInfo) ? "" : request.FooterInfo), "FooterInfo");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hotline) ? "" : request.Hotline), "Hotline");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Email) ? "" : request.Email), "Email");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CoverImage) ? "" : request.CoverImage), "CoverImage");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SaleOffProgram) ? "" : request.SaleOffProgram), "SaleOffProgram");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FbMessage) ? "" : request.FbMessage), "FbMessage");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/configSite/create", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string? id, ConfigSiteModel request)
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

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ConfigSiteId) ? "" : request.ConfigSiteId), "ConfigSiteId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Facebook) ? "" : request.Facebook), "Facebook");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description), "Description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Linkedin) ? "" : request.Linkedin), "Linkedin");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Youtube) ? "" : request.Youtube), "Youtube");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GoogleAnalytics) ? "" : request.GoogleAnalytics), "GoogleAnalytics");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.LiveChat) ? "" : request.LiveChat), "LiveChat");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.nameShopee) ? "" : request.nameShopee), "nameShopee");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.urlWeb) ? "" : request.urlWeb), "urlWeb");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GooglePlus) ? "" : request.GooglePlus), "GooglePlus");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Twitter) ? "" : request.Twitter), "Twitter");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GoogleMap) ? "" : request.GoogleMap), "GoogleMap");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title), "Title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ContactInfo) ? "" : request.ContactInfo), "ContactInfo");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FooterInfo) ? "" : request.FooterInfo), "FooterInfo");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Hotline) ? "" : request.Hotline), "Hotline");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Email) ? "" : request.Email), "Email");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CoverImage) ? "" : request.CoverImage), "CoverImage");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SaleOffProgram) ? "" : request.SaleOffProgram), "SaleOffProgram");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FbMessage) ? "" : request.FbMessage), "FbMessage");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"/configSite/update/" + id + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/configSite/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> CreateImage(FilesModel request, string configSiteId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ConfigsiteId) ? "" : request.ConfigsiteId), "ConfigsiteId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-configsite/create-image?configsiteId=" + configSiteId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateImage(FilesModel request, string configsiteId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ConfigsiteId) ? "" : request.ConfigsiteId), "ConfigsiteId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-configsite/update-image?configsiteId=" + configsiteId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-configsite/delete-files?configsiteId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> DeleteDataFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-configsite/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method
    }
}