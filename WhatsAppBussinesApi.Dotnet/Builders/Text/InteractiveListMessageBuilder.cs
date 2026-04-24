using WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class InteractiveListMessageBuilder : IMessageBuilder<InteractiveListMessage>
    {
        private string? _recipient;
        private string? _bodyText;
        private string? _buttonLabel;
        private readonly List<SectionList> _sections = [];
        private BaseHeader? _header;
        private FooterText? _footer;

        public InteractiveListMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public InteractiveListMessageBuilder WithBody(string text)
        {
            _bodyText = text;
            return this;
        }

        public InteractiveListMessageBuilder WithButtonLabel(string label)
        {
            _buttonLabel = label;
            return this;
        }

        public InteractiveListMessageBuilder AddSection(SectionList section)
        {
            _sections.Add(section);
            return this;
        }

        public InteractiveListMessageBuilder AddSection(string title, IEnumerable<Row> rows)
        {
            _sections.Add(new SectionList(title, rows.ToList()));
            return this;
        }

        public InteractiveListMessageBuilder WithHeader(BaseHeader header)
        {
            _header = header;
            return this;
        }

        public InteractiveListMessageBuilder WithFooter(string text)
        {
            _footer = new FooterText(text);
            return this;
        }

        public InteractiveListMessageBuilder WithFooter(FooterText footer)
        {
            _footer = footer;
            return this;
        }

        public InteractiveListMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (string.IsNullOrWhiteSpace(_bodyText))
            {
                throw new InvalidOperationException("Body text is required.");
            }

            if (string.IsNullOrWhiteSpace(_buttonLabel))
            {
                throw new InvalidOperationException("Button label is required.");
            }

            if (_sections.Count == 0)
            {
                throw new InvalidOperationException("At least one section is required.");
            }

            return new InteractiveListMessage
            {
                to = _recipient,
                interactive = new ListInteractiveComponent(_bodyText, _buttonLabel, _sections, _header, _footer)
            };
        }
    }
}
