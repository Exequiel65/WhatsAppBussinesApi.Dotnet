using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonIgnoreCondition = System.Text.Json.Serialization;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class DocumentMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.document;
        public BaseDocument document { get; set; }

        public DocumentMessage()
        {

        }

        public DocumentMessage(string to, Uri link)
        {
            this.to = to;
            this.document = new DocumentComponentWithLink()
            {
                link = link.ToString()
            };
        }

        public DocumentMessage(string to, DocumentComponentWithLink link)
        {
            this.to = to;
            this.document = link;
        }

        public DocumentMessage(string to, string id, string fileName)
        {
            this.to = to;
            this.document = new DocumentComponentWithId(id, fileName);
        }

        public DocumentMessage(string to, DocumentComponentWithId dId)
        {
            this.to = to;
            this.document = dId;
        }

        public DocumentMessage(string to, Uri link, string providerName)
        {
            this.to = to;
            this.document = new DocumentComponentWithLinkProvider(link.ToString(), providerName);
        }
        public DocumentMessage(string to, Uri link, BaseProvider provider)
        {
            this.to = to;
            this.document = new DocumentComponentWithLinkProvider(link.ToString(), provider);
        }
    }


    public class BaseDocument : BaseMedia { }

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
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public string filename { get; set; }
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
