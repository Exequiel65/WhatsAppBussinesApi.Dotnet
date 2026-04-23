using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class ImageMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.image;

        public required BaseImage image { get; init; }

        public ImageMessage()
        {

        }
        [SetsRequiredMembers]
        public ImageMessage(string to, Uri link, string? caption = null)
        {
            this.image = new ImageComponent(link.ToString())
            {
                caption = caption
            };
            this.to = to ?? throw new ArgumentNullException(nameof(to));
        }

        [SetsRequiredMembers]
        public ImageMessage(string to, Uri link, string providerName, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.image = new ImageComponentWithProvider(link.ToString(), providerName)
            {
                caption = caption
            };
        }

        [SetsRequiredMembers]
        public ImageMessage(string to, ImageComponent component)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.image = component;
        }

        [SetsRequiredMembers]
        public ImageMessage(string to, string id, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.image = new ImageComponentWithId(id)
            {
                caption = caption
            };
        }
    }


    public class BaseImage : BaseMedia
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? caption { get; set; }
    }
    public class ImageComponentWithProvider : BaseImage
    {
        public string link { get; set; }
        public BaseProvider provider { get; set; }
        public ImageComponentWithProvider() { }
        public ImageComponentWithProvider(string link, string providerName)
        {
            this.link = link;
            this.provider = new BaseProvider(providerName);
        }

        public ImageComponentWithProvider(string link, BaseProvider providerName)
        {
            this.link = link;
            this.provider = providerName;
        }
    }

    public class ImageComponent : BaseImage
    {
        public string link { get; set; }
        public ImageComponent() { }
        public ImageComponent(string link)
        {
            this.link = link;
        }
    }

    public class ImageComponentWithId : BaseImage
    {
        public string id { get; set; }

        public ImageComponentWithId()
        {

        }
        public ImageComponentWithId(string id)
        {
            this.id = id;
        }
    }
}
