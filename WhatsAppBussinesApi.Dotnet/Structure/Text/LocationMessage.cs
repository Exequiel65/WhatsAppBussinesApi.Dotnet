using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class LocationMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.location;
        public LocationComponent location { get; set; }

        public LocationMessage()
        {

        }
        public LocationMessage(string to, LocationComponent location)
        {
            this.to = to;
            this.location = location;
        }

        public LocationMessage(string to, decimal longitude, decimal latitude, string name, string address)
        {
            this.to = to;
            this.location = new LocationComponent()
            {
                address = address,
                longitude = longitude,
                latitude = latitude,
                name = name,
            };
        }
    }

    public class LocationComponent
    {
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
}
