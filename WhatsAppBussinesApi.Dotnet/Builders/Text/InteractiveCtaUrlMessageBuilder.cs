using WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class InteractiveCtaUrlMessageBuilder : IMessageBuilder<InteractiveButtonUrlMessage>
    {
        private string? _recipient;
        private TextBody? _body;
        private string? _displayText;
        private string? _url;
        private BaseHeader? _header;
        private FooterText? _footer;

        public InteractiveCtaUrlMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithBody(string text)
        {
            _body = new TextBody(text);
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithBody(TextBody body)
        {
            _body = body;
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithDisplayText(string displayText)
        {
            _displayText = displayText;
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithHeader(BaseHeader header)
        {
            _header = header;
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithHeader(Action<InteractiveHeaderBuilder> configure)
        {
            var builder = new InteractiveHeaderBuilder();
            configure(builder);
            _header = builder.Build();
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithFooter(string text)
        {
            _footer = new FooterText(text);
            return this;
        }

        public InteractiveCtaUrlMessageBuilder WithFooter(FooterText footer)
        {
            _footer = footer;
            return this;
        }

        public InteractiveButtonUrlMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_body is null || string.IsNullOrWhiteSpace(_body.text))
            {
                throw new InvalidOperationException("Body text is required.");
            }

            if (_body.text.Length > 1024)
            {
                throw new InvalidOperationException("Body text cannot exceed 1024 characters.");
            }

            if (string.IsNullOrWhiteSpace(_displayText))
            {
                throw new InvalidOperationException("Display text is required.");
            }

            if (_displayText.Length > 20)
            {
                throw new InvalidOperationException("Display text cannot exceed 20 characters.");
            }

            if (string.IsNullOrWhiteSpace(_url))
            {
                throw new InvalidOperationException("URL is required.");
            }

            if (_footer is not null && _footer.text?.Length > 60)
            {
                throw new InvalidOperationException("Footer text cannot exceed 60 characters.");
            }

            if (_header is HeaderInteractive textHeader && textHeader.text?.Length > 60)
            {
                throw new InvalidOperationException("Header text cannot exceed 60 characters.");
            }

            return new InteractiveButtonUrlMessage
            {
                to = _recipient,
                interactive = new InteractiveCtaUrlComponent(
                    _body,
                    new CtaUrlAction(_displayText, _url),
                    _header,
                    _footer)
            };
        }
    }
}
