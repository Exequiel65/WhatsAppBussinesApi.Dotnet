using System.Diagnostics.CodeAnalysis;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class StickerMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.sticker;
        public required BaseSticker sticker { get; init; }

        public StickerMessage()
        {
        }

        [SetsRequiredMembers]
        public StickerMessage(string to, string id)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            sticker = new StickerComponentWithId(id);
        }

        [SetsRequiredMembers]
        public StickerMessage(string to, string link, string providerName)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            sticker = new StickerComponent(link, providerName);
        }

        [SetsRequiredMembers]
        public StickerMessage(string to, BaseSticker sticker)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.sticker = sticker ?? throw new ArgumentNullException(nameof(sticker));
        }
    }

    public class BaseSticker { }

    public class StickerComponent : BaseSticker
    {
        public string link { get; set; }
        public BaseProvider provider { get; set; }

        public StickerComponent() { }

        public StickerComponent(string link, string providerName)
        {
            this.link = link;
            this.provider = new BaseProvider(providerName);
        }

        public StickerComponent(string link, BaseProvider provider)
        {
            this.link = link;
            this.provider = provider;
        }
    }

    public class StickerComponentWithId : BaseSticker
    {
        public string id { get; set; }

        public StickerComponentWithId()
        {

        }

        public StickerComponentWithId(string id)
        {
            this.id = id;
        }
    }

}
