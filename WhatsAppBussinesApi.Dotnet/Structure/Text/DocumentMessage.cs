using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using JsonIgnoreCondition = System.Text.Json.Serialization;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class DocumentMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.document;
        public required BaseDocument document { get; init; }

        public DocumentMessage()
        {

        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, Uri link)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithLink()
            {
                link = link.ToString()
            };
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, DocumentComponentWithLink link)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = link;
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, string id, string fileName)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithId(id, fileName);
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, DocumentComponentWithId dId)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = dId;
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, Uri link, string providerName)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithLinkProvider(link.ToString(), providerName);
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, Uri link, BaseProvider provider)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
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
