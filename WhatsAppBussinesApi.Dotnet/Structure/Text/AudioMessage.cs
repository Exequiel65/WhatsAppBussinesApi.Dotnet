using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class AudioMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.audio;
        public AudioComponent audio { get; set; }
    }

    public class AudioComponent
    {
        public string id { get; set; }
    }
}
