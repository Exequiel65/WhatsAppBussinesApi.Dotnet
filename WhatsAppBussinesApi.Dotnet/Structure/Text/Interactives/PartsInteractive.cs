using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives
{
    #region Header
    public class BaseHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderInteractiveType type { get; protected set; }
    }

    public class HeaderInteractive : BaseHeader
    {
        public string text { get; set; }

        public HeaderInteractive()
        {
            type = HeaderInteractiveType.text;
        }

        public HeaderInteractive(string text)
        {
            type = HeaderInteractiveType.text;
            this.text = text;
        }
    }

    public class HeaderDocumentInteractive : BaseHeader
    {
        public BaseDocument document { get; set; }

        public HeaderDocumentInteractive()
        {
            type = HeaderInteractiveType.document;
        }

        public HeaderDocumentInteractive(string id, string? fileName = null)
        {
            type = HeaderInteractiveType.document;
            document = new DocumentComponentWithId(id, fileName);
        }

        public HeaderDocumentInteractive(DocumentComponentWithLink document)
        {
            type = HeaderInteractiveType.document;
            this.document = document;
        }

        public HeaderDocumentInteractive(Uri link, string? fileName = null)
        {
            type = HeaderInteractiveType.document;
            document = new DocumentComponentWithLink
            {
                link = link.ToString(),
                filename = fileName
            };
        }
    }

    public class HeaderImageInteractive : BaseHeader
    {
        public BaseImage image { get; set; }

        public HeaderImageInteractive()
        {
            type = HeaderInteractiveType.image;
        }

        public HeaderImageInteractive(string id)
        {
            type = HeaderInteractiveType.image;
            image = new ImageComponentWithId(id);
        }

        public HeaderImageInteractive(Uri link)
        {
            type = HeaderInteractiveType.image;
            image = new ImageComponentWithProvider(link.ToString(), new BaseProvider("default"));
        }
    }

    public class HeaderVideoInteractive : BaseHeader
    {
        public BaseVideo video { get; set; }

        public HeaderVideoInteractive()
        {
            type = HeaderInteractiveType.video;
        }

        public HeaderVideoInteractive(string id)
        {
            type = HeaderInteractiveType.video;
            video = new VideoComponentWithId(id);
        }

        public HeaderVideoInteractive(Uri link)
        {
            type = HeaderInteractiveType.video;
            video = new VideoComponentWithLink(link.ToString());
        }
    }

    #endregion

    #region Body
    public class BaseBody
    {

    }

    public class TextBody : BaseBody
    {
        public string text { get; set; }

        public TextBody()
        {

        }

        public TextBody(string text)
        {
            this.text = text;
        }
    }
    #endregion


    #region Footer

    public class FooterText
    {
        public string text { set; get; }
        public FooterText()
        {

        }

        public FooterText(string text)
        {
            this.text = text;
        }
    }

    #endregion
}
