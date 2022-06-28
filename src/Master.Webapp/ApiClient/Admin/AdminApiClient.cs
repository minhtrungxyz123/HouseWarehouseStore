using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace Master.Webapp.ApiClient
{
    public class AdminApiClient : IAdminApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region List

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

        public async Task<AdminModel> GetCheckActive(string id, bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/admin/check-active?name={id}&showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<AdminModel>(body);

            return JsonConvert.DeserializeObject<AdminModel>(body);
        }

        public async Task<ApiResult<Pagination<AdminModel>>> Get(AdminSearchModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/admin/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<AdminModel>>>(body);
            return model;
        }

        public async Task<ApiResult<AdminModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/admin/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<AdminModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<AdminModel>>(body);
        }

        #endregion List

        #region Method

        public async Task<bool> DeleteFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-admin/delete-files?adminId={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<List<FilesModel>> GetFilesAdmin(int take)
        {
            var data = await GetListAsync<FilesModel>($"/files-admin/admin/{take}");
            return data;
        }

        public async Task<bool> DeleteDataFiles(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.DeleteAsync($"/files-admin/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        public async Task<bool> UpdateImage(FilesModel request, string adminId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AdminId) ? "" : request.AdminId), "AdminId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-admin/update-image?adminId=" + adminId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateImage(FilesModel request, string adminId)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AdminId) ? "" : request.AdminId), "AdminId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["ApiFiles"]);
            var response = await client.PostAsync("/files-admin/create-image?adminId=" + adminId + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Create(AdminModel request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Username) ? "" : request.Username), "Username");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Password) ? "" : request.Password), "Password");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Role) ? "" : request.Role), "Role");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FullName) ? "" : request.FullName), "FullName");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Address) ? "" : request.Address), "Address");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Sex) ? "" : request.Sex), "Sex");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Age) ? "" : request.Age), "Age");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Position) ? "" : request.Position), "Position");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Email) ? "" : request.Email), "Email");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/admin/create", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string? id, AdminModel request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Username) ? "" : request.Username), "Username");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Password) ? "" : request.Password), "Password");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Active.ToString()) ? "" : request.Active.ToString()), "Active");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Role) ? "" : request.Role), "Role");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Image) ? "" : request.Image), "Image");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FullName) ? "" : request.FullName), "FullName");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.CreateDate.ToString()) ? "" : request.CreateDate.ToString()), "CreateDate");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Address) ? "" : request.Address), "Address");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Sex) ? "" : request.Sex), "Sex");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Age) ? "" : request.Age), "Age");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Position) ? "" : request.Position), "Position");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Email) ? "" : request.Email), "Email");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"/admin/update/" + id + "", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/admin/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method
    }
}