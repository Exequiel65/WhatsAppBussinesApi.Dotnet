using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class ImageMessageBuilder : IMessageBuilder<ImageMessage>
    {
        private string? _recipient;
        private BaseImage? _image;

        public ImageMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public ImageMessageBuilder WithImage(BaseImage image)
        {
            _image = image;
            return this;
        }

        public ImageMessageBuilder WithImageLink(string link)
        {
            _image = new ImageComponent(link);
            return this;
        }

        public ImageMessageBuilder WithImageLink(string link, string providerName)
        {
            _image = new ImageComponentWithProvider(link, providerName);
            return this;
        }

        public ImageMessageBuilder WithImageId(string id)
        {
            _image = new ImageComponentWithId(id);
            return this;
        }

        public ImageMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_image is null)
            {
                throw new InvalidOperationException("Image payload is required.");
            }

            return new ImageMessage
            {
                to = _recipient,
                image = _image
            };
        }
    }
}
