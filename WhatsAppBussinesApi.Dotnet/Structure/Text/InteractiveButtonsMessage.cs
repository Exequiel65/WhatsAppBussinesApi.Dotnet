using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class InteractiveButtonsMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; }

    }

    public class InteractiveComponent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public InteractiveType type { get; } = InteractiveType.list;

        public BaseHeader header { get; set; }

        public BaseBody body { get; set; }
    }

    #region Header
    public class BaseHeader 
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderInteractiveType type { get; } = HeaderInteractiveType.text;
    }

    public class HeaderInteractive  : BaseHeader
    {
        public string text { get; set; }
    }

    public class HeaderDocumentInteractive : BaseHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderInteractiveType type { get; } = HeaderInteractiveType.document;
        public BaseDocument document { get; set; }

        public HeaderDocumentInteractive()
        {
            
        }

        public HeaderDocumentInteractive(string id, string fileName)
        {
            this.document = new DocumentComponentWithId(id, fileName);
        }
        public HeaderDocumentInteractive(DocumentComponentWithId document)
        {
            this.document = document;
        }

        public HeaderDocumentInteractive(Uri link, string providerName)
        {
            this.document = new DocumentComponentWithLinkProvider(link.ToString(), providerName);
        }
    }


    public class HeaderImageInteractive : BaseHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderInteractiveType type { get; } = HeaderInteractiveType.image;
        public BaseImage image { get; set; }


        public HeaderImageInteractive()
        {
            
        }

        public HeaderImageInteractive(string id)
        {
            this.image = new ImageComponentWithId(id);
        }

        public HeaderImageInteractive(Uri link, string providerName)
        {
            this.image = new ImageComponentWithProvider(link.ToString(), providerName);
        }
    }

    #endregion

    #region Body
    public class BaseBody
    {

    }

    public class TextBody: BaseBody
    {

    }
    #endregion

    public enum InteractiveType
    {
        list,
        button,
        location
    }

    public enum HeaderInteractiveType
    {
        text,
        image,
        video,
        document
    }
}




