using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure
{
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
    interface IMessage
    {
        string messaging_product { get; set; }
        string recipient_type { get; set; }
        string to { get; set; }
        TypeMessage type { get; set; }
    }
    public class BaseMessage : IMessage
    {
        public string messaging_product { get; set; } = "whatsapp";
        public string recipient_type { get; set; } = "individual";
        public string to { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.text;
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
}
