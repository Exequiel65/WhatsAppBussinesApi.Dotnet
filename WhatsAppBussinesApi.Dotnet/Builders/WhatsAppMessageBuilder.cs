using WhatsAppBussinesApi.Dotnet.Builders.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders
{
    public static class WhatsAppMessageBuilder
    {
        public static TextMessagesBuilder Text => new();

        public static TemplateTextMessageBuilder Template => new();
    }

    public sealed class TextMessagesBuilder
    {
        public TextMessageBuilder Text() => new();

        public AudioMessageBuilder Audio() => new();

        public ImageMessageBuilder Image() => new();

        public DocumentMessageBuilder Document() => new();

        public LocationMessageBuilder Location() => new();

        public StickerMessageBuilder Sticker() => new();

        public InteractiveButtonsMessageBuilder InteractiveButtons() => new();

        public InteractiveListMessageBuilder InteractiveList() => new();
    }
}
