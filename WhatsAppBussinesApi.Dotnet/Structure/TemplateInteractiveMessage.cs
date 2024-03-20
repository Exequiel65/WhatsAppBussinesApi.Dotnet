using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppApi.Structure
{
    public class TemplateInteractiveMessage : TemplateTextMessage
    {
        public TemplateInteractiveMessage() { }
        public TemplateInteractiveMessage(string sender, TemplateData template) : base(sender, template) { }

        public TemplateInteractiveMessage(string sender, string templateName, string lang, List<BaseParameters> parametersBody, List<BaseButtonComponent>? buttonComponents = null) : base(sender, templateName, lang, parametersBody)
        {
            if (buttonComponents != null)
            {
                this.template.components.AddRange(buttonComponents);
            }

        }

        public TemplateInteractiveMessage(string sender, string templateName, string lang, List<BaseParameters> parametersBody, Uri? HeaderUrlImage = null, List<BaseButtonComponent>? buttonComponents = null) : base(sender, templateName, lang, parametersBody)
        {
            if (HeaderUrlImage != null)
            {
                this.template.components.Add(new Component()
                {
                    type = "header",
                    parameters = [new HeaderImageParameter(HeaderUrlImage.ToString())]
                });
            }
            if (buttonComponents != null)
            {
                this.template.components.AddRange(buttonComponents);
            }

        }
        public TemplateInteractiveMessage(string sender, string templateName, string lang, List<BaseParameters> parametersBody, string? headerText = null, List<BaseButtonComponent>? buttonComponents = null) : base(sender, templateName, lang, parametersBody)
        {
            if (headerText != null)
            {
                this.template.components.Add(new Component()
                {
                    type = "header",
                    parameters = [new HeaderTextParameter(headerText)]
                });
            }

            if (buttonComponents != null)
            {
                this.template.components.AddRange(buttonComponents);
            }

        }

        public TemplateInteractiveMessage(string sender, string templateName, string lang, List<BaseParameters> parametersBody, LocationParameter? headerLocationParameters = null, List<BaseButtonComponent>? buttonComponents = null) : base(sender, templateName, lang, parametersBody)
        {
            if (headerLocationParameters != null)
            {
                this.template.components.Add(new Component()
                {
                    type = "header",
                    parameters = [new HeaderLocationParameter(headerLocationParameters)]
                });
            }

            if (buttonComponents != null)
            {
                this.template.components.AddRange(buttonComponents);
            }
        }
    }


    public class BaseButtonComponent : Component
    {
        public string type { get; set; } = "button";
        [JsonConverter(typeof(StringEnumConverter))]
        public ButtonType sub_type { get; set; } = ButtonType.quick_reply;
        public int index { get; set; } = 0;
    }

    public class QuickReplyButton : BaseButtonComponent
    {
        public QuickReplyButton(string payload, int index = 0)
        {
            this.index = index;
            this.parameters = new List<BaseParameters> { new QuickReplyParameter(payload) };
        }
        public QuickReplyButton(QuickReplyParameter quickReply, int index = 0)
        {
            this.index = index;
            this.parameters = new List<BaseParameters> { quickReply };
        }
    }

    public class CopyCodeButton : BaseButtonComponent
    {
        public CopyCodeButton(string code, int index = 0)
        {
            this.index = index;
            this.sub_type = ButtonType.COPY_CODE;
            this.parameters = new List<BaseParameters> { new CopyCodeParameter(code) };
        }
        public CopyCodeButton(CopyCodeParameter copyCodeParameter, int index = 0)
        {
            this.index = index;
            this.sub_type = ButtonType.COPY_CODE;
            this.parameters = new List<BaseParameters> { copyCodeParameter};
        }
    }

    public class UrlButtonComponent : BaseButtonComponent
    {
        public UrlButtonComponent(Uri url, int index = 0)
        {
            this.index = index;
            this.sub_type = ButtonType.url;
            this.parameters = new List<BaseParameters>()
            {
                new UrlButtonParameter(url)
            };
        }
    }


    public class QuickReplyParameter : BaseParameters
    {
        public override string type { get; set; } = "payload";
        public string payload { get; set; }

        public QuickReplyParameter(string payload)
        {
            this.payload = payload;
        }
    }

    public class CopyCodeParameter : BaseParameters
    {
        public override string type { get; set; } = "coupon_code";
        public string coupon_code { get; set; }
        public CopyCodeParameter(string code)
        {
            this.coupon_code = code;
        }
    }
    public class UrlButtonParameter : BaseParameters
    {
        public override string type { get; set; } = "text";
        public string text { get; set; }

        public UrlButtonParameter(Uri url)
        {
            this.text = url.ToString();
        }
    }

    public enum ButtonType
    {
        quick_reply,
        url,
        COPY_CODE
    }
}
