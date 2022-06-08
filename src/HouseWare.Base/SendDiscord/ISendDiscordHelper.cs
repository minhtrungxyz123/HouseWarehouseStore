namespace HouseWare.Base
{
    public interface ISendDiscordHelper
    {
        Task<object> SendMessage(string content);
    }
}