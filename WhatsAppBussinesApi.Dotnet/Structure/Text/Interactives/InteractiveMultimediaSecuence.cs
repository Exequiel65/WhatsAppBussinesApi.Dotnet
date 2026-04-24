using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using JsonIgnoreCondition = System.Text.Json.Serialization;

namespace WhatsAppBussinesApi.Dotnet.Structure.Text.Interactives
{
    public class InteractiveMultimediaSecuence : BaseMessage
    {
        public override TypeMessage type => TypeMessage.interactive;
        public required SequenceInteractiveComponent interactive { get; init; }
        public InteractiveMultimediaSecuence()
        {
        }
        [SetsRequiredMembers]
        public InteractiveMultimediaSecuence(string to, SequenceInteractiveComponent component)
        {
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            interactive = component ?? throw new ArgumentNullException(nameof(component));
        }
    }


    public abstract class BaseInteractive
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract InteractiveType type { get; }

        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public BaseHeader? header { get; set; }
        public TextBody body { get; set; }

        [JsonIgnoreCondition.JsonIgnore(Condition = JsonIgnoreCondition.JsonIgnoreCondition.WhenWritingNull)]
        public FooterText? footer { get; set; }

        public BaseAction action { get; set; }
    }

    public class SequenceInteractiveComponent : BaseInteractive
    {
        public override InteractiveType type => InteractiveType.carousel;

        public SequenceInteractiveComponent(string textBody, SequenceAction sequence,  BaseHeader? header = null, FooterText? footer = null)
        {
            this.body = new TextBody(textBody);
            this.action = sequence;
            this.header = header;
            this.footer = footer;

        }
    }


    public class SequenceAction : BaseAction
    {
        public List<CardAction> cards { get; set; }
        public SequenceAction()
        {
            cards = new List<CardAction>();
        }
        public SequenceAction(List<CardAction> sections)
        {
            this.cards = sections ?? throw new ArgumentNullException(nameof(sections));
        }
    }


    public class CardAction
    {
        public required string card_index { get; init; }
        public string type { get; } = "cta_url";
        public required HeaderCard header { get; init; }
        public required TextBody body { get; init; }

        public required BaseAction action { get; init; }

        [SetsRequiredMembers]
        public CardAction(string cardIndex, HeaderCard header, TextBody body, BaseAction action)
        {
            this.card_index = cardIndex ?? throw new ArgumentNullException(nameof(cardIndex));
            this.header = header ?? throw new ArgumentNullException(nameof(header));
            this.body = body ?? throw new ArgumentNullException(nameof(body));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }
    }


    public class HeaderCard 
    { 
        public HeaderCardType type { get; init; }
        public DataHeaderCard? image { get; init; }
        public DataHeaderCard? video { get; init; }

        public HeaderCard(string url, HeaderCardType type)
        {
            this.type = type;
            if (type == HeaderCardType.video)
            {
                this.video = new DataHeaderCard(url);
            }else if (type == HeaderCardType.image)
            {
                this.image = new DataHeaderCard(url);
            }
        }
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeaderCardType
    {
        image,
        video
    }

    public class DataHeaderCard
    {
        public string link { get; init; }
        public DataHeaderCard(string link)
        {
            this.link = link;
        }
    }
}
