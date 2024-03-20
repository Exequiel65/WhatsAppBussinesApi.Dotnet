using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WhatsAppBussinesApi.Dotnet.Structure;

namespace WhatsAppBussinesApi.Dotnet.Client
{
    public interface IWhatsAppBusinessClient
    {
        Task<string?> SendMessage(BaseMessage message);
    }

    public class WhatsAppBusinessClient : IWhatsAppBusinessClient
    {
        private readonly IConfiguration _configuration;

        public WhatsAppBusinessClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> SendMessage(BaseMessage message)
        {
            if (string.IsNullOrEmpty(message.to))
            {
                return "Sender is null";
            }

            var json = JsonConvert.SerializeObject(message);
            return await Post(json);

        }

        private async Task<string?> Post(dynamic body)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://graph.facebook.com/{_configuration.GetSection("WhatsAppBussines:Version").Value}/{_configuration.GetSection("WhatsAppBussines:NroWhatsApp").Value}/messages");
            request.Headers.Add("Authorization", $"Bearer {_configuration.GetSection("WhatsAppBussines:BRT").Value}");

            var content = new StringContent(body, null, "application/json");
            request.Content = content;

            try
            {
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return errorContent;
                }
                else
                {

                    return await response.Content.ReadAsStringAsync(); ;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
