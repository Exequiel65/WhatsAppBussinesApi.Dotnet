using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class LocationMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.location;
        public required LocationComponent location { get; init; }

        public LocationMessage()
        {

        }
        [SetsRequiredMembers]
        public LocationMessage(string to, LocationComponent location)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.location = location ?? throw new ArgumentNullException(nameof(location));
        }

        [SetsRequiredMembers]
        public LocationMessage(string to, decimal longitude, decimal latitude, string? name = null, string? address = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? address { get; set; }
    }
}
