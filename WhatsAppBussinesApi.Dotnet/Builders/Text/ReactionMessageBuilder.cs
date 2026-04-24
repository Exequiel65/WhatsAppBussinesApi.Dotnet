using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class ReactionMessageBuilder : IMessageBuilder<ReactionMessage>
    {
        private string? _recipient;
        private string? _messageId;
        private string? _emoji;

        public ReactionMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public ReactionMessageBuilder WithMessageId(string messageId)
        {
            _messageId = messageId;
            return this;
        }

        public ReactionMessageBuilder WithEmoji(string emoji)
        {
            _emoji = emoji;
            return this;
        }

        public ReactionMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (string.IsNullOrWhiteSpace(_messageId))
            {
                throw new InvalidOperationException("Message id is required.");
            }

            if (string.IsNullOrWhiteSpace(_emoji))
            {
                throw new InvalidOperationException("Emoji is required.");
            }

            return new ReactionMessage
            {
                to = _recipient,
                reaction = new ReactionComponent(_messageId, _emoji)
            };
        }
    }
}
