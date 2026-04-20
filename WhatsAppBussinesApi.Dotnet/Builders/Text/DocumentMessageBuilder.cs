using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class DocumentMessageBuilder : IMessageBuilder<DocumentMessage>
    {
        private string? _recipient;
        private BaseDocument? _document;

        public DocumentMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public DocumentMessageBuilder WithDocument(BaseDocument document)
        {
            _document = document;
            return this;
        }

        public DocumentMessageBuilder WithDocumentLink(string link, string? fileName = null)
        {
            _document = new DocumentComponentWithLink
            {
                link = link,
                filename = fileName
            };
            return this;
        }

        public DocumentMessageBuilder WithDocumentLink(string link, string providerName, string? fileName = null)
        {
            _document = new DocumentComponentWithLinkProvider(link, providerName)
            {
                filename = fileName
            };
            return this;
        }

        public DocumentMessageBuilder WithDocumentId(string id, string fileName)
        {
            _document = new DocumentComponentWithId(id, fileName);
            return this;
        }

        public DocumentMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_document is null)
            {
                throw new InvalidOperationException("Document payload is required.");
            }

            return new DocumentMessage
            {
                to = _recipient,
                document = _document
            };
        }
    }
}
