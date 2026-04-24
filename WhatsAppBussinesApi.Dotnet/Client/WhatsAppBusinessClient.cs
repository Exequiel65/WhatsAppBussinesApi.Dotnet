using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WhatsAppBussinesApi.Dotnet.Media;
using WhatsAppBussinesApi.Dotnet.Structure;

namespace WhatsAppBussinesApi.Dotnet.Client
{
    public interface IWhatsAppBusinessClient
    {
        Task<string?> SendMessage(BaseMessage message);
        Task<string?> UploadMedia(Stream fileStream, string fileName, string mimeType);
        Task<string?> GetMediaUrl(string mediaId, string? phoneNumberId = null);
        Task<string?> DeleteMedia(string mediaId, string? phoneNumberId = null);
        Task<MediaDownloadResult> DownloadMedia(string mediaUrl);
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

        public async Task<string?> UploadMedia(Stream fileStream, string fileName, string mimeType)
        {
            try
            {
                if (fileStream is null)
                {
                    return "File stream is required.";
                }

                if (!fileStream.CanRead)
                {
                    return "File stream is not readable.";
                }

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return "File name is required.";
                }

                if (string.IsNullOrWhiteSpace(mimeType))
                {
                    return "MIME type is required.";
                }

                if (fileStream.CanSeek)
                {
                    var streamLength = fileStream.Length;
                    var validationError = WhatsAppMediaValidator.Validate(mimeType, streamLength);
                    if (validationError is not null)
                    {
                        return validationError;
                    }

                    fileStream.Position = 0;
                }

                var version = GetSetting("WhatsAppBusiness:Version", "WhatsAppBussines:Version");
                var phoneNumber = GetSetting("WhatsAppBusiness:PhoneNumber", "WhatsAppBussines:NroWhatsApp");
                var bearerToken = GetSetting("WhatsAppBusiness:BearerToken", "WhatsAppBussines:BRT");

                if (string.IsNullOrWhiteSpace(version) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(bearerToken))
                {
                    return "Faltan valores de configuración de WhatsAppBusiness en appsettings.";
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://graph.facebook.com/{version}/{phoneNumber}/media");
                request.Headers.Add("Authorization", $"Bearer {bearerToken}");

                using var form = new MultipartFormDataContent();
                form.Add(new StringContent("whatsapp"), "messaging_product");

                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mimeType);
                form.Add(fileContent, "file", fileName);

                request.Content = form;

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return responseContent;
                }

                return TryExtractId(responseContent) ?? responseContent;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<MediaDownloadResult> DownloadMedia(string mediaUrl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mediaUrl))
                {
                    return MediaDownloadResult.Failed("Media URL is required.");
                }

                var bearerToken = GetSetting("WhatsAppBusiness:BearerToken", "WhatsAppBussines:BRT");
                if (string.IsNullOrWhiteSpace(bearerToken))
                {
                    return MediaDownloadResult.Failed("Faltan valores de configuración de WhatsAppBusiness en appsettings.");
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, mediaUrl);
                request.Headers.Add("Authorization", $"Bearer {bearerToken}");

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return MediaDownloadResult.Failed(errorContent, (int)response.StatusCode);
                }

                var bytes = await response.Content.ReadAsByteArrayAsync();
                var mimeType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                var fileName = response.Content.Headers.ContentDisposition?.FileNameStar
                    ?? response.Content.Headers.ContentDisposition?.FileName
                    ?? "downloaded-media";

                fileName = fileName.Trim('"');

                return MediaDownloadResult.Success(bytes, mimeType, fileName);
            }
            catch (Exception e)
            {
                return MediaDownloadResult.Failed(e.Message);
            }
        }

        public async Task<string?> DeleteMedia(string mediaId, string? phoneNumberId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mediaId))
                {
                    return "Media id is required.";
                }

                var version = GetSetting("WhatsAppBusiness:Version", "WhatsAppBussines:Version");
                var bearerToken = GetSetting("WhatsAppBusiness:BearerToken", "WhatsAppBussines:BRT");

                if (string.IsNullOrWhiteSpace(version) || string.IsNullOrWhiteSpace(bearerToken))
                {
                    return "Faltan valores de configuración de WhatsAppBusiness en appsettings.";
                }

                var url = $"https://graph.facebook.com/{version}/{mediaId}";
                if (!string.IsNullOrWhiteSpace(phoneNumberId))
                {
                    url += $"?phone_number_id={Uri.EscapeDataString(phoneNumberId)}";
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                request.Headers.Add("Authorization", $"Bearer {bearerToken}");

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return responseContent;
                }

                return responseContent;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string?> GetMediaUrl(string mediaId, string? phoneNumberId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mediaId))
                {
                    return "Media id is required.";
                }

                var version = GetSetting("WhatsAppBusiness:Version", "WhatsAppBussines:Version");
                var bearerToken = GetSetting("WhatsAppBusiness:BearerToken", "WhatsAppBussines:BRT");

                if (string.IsNullOrWhiteSpace(version) || string.IsNullOrWhiteSpace(bearerToken))
                {
                    return "Faltan valores de configuración de WhatsAppBusiness en appsettings.";
                }

                var url = $"https://graph.facebook.com/{version}/{mediaId}";
                if (!string.IsNullOrWhiteSpace(phoneNumberId))
                {
                    url += $"?phone_number_id={Uri.EscapeDataString(phoneNumberId)}";
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", $"Bearer {bearerToken}");

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return responseContent;
                }

                return responseContent;
            }
            catch (Exception e)
            {
                return e.Message;
            }
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

        private static string? TryExtractId(string responseContent)
        {
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return null;
            }

            try
            {
                var parsed = JsonConvert.DeserializeObject<IdResponse>(responseContent);
                return parsed?.id;
            }
            catch
            {
                return null;
            }
        }

        private sealed class IdResponse
        {
            public string? id { get; init; }
        }
    }

    
}
