using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class DocumentMessageBuilder : IMessageBuilder<DocumentMessage>
    {
        private string? _recipient;
        private BaseDocument? _document;
        private string? _caption;
        private string? _fileName;

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

        public DocumentMessageBuilder WithCaption(string caption)
        {
            _caption = caption;
            return this;
        }

        public DocumentMessageBuilder WithFileName(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public DocumentMessageBuilder WithDocumentLink(string link, string? fileName = null, string? caption = null)
        {
            _document = new DocumentComponentWithLink
            {
                link = link,
                filename = fileName,
                caption = caption
            };
            return this;
        }

        public DocumentMessageBuilder WithDocumentLink(string link, string providerName, string? fileName = null, string? caption = null)
        {
            _document = new DocumentComponentWithLinkProvider(link, providerName)
            {
                filename = fileName,
                caption = caption
            };
            return this;
        }

        public DocumentMessageBuilder WithDocumentId(string id, string? fileName = null, string? caption = null)
        {
            _document = new DocumentComponentWithId(id, fileName, caption);
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

            if (_caption is not null && _caption.Length > 1024)
            {
                throw new InvalidOperationException("Caption cannot exceed 1024 characters.");
            }

            if (_document is DocumentComponentWithId idDocument && string.IsNullOrWhiteSpace(idDocument.id))
            {
                throw new InvalidOperationException("Document id is required when using id payload.");
            }

            if (_document is DocumentComponentWithLink linkDocument && string.IsNullOrWhiteSpace(linkDocument.link))
            {
                throw new InvalidOperationException("Document link is required when using link payload.");
            }

            _document.filename ??= _fileName;
            _document.caption ??= _caption;

            return new DocumentMessage
            {
                to = _recipient,
                document = _document
            };
        }
    }
}
