using System.Text.Json.Serialization;

namespace WhatsAppBussinesApi.Dotnet.Structure.Webhook
{
    public sealed class WhatsAppWebhook
    {
        [JsonPropertyName("object")]
        public string? Object { get; init; }

        [JsonPropertyName("entry")]
        public List<WhatsAppWebhookEntry> Entry { get; init; } = [];
    }

    public sealed class WhatsAppWebhookEntry
    {
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("changes")]
        public List<WhatsAppWebhookChange> Changes { get; init; } = [];
    }

    public sealed class WhatsAppWebhookChange
    {
        [JsonPropertyName("field")]
        public string? Field { get; init; }

        [JsonPropertyName("value")]
        public WhatsAppWebhookValue? Value { get; init; }
    }

    public sealed class WhatsAppWebhookValue
    {
        [JsonPropertyName("messaging_product")]
        public string? MessagingProduct { get; init; }

        [JsonPropertyName("metadata")]
        public WhatsAppWebhookMetadata? Metadata { get; init; }

        [JsonPropertyName("contacts")]
        public List<WhatsAppWebhookContact> Contacts { get; init; } = [];

        [JsonPropertyName("messages")]
        public List<WhatsAppWebhookMessage> Messages { get; init; } = [];

        [JsonPropertyName("statuses")]
        public List<WhatsAppWebhookStatus> Statuses { get; init; } = [];
    }

    public sealed class WhatsAppWebhookMetadata
    {
        [JsonPropertyName("display_phone_number")]
        public string? DisplayPhoneNumber { get; init; }

        [JsonPropertyName("phone_number_id")]
        public string? PhoneNumberId { get; init; }
    }

    public sealed class WhatsAppWebhookContact
    {
        [JsonPropertyName("wa_id")]
        public string? WaId { get; init; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; init; }

        [JsonPropertyName("identity_key_hash")]
        public string? IdentityKeyHash { get; init; }

        [JsonPropertyName("profile")]
        public WhatsAppWebhookProfile? Profile { get; init; }
    }

    public sealed class WhatsAppWebhookProfile
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }
    }

    public sealed class WhatsAppWebhookMessage
    {
        [JsonPropertyName("from")]
        public string? From { get; init; }

        [JsonPropertyName("from_user_id")]
        public string? FromUserId { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("text")]
        public WhatsAppWebhookText? Text { get; init; }

        [JsonPropertyName("context")]
        public WhatsAppWebhookMessageContext? Context { get; init; }

        [JsonPropertyName("referral")]
        public WhatsAppWebhookReferral? Referral { get; init; }

        [JsonPropertyName("location")]
        public WhatsAppWebhookLocation? Location { get; init; }
    }

    public sealed class WhatsAppWebhookLocation
    {
        [JsonPropertyName("address")]
        public string? Address { get; init; }

        [JsonPropertyName("latitude")]
        public decimal? Latitude { get; init; }

        [JsonPropertyName("longitude")]
        public decimal? Longitude { get; init; }

        [JsonPropertyName("name")]
        public string? Name { get; init; }
    }

    public sealed class WhatsAppWebhookMessageContext
    {
        [JsonPropertyName("from")]
        public string? From { get; init; }

        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("forwarded")]
        public bool? Forwarded { get; init; }

        [JsonPropertyName("frequently_forwarded")]
        public bool? FrequentlyForwarded { get; init; }

        [JsonPropertyName("referred_product")]
        public WhatsAppWebhookReferredProduct? ReferredProduct { get; init; }
    }

    public sealed class WhatsAppWebhookReferredProduct
    {
        [JsonPropertyName("catalog_id")]
        public string? CatalogId { get; init; }

        [JsonPropertyName("product_retailer_id")]
        public string? ProductRetailerId { get; init; }
    }

    public sealed class WhatsAppWebhookReferral
    {
        [JsonPropertyName("source_url")]
        public string? SourceUrl { get; init; }

        [JsonPropertyName("source_id")]
        public string? SourceId { get; init; }

        [JsonPropertyName("source_type")]
        public string? SourceType { get; init; }

        [JsonPropertyName("body")]
        public string? Body { get; init; }

        [JsonPropertyName("headline")]
        public string? Headline { get; init; }

        [JsonPropertyName("media_type")]
        public string? MediaType { get; init; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; init; }

        [JsonPropertyName("video_url")]
        public string? VideoUrl { get; init; }

        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; init; }

        [JsonPropertyName("ctwa_clid")]
        public string? CtwaClid { get; init; }

        [JsonPropertyName("welcome_message")]
        public WhatsAppWebhookWelcomeMessage? WelcomeMessage { get; init; }
    }

    public sealed class WhatsAppWebhookWelcomeMessage
    {
        [JsonPropertyName("text")]
        public string? Text { get; init; }
    }

    public sealed class WhatsAppWebhookStatus
    {
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("status")]
        public string? Status { get; init; }

        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; init; }

        [JsonPropertyName("recipient_id")]
        public string? RecipientId { get; init; }

        [JsonPropertyName("recipient_user_id")]
        public string? RecipientUserId { get; init; }

        [JsonPropertyName("pricing")]
        public WhatsAppWebhookPricing? Pricing { get; init; }
    }

    public sealed class WhatsAppWebhookPricing
    {
        [JsonPropertyName("billable")]
        public bool? Billable { get; init; }

        [JsonPropertyName("pricing_model")]
        public string? PricingModel { get; init; }

        [JsonPropertyName("category")]
        public string? Category { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }
    }

    public sealed class WhatsAppWebhookText
    {
        [JsonPropertyName("body")]
        public string? Body { get; init; }
    }
}
