using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using JsonIgnoreCondition = System.Text.Json.Serialization;
namespace WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives
{
    public class InteractiveListMessage : BaseMessage
    {
        public override TypeMessage type => TypeMessage.interactive;

        public required ListInteractiveComponent interactive { get; init; }

        public InteractiveListMessage()
        {

        }

        [SetsRequiredMembers]
        public InteractiveListMessage(string to, string textBody, string labelButton, List<SectionList> sectionLists, BaseHeader header = null, FooterText footer = null)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = new ListInteractiveComponent(textBody, labelButton, sectionLists, header, footer);
        }
    }
    public class ListInteractiveComponent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public InteractiveType type { get; set; } = InteractiveType.list;

        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public BaseHeader? header { get; set; }

        public TextBody body { get; set; }

        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public FooterText? footer { get; set; }

        public BaseAction action { get; set; }

        public ListInteractiveComponent()
        {

        }

        public ListInteractiveComponent(string textBody, string labelButton, List<SectionList> sectionLists, BaseHeader header = null, FooterText footer = null)
        {
            this.body = new TextBody(textBody);
            this.action = new ListAction(labelButton, sectionLists);
            this.footer = footer;
            this.header = header;
        }
    }

    public class ListAction : BaseAction
    {
        public string button { get; }
        public List<SectionList> sections { get; set; }

        public ListAction() { }

        public ListAction(string labelButton, SectionList section)
        {
            this.button = labelButton;
            this.sections = new List<SectionList> { section };
        }
        public ListAction(string labelButton, List<SectionList> sections)
        {
            this.button = labelButton;
            this.sections = sections;
        }
    }

    public class SectionList
    {
        public string title { get; set; }
        public List<Row> rows { get; set; }
        public SectionList() { }

        public SectionList(string title, List<Row> rows)
        {
            this.title = title;
            this.rows = rows;
        }

    }

    public class Row
    {
        public string id { get; set; }
        public string title { get; set; }
        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public string? description { get; set; }

        public Row() { }

        public Row(string id, string title, string description = null)
        {
            this.id = id;
            this.title = title;
            this.description = description;
        }
    }
}
