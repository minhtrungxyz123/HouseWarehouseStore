namespace HouseWare.Base
{
    public class SendDiscordHelper : ISendDiscordHelper
    {
        private readonly IHttpClientFactory _clientFactory;

        public SendDiscordHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<object> SendMessage(string content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            try
            {
                var avatar = "https://scontent.fhan12-1.fna.fbcdn.net/v/t39.30808-6/283838965_3301948990085889_3994107580724281571_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=174925&_nc_ohc=cY8guja7ENgAX9MRav0&_nc_ht=scontent.fhan12-1.fna&oh=00_AT-tEU-2L-iRTT1WblgNF4H6PEm0W322P9EjS-IUfgW25Q&oe=62A60AEC";
                var username = "Thông báo Admin !";
                var Webhook = "https://discord.com/api/webhooks/984270697431986217/Gpd0pLVgWrC8piV1KmxJEzZEJRatTGSYsuPG6DryuPmwa85-40-mmkCiZamAT0s7aAIU";
                if (string.IsNullOrEmpty(Webhook))
                    return false;
                var formdata = new MultipartFormDataContent();
                formdata.Add(new StringContent(username), "username");
                formdata.Add(new StringContent(content), "content");
                formdata.Add(new StringContent(avatar), "avatar_url");
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(Webhook, formdata);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                return response;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public class DiscordMessageModel
    {
        public string Webhook { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Content { get; set; }
    }
}