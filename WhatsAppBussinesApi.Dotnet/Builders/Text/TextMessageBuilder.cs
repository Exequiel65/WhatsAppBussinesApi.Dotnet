using WhatsAppBussinesApi.Dotnet.Structure;
using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class TextMessageBuilder : IMessageBuilder<TextMessage>
    {
        private string _to;
        private BodyText _text;

        public TextMessageBuilder To(string phoneNumber)
        {
            _to = phoneNumber;
            return this;
        }

        public TextMessageBuilder WithBody(string text, bool previewUrl = false)
        {
            _text = new BodyText(text, previewUrl);
            return this;
        }

        public TextMessageBuilder WithBody(BodyText body)
        {
            _text = body;
            return this;
        }

        public TextMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_to))
                throw new InvalidOperationException("Recipient phone number is required.");

            if (_text is null)
                throw new InvalidOperationException("Message body is required.");

            return new TextMessage
            {
                to = _to,
                text = _text
            };
        }
    }
}
