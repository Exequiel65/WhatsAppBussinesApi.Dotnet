namespace WhatsAppBussinesApi.Dotnet.Structure
{
    public class TemplateHeaderMessage : TemplateTextMessage
    {
        public TemplateHeaderMessage() { }
        public TemplateHeaderMessage(string sender, TemplateData template) : base(sender, template) { }

        public TemplateHeaderMessage(string sender, string templateName, string lang, Uri HeaderUrlImage, List<BaseParameters> parametersBody) : base(sender, templateName, lang, parametersBody)
        {
            this.template.components.Add(new Component()
            {
                type = "header",
                parameters = [new HeaderImageParameter(HeaderUrlImage.ToString())]
            });
        }
        public TemplateHeaderMessage(string sender, string templateName, string lang, string headerText, List<BaseParameters> parametersBody) : base(sender, templateName, lang, parametersBody)
        {
            this.template.components.Add(new Component()
            {
                type = "header",
                parameters = [new HeaderTextParameter(headerText)]
            });
        }

        public TemplateHeaderMessage(string sender, string templateName, string lang, LocationParameter headerLocationParameters, List<BaseParameters> parametersBody) : base(sender, templateName, lang, parametersBody)
        {
            this.template.components.Add(new Component()
            {
                type = "header",
                parameters = [new HeaderLocationParameter(headerLocationParameters)]
            });
        }
    }

    public class HeaderImageParameter : BaseParameters
    {
        public override string type { get; set; } = "image";
        public ImageHeader image { get; set; }

        public HeaderImageParameter() { }

        public HeaderImageParameter(string imageUrl)
        {
            this.image = new ImageHeader()
            {
                link = imageUrl
            };
        }
    }

    public class HeaderTextParameter : BaseParameters
    {
        public override string type { get; set; } = "text";
        public string text { get; set; }

        public HeaderTextParameter()
        {

        }

        public HeaderTextParameter(string text)
        {
            this.text = text;
        }
    }

    public class ImageHeader
    {
        public string link { get; set; }
    }


    public class HeaderLocationParameter : BaseParameters
    {
        public override string type { get; set; } = "location";
        public LocationParameter location { get; set; }

        public HeaderLocationParameter() { }

        public HeaderLocationParameter(LocationParameter location)
        {
            this.location = location;
        }

        public HeaderLocationParameter(string latitude, string longitude, string name, string address)
        {
            this.location = new LocationParameter()
            {
                latitude = latitude,
                longitude = longitude,
                name = name,
                address = address
            };
        }
    }

    public class LocationParameter
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
}
