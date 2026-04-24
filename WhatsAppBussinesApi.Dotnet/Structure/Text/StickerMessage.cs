using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

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
        public StickerMessage(string to, Uri link)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            sticker = new StickerComponentWithLink(link?.ToString() ?? throw new ArgumentNullException(nameof(link)));
        }

        [SetsRequiredMembers]
        public StickerMessage(string to, BaseSticker sticker)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.sticker = sticker ?? throw new ArgumentNullException(nameof(sticker));
        }
    }

    public class BaseSticker
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? link { get; set; }
    }

    public class StickerComponentWithLink : BaseSticker
    {
        public StickerComponentWithLink()
        {
        }

        public StickerComponentWithLink(string link)
        {
            this.link = string.IsNullOrWhiteSpace(link)
                ? throw new ArgumentException("Sticker link cannot be empty", nameof(link))
                : link;
        }
    }

    public class StickerComponentWithId : BaseSticker
    {
        public StickerComponentWithId()
        {

        }

        public StickerComponentWithId(string id)
        {
            this.id = string.IsNullOrWhiteSpace(id)
                ? throw new ArgumentException("Sticker id cannot be empty", nameof(id))
                : id;
        }
    }

}
