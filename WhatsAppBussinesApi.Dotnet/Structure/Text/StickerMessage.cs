namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class StickerMessage : BaseMessage
    {
        public TypeMessage type { get; set; } = TypeMessage.sticker;
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
