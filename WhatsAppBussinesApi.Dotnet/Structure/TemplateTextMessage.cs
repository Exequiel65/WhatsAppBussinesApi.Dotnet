using System.Globalization;

namespace WhatsAppBussinesApi.Dotnet.Structure
{
    public interface ITemplateTextMessage
    {
        TemplateData template { get; set; }
    }
    public class TemplateTextMessage : BaseMessage, ITemplateTextMessage
    {
        public TemplateData template { get; set; }

        public TemplateTextMessage()
        {
            this.type = TypeMessage.template;
        }

        public TemplateTextMessage(string sender, TemplateData template)
        {
            this.type = TypeMessage.template;
            this.template = template;
            this.to = sender;
        }

        public TemplateTextMessage(string sender, string templateName, string lang, List<BaseParameters> parametersBody)
        {
            this.type = TypeMessage.template;
            if (this.template is null)
            {
                this.template = new TemplateData()
                {
                    name = templateName,
                    language = new LanguageCode(lang)
                };
            }
            else
            {
                this.template.name = templateName;
                this.template.language = new LanguageCode(lang);
            }

            this.template.components.Add(new Component()
            {
                type = "body",
                parameters = parametersBody,
            });
            this.to = sender;
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
            this.code = lang;
        }
    }
    public class Component
    {
        public string type { get; set; } = "body";
        public List<BaseParameters> parameters { get; set; }
    }

    public abstract class BaseParameters
    {
        public abstract string type { get; set; }
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
            this.currency = new Currency(fallback, code, amount);
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
            this.fallback_value = fallback;
            this.code = code;
            this.amount_1000 = amount;
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
            this.date_time = new Date_Time(dateTime, new CultureInfo(lang));
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
            this.fallback_value = date.ToString("m", culture);
            this.day_of_week = (int)date.DayOfWeek;
            this.calendar = "GREGORIAN";
            this.hour = date.Hour;
            this.minute = date.Minute;
            this.month = date.Month;
            this.year = date.Year;
            this.day_of_month = date.Day;
        }
    }
}
