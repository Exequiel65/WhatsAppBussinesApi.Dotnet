using WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class InteractiveHeaderBuilder
    {
        private BaseHeader? _header;

        public InteractiveHeaderBuilder Text(string text)
        {
            _header = new HeaderInteractive(text);
            return this;
        }

        public InteractiveHeaderBuilder ImageId(string id)
        {
            _header = new HeaderImageInteractive(id);
            return this;
        }

        public InteractiveHeaderBuilder ImageLink(string link)
        {
            _header = new HeaderImageInteractive(new Uri(link));
            return this;
        }

        public InteractiveHeaderBuilder DocumentId(string id)
        {
            _header = new HeaderDocumentInteractive(id);
            return this;
        }

        public InteractiveHeaderBuilder DocumentLink(string link)
        {
            _header = new HeaderDocumentInteractive(new Uri(link));
            return this;
        }

        public InteractiveHeaderBuilder VideoId(string id)
        {
            _header = new HeaderVideoInteractive(id);
            return this;
        }

        public InteractiveHeaderBuilder VideoLink(string link)
        {
            _header = new HeaderVideoInteractive(new Uri(link));
            return this;
        }

        public BaseHeader Build()
        {
            if (_header is null)
            {
                throw new InvalidOperationException("Header is required.");
            }

            return _header;
        }
    }
}
