namespace WhatsAppBussinesApi.Dotnet.Client
{
    internal static class WhatsAppMediaValidator
    {
        private const long OneMb = 1024 * 1024;
        private const long OneKb = 1024;

        private static readonly Dictionary<string, long> AllowedMimeTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            ["audio/aac"] = 16 * OneMb,
            ["audio/amr"] = 16 * OneMb,
            ["audio/mpeg"] = 16 * OneMb,
            ["audio/mp4"] = 16 * OneMb,
            ["audio/ogg"] = 16 * OneMb,

            ["text/plain"] = 100 * OneMb,
            ["application/vnd.ms-excel"] = 100 * OneMb,
            ["application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"] = 100 * OneMb,
            ["application/msword"] = 100 * OneMb,
            ["application/vnd.openxmlformats-officedocument.wordprocessingml.document"] = 100 * OneMb,
            ["application/vnd.ms-powerpoint"] = 100 * OneMb,
            ["application/vnd.openxmlformats-officedocument.presentationml.presentation"] = 100 * OneMb,
            ["application/pdf"] = 100 * OneMb,

            ["image/jpeg"] = 5 * OneMb,
            ["image/png"] = 5 * OneMb,

            ["image/webp"] = 500 * OneKb,

            ["video/3gpp"] = 16 * OneMb,
            ["video/mp4"] = 16 * OneMb
        };

        public static string? Validate(string mimeType, long fileSizeBytes)
        {
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                return "MIME type is required.";
            }

            if (!AllowedMimeTypes.TryGetValue(mimeType, out var maxBytes))
            {
                return $"MIME type '{mimeType}' is not supported by WhatsApp media upload.";
            }

            if (fileSizeBytes <= 0)
            {
                return "File content is empty.";
            }

            if (fileSizeBytes > maxBytes)
            {
                return $"File size exceeds allowed limit for '{mimeType}'. Max allowed: {maxBytes} bytes.";
            }

            return null;
        }
    }
}
