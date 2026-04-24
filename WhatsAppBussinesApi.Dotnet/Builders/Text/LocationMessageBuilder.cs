using WhatsAppBussinesApi.Dotnet.Structure.Text;

namespace WhatsAppBussinesApi.Dotnet.Builders.Text
{
    public sealed class LocationMessageBuilder : IMessageBuilder<LocationMessage>
    {
        private string? _recipient;
        private LocationComponent? _location;

        public LocationMessageBuilder To(string phoneNumber)
        {
            _recipient = phoneNumber;
            return this;
        }

        public LocationMessageBuilder WithLocation(LocationComponent location)
        {
            _location = location;
            return this;
        }

        public LocationMessageBuilder WithLocation(decimal longitude, decimal latitude, string? name = null, string? address = null)
        {
            _location = new LocationComponent
            {
                longitude = longitude,
                latitude = latitude,
                name = name,
                address = address
            };
            return this;
        }

        public LocationMessage Build()
        {
            if (string.IsNullOrWhiteSpace(_recipient))
            {
                throw new InvalidOperationException("Recipient phone number is required.");
            }

            if (_location is null)
            {
                throw new InvalidOperationException("Location payload is required.");
            }

            if (_location.latitude < -90 || _location.latitude > 90)
            {
                throw new InvalidOperationException("Latitude must be between -90 and 90.");
            }

            if (_location.longitude < -180 || _location.longitude > 180)
            {
                throw new InvalidOperationException("Longitude must be between -180 and 180.");
            }

            return new LocationMessage
            {
                to = _recipient,
                location = _location
            };
        }
    }
}
