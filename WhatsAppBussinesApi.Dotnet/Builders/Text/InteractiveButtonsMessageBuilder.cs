using WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class InteractiveButtonsMessageBuilder : IMessageBuilder<InteractiveButtonsMessage>
    {
        private string? _recipient;
        private TextBody? _body;
        private BaseHeader? _header;
        private FooterText? _footer;
        private readonly List<ButtonAction> _buttons = [];

        public InteractiveButtonsMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public InteractiveButtonsMessageBuilder WithBody(string text)
        {
            _body = new TextBody(text);
            return this;
        }

        public InteractiveButtonsMessageBuilder WithBody(TextBody body)
        {
            _body = body;
            return this;
        }

        public InteractiveButtonsMessageBuilder WithHeader(BaseHeader header)
        {
            _header = header;
            return this;
        }

        public InteractiveButtonsMessageBuilder WithHeader(Action<InteractiveHeaderBuilder> configure)
        {
            var builder = new InteractiveHeaderBuilder();
            configure(builder);
            _header = builder.Build();
            return this;
        }

        public InteractiveButtonsMessageBuilder WithFooter(string text)
        {
            _footer = new FooterText(text);
            return this;
        }

        public InteractiveButtonsMessageBuilder WithFooter(FooterText footer)
        {
            _footer = footer;
            return this;
        }

        public InteractiveButtonsMessageBuilder AddReplyButton(string id, string title)
        {
            _buttons.Add(new ButtonAction(id, title));
            return this;
        }

        public InteractiveButtonsMessageBuilder AddButton(ButtonAction button)
        {
            _buttons.Add(button);
            return this;
        }

        public InteractiveButtonsMessage Build()
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

            if (_buttons.Count == 0)
            {
                throw new InvalidOperationException("At least one button is required.");
            }

            if (_buttons.Count > 3)
            {
                throw new InvalidOperationException("At most three buttons are allowed.");
            }

            foreach (var button in _buttons)
            {
                if (button.reply is null || string.IsNullOrWhiteSpace(button.reply.id) || string.IsNullOrWhiteSpace(button.reply.title))
                {
                    throw new InvalidOperationException("Each button requires an id and title.");
                }

                if (button.reply.id.Length > 256)
                {
                    throw new InvalidOperationException("Button id cannot exceed 256 characters.");
                }

                if (button.reply.title.Length > 20)
                {
                    throw new InvalidOperationException("Button title cannot exceed 20 characters.");
                }
            }

            if (_footer is not null && _footer.text?.Length > 60)
            {
                throw new InvalidOperationException("Footer text cannot exceed 60 characters.");
            }

            if (_header is HeaderInteractive headerText && headerText.text?.Length > 60)
            {
                throw new InvalidOperationException("Header text cannot exceed 60 characters.");
            }

            return new InteractiveButtonsMessage
            {
                to = _recipient,
                interactive = new InteractiveComponent(_body, new ActionButtonsReply(_buttons), _header, _footer)
            };
        }
    }
}
