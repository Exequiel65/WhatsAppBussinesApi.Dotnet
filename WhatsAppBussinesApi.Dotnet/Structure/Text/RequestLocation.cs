using WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class RequestLocation : BaseMessage
    {
        public override TypeMessage type => TypeMessage.interactive;
        public required RequestLocationComponent interactive { get; init; }

        public RequestLocation()
        {
        }

        public RequestLocation(string to, string bodyText)
        {
            this.to = to;
            interactive = new RequestLocationComponent(bodyText);
        }
    }

    public class RequestLocationComponent
    {
        public string type { get; } = "location_request_message";
        public required TextBody body { get; init; }
        public RequestLocationAction action { get; } = new();

        public RequestLocationComponent()
        {
        }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
        public RequestLocationComponent(string bodyText)
        {
            body = new TextBody(bodyText);
        }
    }

    public class RequestLocationAction : BaseAction
    {
        public string name { get; } = "send_location";
    }
}
