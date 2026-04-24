namespace WhatsAppBussinesApi.Dotnet.Media
{
    public sealed class MediaDownloadResult
    {
        public bool IsSuccess { get; init; }
        public byte[]? Content { get; init; }
        public string? MimeType { get; init; }
        public string? FileName { get; init; }
        public string? Error { get; init; }
        public int? StatusCode { get; init; }

        public static MediaDownloadResult Success(byte[] content, string mimeType, string fileName)
        {
            return new MediaDownloadResult
            {
                IsSuccess = true,
                Content = content,
                MimeType = mimeType,
                FileName = fileName
            };
        }

        public static MediaDownloadResult Failed(string error, int? statusCode = null)
        {
            return new MediaDownloadResult
            {
                IsSuccess = false,
                Error = error,
                StatusCode = statusCode
            };
        }
    }
}
