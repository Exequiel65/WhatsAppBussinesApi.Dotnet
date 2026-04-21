using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class StickerMessageBuilder : IMessageBuilder<StickerMessage>
    {
        private string? _recipient;
        private BaseSticker? _sticker;

        public StickerMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public StickerMessageBuilder WithSticker(BaseSticker sticker)
        {
            _sticker = sticker;
            return this;
        }

        public StickerMessageBuilder WithStickerId(string id)
        {
            _sticker = new StickerComponentWithId(id);
            return this;
        }

        public StickerMessageBuilder WithStickerLink(string link, string providerName)
        {
            _sticker = new StickerComponent(link, providerName);
            return this;
        }

        public StickerMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_sticker is null)
            {
                throw new InvalidOperationException("Sticker payload is required.");
            }

            return new StickerMessage
            {
                to = _recipient,
                sticker = _sticker
            };
        }
    }
}
