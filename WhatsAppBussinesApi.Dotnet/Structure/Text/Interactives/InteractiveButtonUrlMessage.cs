using System.Diagnostics.CodeAnalysis;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives
{
    public class InteractiveButtonUrlMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.interactive;

        public required InteractiveCtaUrlComponent interactive { get; init; }

        public InteractiveButtonUrlMessage()
        {
        }

        [SetsRequiredMembers]
        public InteractiveButtonUrlMessage(string to, InteractiveCtaUrlComponent component)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = component ?? throw new ArgumentNullException(nameof(component));
        }

        [SetsRequiredMembers]
        public InteractiveButtonUrlMessage(string to, string bodyText, string displayText, string url, BaseHeader? header = null, FooterText? footer = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = new InteractiveCtaUrlComponent(new TextBody(bodyText), new CtaUrlAction(displayText, url), header, footer);
        }
    }

    public class InteractiveCtaUrlComponent
    {
        public string type { get; } = "cta_url";
        public BaseHeader? header { get; set; }
        public required TextBody body { get; init; }
        public FooterText? footer { get; set; }
        public required CtaUrlAction action { get; init; }

        public InteractiveCtaUrlComponent()
        {
        }

        [SetsRequiredMembers]
        public InteractiveCtaUrlComponent(TextBody body, CtaUrlAction action, BaseHeader? header = null, FooterText? footer = null)
        {
            this.body = body ?? throw new ArgumentNullException(nameof(body));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.header = header;
            this.footer = footer;
        }
    }

    public class CtaUrlAction : BaseAction
    {
        public string name { get; } = "cta_url";
        public required CtaUrlParameters parameters { get; init; }

        public CtaUrlAction()
        {
        }

        [SetsRequiredMembers]
        public CtaUrlAction(string displayText, string url)
        {
            parameters = new CtaUrlParameters(displayText, url);
        }
    }

    public class CtaUrlParameters
    {
        public string display_text { get; set; }
        public string url { get; set; }

        public CtaUrlParameters()
        {
            display_text = string.Empty;
            url = string.Empty;
        }

        public CtaUrlParameters(string displayText, string url)
        {
            if (string.IsNullOrWhiteSpace(displayText))
            {
                throw new ArgumentException("Display text is required.", nameof(displayText));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL is required.", nameof(url));
            }

            this.display_text = displayText;
            this.url = url;
        }
    }
}
