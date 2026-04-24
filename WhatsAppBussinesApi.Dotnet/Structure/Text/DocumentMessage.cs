using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

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
        public DocumentMessage(string to, Uri link, string? fileName = null, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithLink()
            {
                link = link.ToString(),
                filename = fileName,
                caption = caption
            };
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, DocumentComponentWithLink link)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = link;
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, string id, string? fileName = null, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithId(id, fileName, caption);
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, DocumentComponentWithId dId)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = dId;
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, Uri link, string providerName, string? fileName = null, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithLinkProvider(link.ToString(), providerName)
            {
                filename = fileName,
                caption = caption
            };
        }

        [SetsRequiredMembers]
        public DocumentMessage(string to, Uri link, BaseProvider provider, string? fileName = null, string? caption = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.document = new DocumentComponentWithLinkProvider(link.ToString(), provider)
            {
                filename = fileName,
                caption = caption
            };
        }
    }


    public class BaseDocument : BaseMedia
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? filename { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? caption { get; set; }
    }

    public class DocumentComponentWithId : BaseDocument
    {
        public string id { get; set; }

        public DocumentComponentWithId() { }

        public DocumentComponentWithId(string id, string? filename = null, string? caption = null)
        {
            this.id = id;
            this.filename = filename;
            this.caption = caption;
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
