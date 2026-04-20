namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public sealed class TextMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.text;
        public BodyText text { get; init; }

        public TextMessage() { }
        public TextMessage(string phoneNumber, BodyText text)
        {
            to = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }
        public TextMessage(string phoneNumber, string text, bool previewUrl = false)
        {
            to = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            this.text = new BodyText(text, previewUrl);
        }
    }

    public class BodyText
    {
        public bool preview_url { get; }
        public string body { get; }

        public BodyText(string body, bool previewUrl = false)
        {
            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Body cannot be empty", nameof(body));

            this.body = body.Trim();
            preview_url = previewUrl;
        }
    }
}
