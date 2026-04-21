using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace WhatsAppBussinesApi.Dotnet.Structure.Templates
{
    public interface ITemplateTextMessage
    {
        TemplateData template { get; }
    }
    public class TemplateTextMessage : BaseMessage, ITemplateTextMessage
    {
        public override TypeMessage type => TypeMessage.template;

        public required TemplateData template { get; init; }

        public TemplateTextMessage()
        {
        }

        [SetsRequiredMembers]
        public TemplateTextMessage(string to, TemplateData template)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.template = template ?? throw new ArgumentNullException(nameof(template));
        }

        [SetsRequiredMembers]
        public TemplateTextMessage(string to, string templateName, string lang, List<BaseParameters> parametersBody)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));

            template = new TemplateData
            {
                name = templateName,
                language = new LanguageCode(lang),
                components = new List<Component>
                {
                    new()
                    {
                        type = "body",
                        parameters = parametersBody ?? []
                    }
                }
            };
        }
    }

    public class TemplateData
    {
        public string name { get; set; }
        public LanguageCode language { get; set; }
        public List<Component> components { get; set; } = new List<Component>();
    }

    public class LanguageCode
    {
        public string code { get; set; } = "en";
        public LanguageCode() { }

        public LanguageCode(string lang)
        {
            code = lang;
        }
    }
    public class Component
    {
        public string type { get; set; } = "body";
        public List<BaseParameters> parameters { get; set; }
    }

    public class ParameterText : BaseParameters
    {
        public override string type { get; set; } = "text";
        public string text { get; set; }

        public ParameterText() { }
        public ParameterText(string text)
        {
            this.text = text;
        }
    }
    public class ParameterCurrency : BaseParameters
    {
        public override string type { get; set; } = "currency";
        public Currency currency { get; set; }

        public ParameterCurrency()
        {

        }

        public ParameterCurrency(Currency currency)
        {
            this.currency = currency;
        }

        public ParameterCurrency(string fallback, string code, decimal amount)
        {
            currency = new Currency(fallback, code, amount);
        }
    }
    public class Currency
    {
        public string fallback_value { get; set; }
        public string code { get; set; }
        public decimal amount_1000 { get; set; }
        public Currency()
        {

        }
        public Currency(string fallback, string code, decimal amount)
        {
            fallback_value = fallback;
            this.code = code;
            amount_1000 = amount;
        }
    }

    public class ParameterDateTime : BaseParameters
    {
        public override string type { get; set; } = "date_time";
        public Date_Time date_time { get; set; }

        public ParameterDateTime()
        {

        }

        public ParameterDateTime(DateTime dateTime, string lang)
        {
            date_time = new Date_Time(dateTime, new CultureInfo(lang));
        }
    }



    public class Date_Time
    {
        public string fallback_value { get; set; }
        public int day_of_week { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day_of_month { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public string calendar { get; set; }

        public Date_Time()
        {

        }

        public Date_Time(DateTime date, CultureInfo culture)
        {
            fallback_value = date.ToString("m", culture);
            day_of_week = (int)date.DayOfWeek;
            calendar = "GREGORIAN";
            hour = date.Hour;
            minute = date.Minute;
            month = date.Month;
            year = date.Year;
            day_of_month = date.Day;
        }
    }

    public class BaseButtonComponent : Component
    {
        public new string type { get; set; } = "button";

        public ButtonType sub_type { get; set; } = ButtonType.quick_reply;
        public int index { get; set; } = 0;
    }

    public class QuickReplyButton : BaseButtonComponent
    {
        public QuickReplyButton(string payload, int index = 0)
        {
            this.index = index;
            parameters = new List<BaseParameters> { new QuickReplyParameter(payload) };
        }
        public QuickReplyButton(QuickReplyParameter quickReply, int index = 0)
        {
            this.index = index;
            parameters = new List<BaseParameters> { quickReply };
        }
    }

    public class CopyCodeButton : BaseButtonComponent
    {
        public CopyCodeButton(string code, int index = 0)
        {
            this.index = index;
            sub_type = ButtonType.copy_code;
            parameters = new List<BaseParameters> { new CopyCodeParameter(code) };
        }
        public CopyCodeButton(CopyCodeParameter copyCodeParameter, int index = 0)
        {
            this.index = index;
            sub_type = ButtonType.copy_code;
            parameters = new List<BaseParameters> { copyCodeParameter };
        }
    }

    public class UrlButtonComponent : BaseButtonComponent
    {
        public UrlButtonComponent(Uri url, int index = 0)
        {
            this.index = index;
            sub_type = ButtonType.url;
            parameters = new List<BaseParameters>()
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
            coupon_code = code;
        }
    }
    public class UrlButtonParameter : BaseParameters
    {
        public override string type { get; set; } = "text";
        public string text { get; set; }

        public UrlButtonParameter(Uri url)
        {
            text = url.ToString();
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ButtonType
    {
        quick_reply,
        url,
        copy_code
    }

    public class HeaderImageParameter : BaseParameters
    {
        public override string type { get; set; } = "image";
        public ImageHeader image { get; set; }

        public HeaderImageParameter() { }

        public HeaderImageParameter(string imageUrl)
        {
            image = new ImageHeader()
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
            location = new LocationParameter()
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
