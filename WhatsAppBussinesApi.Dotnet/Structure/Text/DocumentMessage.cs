using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class DocumentMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.document;
        public BaseDocument document { get; set; }
    }


    public class BaseDocument { }

    public class DocumentComponentWithId : BaseDocument
    {
        public string id { get; set; }
        public string filename { get; set; }

        public DocumentComponentWithId() { }

        public DocumentComponentWithId(string id, string filename)
        {
            this.id = id;
            this.filename = filename;
        }
    }

    public class DocumentComponentWithLink : BaseDocument
    {
        public string link { get; set; }
    }

    public class DocumentComponentWithLinkProvider : DocumentComponentWithLink
    {
        public BaseProvider provider { get; set; }

        public DocumentComponentWithLinkProvider() { }

        public DocumentComponentWithLinkProvider(string link, BaseProvider provider)
        {
            this.link = link;
            this.provider = provider;
        }

        public DocumentComponentWithLinkProvider(string link, string providerName)
        {
            this.link = link;
            this.provider = new BaseProvider(providerName);
        }
    }
}
