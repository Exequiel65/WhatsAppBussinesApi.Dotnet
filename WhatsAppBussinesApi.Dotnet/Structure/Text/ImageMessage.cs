using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class ImageMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.image;

        public BaseImage image { get; set; }

        public ImageMessage()
        {

        }
        public ImageMessage(string to, Uri link)
        {
            this.image = new ImageComponent(link.ToString());
            this.to = to;
        }
        public ImageMessage(string to, Uri link, string providerName)
        {
            this.to = to;
            this.image = new ImageComponentWithProvider(link.ToString(), providerName);
        }

        public ImageMessage(string to, ImageComponent component)
        {
            this.to = to;
            this.image = component;
        }

        public ImageMessage(string to, string id)
        {
            this.to = to;
            this.image = new ImageComponentWithId(id);
        }
    }


    public class BaseImage { }
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
