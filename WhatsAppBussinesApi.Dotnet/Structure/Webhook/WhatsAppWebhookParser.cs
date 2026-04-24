namespace WhatsAppBussinesApi.Dotnet.Structure.Webhook
{
    public static class WhatsAppWebhookParser
    {
        public static IReadOnlyList<IncomingTextMessage> ExtractIncomingTextMessages(WhatsAppWebhook webhook)
        {
            if (webhook is null)
            {
                throw new ArgumentNullException(nameof(webhook));
            }

            var result = new List<IncomingTextMessage>();

            foreach (var entry in webhook.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    if (!string.Equals(change.Field, "messages", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var value = change.Value;
                    if (value is null)
                    {
                        continue;
                    }

                    var contactsByWaId = value.Contacts
                        .Where(c => !string.IsNullOrWhiteSpace(c.WaId))
                        .ToDictionary(c => c.WaId!, c => c);

                    foreach (var message in value.Messages)
                    {
                        if (!string.Equals(message.Type, "text", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        contactsByWaId.TryGetValue(message.From ?? string.Empty, out var contact);

                        result.Add(new IncomingTextMessage
                        {
                            BusinessPhoneNumberId = value.Metadata?.PhoneNumberId,
                            BusinessDisplayPhoneNumber = value.Metadata?.DisplayPhoneNumber,
                            From = message.From,
                            FromUserId = message.FromUserId,
                            ContactName = contact?.Profile?.Name,
                            ContactWaId = contact?.WaId,
                            ContactUserId = contact?.UserId,
                            ContactIdentityKeyHash = contact?.IdentityKeyHash,
                            MessageId = message.Id,
                            MessageType = message.Type,
                            Body = message.Text?.Body,
                            ContextFrom = message.Context?.From,
                            ContextMessageId = message.Context?.Id,
                            ContextForwarded = message.Context?.Forwarded,
                            ContextFrequentlyForwarded = message.Context?.FrequentlyForwarded,
                            ReferredProductCatalogId = message.Context?.ReferredProduct?.CatalogId,
                            ReferredProductRetailerId = message.Context?.ReferredProduct?.ProductRetailerId,
                            ReferralSourceUrl = message.Referral?.SourceUrl,
                            ReferralSourceId = message.Referral?.SourceId,
                            ReferralSourceType = message.Referral?.SourceType,
                            ReferralBody = message.Referral?.Body,
                            ReferralHeadline = message.Referral?.Headline,
                            ReferralMediaType = message.Referral?.MediaType,
                            ReferralImageUrl = message.Referral?.ImageUrl,
                            ReferralVideoUrl = message.Referral?.VideoUrl,
                            ReferralThumbnailUrl = message.Referral?.ThumbnailUrl,
                            ReferralClickId = message.Referral?.CtwaClid,
                            ReferralWelcomeText = message.Referral?.WelcomeMessage?.Text,
                            Timestamp = ParseUnixTimestamp(message.Timestamp)
                        });
                    }
                }
            }

            return result;
        }

        public static IReadOnlyList<IncomingLocationMessage> ExtractIncomingLocationMessages(WhatsAppWebhook webhook)
        {
            if (webhook is null)
            {
                throw new ArgumentNullException(nameof(webhook));
            }

            var result = new List<IncomingLocationMessage>();

            foreach (var entry in webhook.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    if (!string.Equals(change.Field, "messages", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var value = change.Value;
                    if (value is null)
                    {
                        continue;
                    }

                    var contactsByWaId = value.Contacts
                        .Where(c => !string.IsNullOrWhiteSpace(c.WaId))
                        .ToDictionary(c => c.WaId!, c => c);

                    foreach (var message in value.Messages)
                    {
                        if (!string.Equals(message.Type, "location", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        contactsByWaId.TryGetValue(message.From ?? string.Empty, out var contact);

                        result.Add(new IncomingLocationMessage
                        {
                            BusinessPhoneNumberId = value.Metadata?.PhoneNumberId,
                            BusinessDisplayPhoneNumber = value.Metadata?.DisplayPhoneNumber,
                            From = message.From,
                            ContactName = contact?.Profile?.Name,
                            ContactWaId = contact?.WaId,
                            MessageId = message.Id,
                            ContextFrom = message.Context?.From,
                            ContextMessageId = message.Context?.Id,
                            Address = message.Location?.Address,
                            Name = message.Location?.Name,
                            Latitude = message.Location?.Latitude,
                            Longitude = message.Location?.Longitude,
                            Timestamp = ParseUnixTimestamp(message.Timestamp)
                        });
                    }
                }
            }

            return result;
        }

        public static IReadOnlyList<OutgoingStatusMessage> ExtractStatusMessages(WhatsAppWebhook webhook)
        {
            if (webhook is null)
            {
                throw new ArgumentNullException(nameof(webhook));
            }

            var result = new List<OutgoingStatusMessage>();

            foreach (var entry in webhook.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    if (!string.Equals(change.Field, "messages", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var value = change.Value;
                    if (value is null)
                    {
                        continue;
                    }

                    foreach (var status in value.Statuses)
                    {
                        result.Add(new OutgoingStatusMessage
                        {
                            BusinessPhoneNumberId = value.Metadata?.PhoneNumberId,
                            BusinessDisplayPhoneNumber = value.Metadata?.DisplayPhoneNumber,
                            MessageId = status.Id,
                            Status = status.Status,
                            RecipientId = status.RecipientId,
                            RecipientUserId = status.RecipientUserId,
                            Timestamp = ParseUnixTimestamp(status.Timestamp),
                            Billable = status.Pricing?.Billable,
                            PricingModel = status.Pricing?.PricingModel,
                            PricingCategory = status.Pricing?.Category,
                            PricingType = status.Pricing?.Type
                        });
                    }
                }
            }

            return result;
        }

        private static DateTimeOffset? ParseUnixTimestamp(string? unixTimestamp)
        {
            if (!long.TryParse(unixTimestamp, out var seconds))
            {
                return null;
            }

            return DateTimeOffset.FromUnixTimeSeconds(seconds);
        }
    }

    public sealed class IncomingTextMessage
    {
        public string? BusinessPhoneNumberId { get; init; }
        public string? BusinessDisplayPhoneNumber { get; init; }
        public string? From { get; init; }
        public string? FromUserId { get; init; }
        public string? ContactWaId { get; init; }
        public string? ContactUserId { get; init; }
        public string? ContactIdentityKeyHash { get; init; }
        public string? ContactName { get; init; }
        public string? MessageId { get; init; }
        public string? MessageType { get; init; }
        public string? Body { get; init; }
        public string? ContextFrom { get; init; }
        public string? ContextMessageId { get; init; }
        public bool? ContextForwarded { get; init; }
        public bool? ContextFrequentlyForwarded { get; init; }
        public string? ReferredProductCatalogId { get; init; }
        public string? ReferredProductRetailerId { get; init; }
        public string? ReferralSourceUrl { get; init; }
        public string? ReferralSourceId { get; init; }
        public string? ReferralSourceType { get; init; }
        public string? ReferralBody { get; init; }
        public string? ReferralHeadline { get; init; }
        public string? ReferralMediaType { get; init; }
        public string? ReferralImageUrl { get; init; }
        public string? ReferralVideoUrl { get; init; }
        public string? ReferralThumbnailUrl { get; init; }
        public string? ReferralClickId { get; init; }
        public string? ReferralWelcomeText { get; init; }
        public DateTimeOffset? Timestamp { get; init; }
    }

    public sealed class OutgoingStatusMessage
    {
        public string? BusinessPhoneNumberId { get; init; }
        public string? BusinessDisplayPhoneNumber { get; init; }
        public string? MessageId { get; init; }
        public string? Status { get; init; }
        public string? RecipientId { get; init; }
        public string? RecipientUserId { get; init; }
        public DateTimeOffset? Timestamp { get; init; }
        public bool? Billable { get; init; }
        public string? PricingModel { get; init; }
        public string? PricingCategory { get; init; }
        public string? PricingType { get; init; }
    }

    public sealed class IncomingLocationMessage
    {
        public string? BusinessPhoneNumberId { get; init; }
        public string? BusinessDisplayPhoneNumber { get; init; }
        public string? From { get; init; }
        public string? ContactWaId { get; init; }
        public string? ContactName { get; init; }
        public string? MessageId { get; init; }
        public string? ContextFrom { get; init; }
        public string? ContextMessageId { get; init; }
        public string? Address { get; init; }
        public string? Name { get; init; }
        public decimal? Latitude { get; init; }
        public decimal? Longitude { get; init; }
        public DateTimeOffset? Timestamp { get; init; }
    }
}
