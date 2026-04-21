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
            try
            {
                var version = GetSetting("WhatsAppBusiness:Version", "WhatsAppBussines:Version");
                var phoneNumber = GetSetting("WhatsAppBusiness:PhoneNumber", "WhatsAppBussines:NroWhatsApp");
                var bearerToken = GetSetting("WhatsAppBusiness:BearerToken", "WhatsAppBussines:BRT");

                if (string.IsNullOrWhiteSpace(version) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(bearerToken))
                {
                    return "Faltan valores de configuración de WhatsAppBusiness en appsettings.";
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://graph.facebook.com/{version}/{phoneNumber}/messages");
                request.Headers.Add("Authorization", $"Bearer {bearerToken}");

                var content = new StringContent(body, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return errorContent;
                }
                else
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string? GetSetting(string primaryKey, string fallbackKey)
        {
            return _configuration[primaryKey] ?? _configuration[fallbackKey];
        }
    }
}
