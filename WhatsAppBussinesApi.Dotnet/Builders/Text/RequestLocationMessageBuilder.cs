using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class RequestLocationMessageBuilder : IMessageBuilder<RequestLocation>
    {
        private string? _recipient;
        private string? _bodyText;

        public RequestLocationMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public RequestLocationMessageBuilder WithBody(string bodyText)
        {
            _bodyText = bodyText;
            return this;
        }

        public RequestLocation Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (string.IsNullOrWhiteSpace(_bodyText))
            {
                throw new InvalidOperationException("Body text is required.");
            }

            if (_bodyText.Length > 1024)
            {
                throw new InvalidOperationException("Body text cannot exceed 1024 characters.");
            }

            return new RequestLocation
            {
                to = _recipient,
                interactive = new RequestLocationComponent(_bodyText)
            };
        }
    }
}
