using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class VideoMessageBuilder : IMessageBuilder<VideoMessage>
    {
        private string? _recipient;
        private BaseVideo? _video;
        private string? _caption;

        public VideoMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public VideoMessageBuilder WithVideo(BaseVideo video)
        {
            _video = video;
            return this;
        }

        public VideoMessageBuilder WithVideoId(string id)
        {
            _video = new VideoComponentWithId(id);
            return this;
        }

        public VideoMessageBuilder WithVideoLink(string link)
        {
            _video = new VideoComponentWithLink(link);
            return this;
        }

        public VideoMessageBuilder WithCaption(string caption)
        {
            _caption = caption;
            return this;
        }

        public VideoMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_video is null)
            {
                throw new InvalidOperationException("Video payload is required.");
            }

            if (_caption is not null && _caption.Length > 1024)
            {
                throw new InvalidOperationException("Caption cannot exceed 1024 characters.");
            }

            if (_video is VideoComponentWithId idVideo && string.IsNullOrWhiteSpace(idVideo.id))
            {
                throw new InvalidOperationException("Video id is required when using id payload.");
            }

            if (_video is VideoComponentWithLink linkVideo && string.IsNullOrWhiteSpace(linkVideo.link))
            {
                throw new InvalidOperationException("Video link is required when using link payload.");
            }

            _video.caption ??= _caption;

            return new VideoMessage
            {
                to = _recipient,
                video = _video
            };
        }
    }
}
