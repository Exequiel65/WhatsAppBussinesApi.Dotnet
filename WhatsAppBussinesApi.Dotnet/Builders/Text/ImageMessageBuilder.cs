using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class ImageMessageBuilder : IMessageBuilder<ImageMessage>
    {
        private string? _recipient;
        private BaseImage? _image;
        private string? _caption;

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

        public ImageMessageBuilder WithCaption(string caption)
        {
            _caption = caption;
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

            if (_caption is not null && _caption.Length > 1024)
            {
                throw new InvalidOperationException("Caption cannot exceed 1024 characters.");
            }

            if (_image is ImageComponentWithId idImage && string.IsNullOrWhiteSpace(idImage.id))
            {
                throw new InvalidOperationException("Image id is required when using id payload.");
            }

            if (_image is ImageComponent linkImage && string.IsNullOrWhiteSpace(linkImage.link))
            {
                throw new InvalidOperationException("Image link is required when using link payload.");
            }

            _image.caption ??= _caption;

            return new ImageMessage
            {
                to = _recipient,
                image = _image
            };
        }
    }
}
