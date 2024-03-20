namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    interface ITextMessage
    {
        BodyText text { get; set; }
    }
    public class TextMessage : BaseMessage, ITextMessage
    {
        public BodyText text { get; set; }

        public TextMessage() { }
        public TextMessage(string phoneNumber, BodyText text)
        {
            this.text = text;
            to = phoneNumber;
        }
        public TextMessage(string phoneNumber, string text, bool previewUrl = false)
        {
            to = phoneNumber;
            this.text = new BodyText(text, previewUrl);
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
            preview_url = previewUrl;
        }
    }

}
