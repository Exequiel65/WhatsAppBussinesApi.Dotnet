using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using JsonIgnoreCondition = System.Text.Json.Serialization;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives
{
    public class InteractiveButtonsMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.interactive;

        public required InteractiveComponent interactive { get; init; }
        public InteractiveButtonsMessage()
        {
        }

        [SetsRequiredMembers]
        public InteractiveButtonsMessage(string to, InteractiveComponent component)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = component ?? throw new ArgumentNullException(nameof(component));
        }

        [SetsRequiredMembers]
        public InteractiveButtonsMessage(string to, TextBody body, ActionButtonsReply action, BaseHeader? header = null, FooterText? footer = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = new InteractiveComponent(body, action, header, footer);
        }

        [SetsRequiredMembers]
        public InteractiveButtonsMessage(string to, string textBody, ActionButtonsReply action, BaseHeader? header = null, FooterText? footer = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = new InteractiveComponent(new TextBody(textBody), action, header, footer);
        }
    }

    public class InteractiveComponent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public InteractiveType type { get; set; } = InteractiveType.button;

        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public BaseHeader? header { get; set; }

        public required BaseBody body { get; init; }

        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public FooterText? footer { get; set; }

        public required BaseAction action { get; init; }

        public InteractiveComponent()
        {
        }

        [SetsRequiredMembers]
        public InteractiveComponent(TextBody body, ActionButtonsReply action, BaseHeader? header = null, FooterText? footer = null)
        {
            this.body = body ?? throw new ArgumentNullException(nameof(body));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.header = header;
            this.footer = footer;
        }
    }

    public class ActionButtonsReply : BaseAction
    {
        public List<ButtonAction> buttons { get; set; }

        public ActionButtonsReply()
        {
        }

        public ActionButtonsReply(List<ButtonAction> buttons)
        {
            this.buttons = buttons;
        }
    }

    public class ButtonAction
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ButtonsType type { get; set; } = ButtonsType.reply;
        public ReplyButton reply { get; set; }

        public ButtonAction()
        {
        }

        public ButtonAction(ReplyButton reply)
        {
            this.reply = reply;
        }

        public ButtonAction(string id, string title)
        {
            this.reply = new ReplyButton(id, title);
        }
    }

    public class ReplyButton
    {
        public string id { get; set; }
        public string title { get; set; }

        public ReplyButton()
        {
        }

        public ReplyButton(string id, string title)
        {
            this.id = id;
            this.title = title;
        }
    }
}




