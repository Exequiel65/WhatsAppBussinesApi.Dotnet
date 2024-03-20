namespace WhatsAppBussinesApi.Dotnet.Structure
{
    interface ITextMessage
    {
        BodyText Text { get; set; }
    }
    public class TextMessage : BaseMessage, ITextMessage
    {
        public BodyText Text { get; set; }

        public TextMessage() { }
        public TextMessage(string phoneNumber, BodyText text)
        {
            this.Text = text;
            this.to = phoneNumber;
        }
        public TextMessage(string phoneNumber, string text, bool previewUrl = false)
        {
            this.to = phoneNumber;
            this.Text = new BodyText(text, previewUrl);
        }
    }

    public class BodyText
    {
        public bool preview_url { get; set; } = false;
        public string body { get; set; }

        public BodyText()
        {

        }
        public BodyText(string body, bool previewUrl = false)
        {
            this.body = body.Trim();
            this.preview_url = previewUrl;
        }
    }

}
