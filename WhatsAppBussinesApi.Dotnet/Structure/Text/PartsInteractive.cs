using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    #region Header
    public class BaseHeader
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HeaderInteractiveType type { get; } = HeaderInteractiveType.text;
    }


    public class HeaderInteractive : BaseHeader
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
