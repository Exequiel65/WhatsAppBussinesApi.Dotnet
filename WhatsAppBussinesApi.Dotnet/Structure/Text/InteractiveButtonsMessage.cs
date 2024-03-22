using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonIgnoreCondition = System.Text.Json.Serialization;
namespace WhatsAppBussinesApi.Dotnet.Structure.Text
{
    public class InteractiveButtonsMessage : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeMessage type { get; set; } = TypeMessage.interactive;

        public InteractiveComponent interactive { get; set; }
        public InteractiveButtonsMessage() { }

        public InteractiveButtonsMessage(string to, InteractiveComponent component)
        {
            this.interactive = component;
            this.to = to;
        }

        public InteractiveButtonsMessage(string to, TextBody body, ActionButtonsReply action, BaseHeader header = null, FooterText footer = null)
        {
            this.interactive = new InteractiveComponent(body, action, header, footer);
            this.to = to;
        }

        public InteractiveButtonsMessage(string to, string textBody, ActionButtonsReply action, BaseHeader header = null, FooterText footer = null)
        {
            this.interactive = new InteractiveComponent(new TextBody(textBody), action, header, footer);
            this.to = to;
        }

    }

    public class InteractiveComponent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public InteractiveType type { get; } = InteractiveType.list;
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public BaseHeader header { get; set; }

        public BaseBody body { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public FooterText footer {  get; set; }

        public BaseAction action { get; set; }

        public InteractiveComponent(TextBody body, ActionButtonsReply action, BaseHeader header = null, FooterText footer = null )
        {
            this.body = body;
            this.action = action;
            this.type = InteractiveType.button;
            if (header != null)
            {
                this.header = header;
            }

            if (footer != null)
            {
                this.footer = footer;
            }
        }
    }

    #region Actions

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
    #endregion

    
}




