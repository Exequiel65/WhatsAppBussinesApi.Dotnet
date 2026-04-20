using WhatsAppBussinesApi.Dotnet.Structure.Templates;
using WhatsAppBussinesApi.Dotnet.Structure;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class TemplateTextMessageBuilder : IMessageBuilder<TemplateTextMessage>
    {
        private string? _to;
        private string? _templateName;
        private string _languageCode = "en";
        private readonly List<BaseParameters> _bodyParameters = [];
        private BaseParameters? _headerParameter;
        private readonly List<BaseButtonComponent> _interactionComponents = [];

        public TemplateTextMessageBuilder To(string phoneNumber)
        {
            _to = phoneNumber;
            return this;
        }

        public TemplateTextMessageBuilder WithTemplateName(string templateName)
        {
            _templateName = templateName;
            return this;
        }

        public TemplateTextMessageBuilder WithLanguage(string languageCode)
        {
            _languageCode = languageCode;
            return this;
        }

        public TemplateTextMessageBuilder AddBodyParameter(BaseParameters parameter)
        {
            _bodyParameters.Add(parameter);
            return this;
        }

        public TemplateTextMessageBuilder AddBodyTextParameter(string text)
        {
            return AddBodyParameter(new ParameterText(text));
        }

        public TemplateTextMessageBuilder AddBodyCurrencyParameter(string fallbackValue, string code, decimal amount)
        {
            return AddBodyParameter(new ParameterCurrency(fallbackValue, code, amount));
        }

        public TemplateTextMessageBuilder AddBodyDateTimeParameter(DateTime dateTime, string languageCode)
        {
            return AddBodyParameter(new ParameterDateTime(dateTime, languageCode));
        }

        public TemplateTextMessageBuilder AddHeader(BaseParameters headerParameter)
        {
            _headerParameter = headerParameter ?? throw new ArgumentNullException(nameof(headerParameter));
            return this;
        }

        public TemplateTextMessageBuilder AddHeaderText(string text)
        {
            return AddHeader(new HeaderTextParameter(text));
        }

        public TemplateTextMessageBuilder AddHeaderImage(Uri imageUrl)
        {
            return AddHeader(new HeaderImageParameter(imageUrl.ToString()));
        }

        public TemplateTextMessageBuilder AddHeaderLocation(LocationParameter location)
        {
            return AddHeader(new HeaderLocationParameter(location));
        }

        public TemplateTextMessageBuilder AddInteraction(BaseButtonComponent interaction)
        {
            _interactionComponents.Add(interaction ?? throw new ArgumentNullException(nameof(interaction)));
            return this;
        }

        public TemplateTextMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_to))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (string.IsNullOrWhiteSpace(_templateName))
            {
                throw new InvalidOperationException("Template name is required.");
            }

            if (string.IsNullOrWhiteSpace(_languageCode))
            {
                throw new InvalidOperationException("Template language is required.");
            }

            var template = new TemplateData
            {
                name = _templateName,
                language = new LanguageCode(_languageCode),
                components =
                [
                    new Component
                    {
                        type = "body",
                        parameters = [.._bodyParameters]
                    }
                ]
            };

            if (_headerParameter is not null)
            {
                template.components.Add(new Component
                {
                    type = "header",
                    parameters = [_headerParameter]
                });
            }

            if (_interactionComponents.Count > 0)
            {
                template.components.AddRange(_interactionComponents);
            }

            return new TemplateTextMessage
            {
                to = _to,
                template = template
            };
        }
    }
}
