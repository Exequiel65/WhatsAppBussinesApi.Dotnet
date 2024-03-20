namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class ImageMessage : BaseMessage
    {
        public TypeMessage Type { get; set; } = TypeMessage.image;

        public BaseImage image { get; set; }
    }


    public class BaseImage { }
    public class ImageComponent : BaseImage
    {
        public string link { get; set; }
        public BaseProvider provider { get; set; }
        public ImageComponent() { }
        public ImageComponent(string link, string providerName)
        {
            this.link = link;
            this.provider = new BaseProvider(providerName);
        }

        public ImageComponent(string link, BaseProvider providerName)
        {
            this.link = link;
            this.provider = providerName;
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
