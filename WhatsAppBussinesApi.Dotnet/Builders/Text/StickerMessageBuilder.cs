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

        public StickerMessageBuilder WithStickerLink(string link)
        {
            _sticker = new StickerComponentWithLink(link);
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

            if (_sticker is StickerComponentWithId idSticker && string.IsNullOrWhiteSpace(idSticker.id))
            {
                throw new InvalidOperationException("Sticker id is required when using id payload.");
            }

            if (_sticker is StickerComponentWithLink linkSticker && string.IsNullOrWhiteSpace(linkSticker.link))
            {
                throw new InvalidOperationException("Sticker link is required when using link payload.");
            }

            return new StickerMessage
            {
                to = _recipient,
                sticker = _sticker
            };
        }
    }
}
