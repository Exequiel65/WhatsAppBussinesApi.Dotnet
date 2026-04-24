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

            if (_buttons.Count == 0)
            {
                throw new InvalidOperationException("At least one button is required.");
            }

            var action = new ActionButtonsReply(_buttons);
            return new InteractiveButtonsMessage
            {
                to = _recipient,
                interactive = new InteractiveComponent(_body, action, _header, _footer)
            };
        }
    }
}
