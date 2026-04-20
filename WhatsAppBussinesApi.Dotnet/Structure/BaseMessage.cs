using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeMessage
    {
        template,
        document,
        text,
        audio,
        contact,
        image,
        location,
        sticker,
        video,
        interactive,
        location_request_message
    }

    public abstract class BaseMessage
    {
        public string messaging_product { get; init; } = "whatsapp";
        public string recipient_type { get; init; } = "individual";
        public required string to { get; init; }
        public abstract TypeMessage type { get; }
    }

    public class BaseProvider
    {
        public string name { get; set; }
        public BaseProvider() { }
        public BaseProvider(string name)
        {
            this.name = name;
        }
    }


    public abstract class BaseParameters
    {
        public abstract string type { get; set; }
    }

    public class BaseMedia
    {

    }

    public class BaseAction { }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum InteractiveType
    {
        list,
        button,
        location
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeaderInteractiveType
    {
        text,
        image,
        video,
        document
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ButtonsType
    {
        reply
    }
}
